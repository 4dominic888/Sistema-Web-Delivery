using Delivery.Domain.Food;
using Delivery.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Controllers
{
    public class ComidaController : Controller
    {
        private readonly IComidaRepository _comidaRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ComidaController(
            IComidaRepository comidaRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _comidaRepository = comidaRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        #region VerMenu

        public async Task<IActionResult> ConsultarStock(int idComida)
        {
            var comida = await _comidaRepository.ObtenerPorId(idComida);
            int stock = comida.Stock;
            return Json(stock);
        }

        public async Task<IActionResult> RealizarPedido(string listaComidasPedido, string idcliente, string precio)
        {
            TempData["lista"] = listaComidasPedido;
            TempData["idCliente"] = idcliente;
            TempData["PrecioTotal"] = precio;
            return RedirectToAction("CrearPedido", "Pedido");
        }

        //Vista parcial para ver las comidas pedidas antes de hacer el envio
        public async Task<IActionResult> _VerComidasPedido()
        {   
            return PartialView();
        }

        //Vista parcial para elegir alguna característica de una comida
        [Authorize(Roles = "Cliente")]
        public async Task<IActionResult> _ElegirCaracComida(int id = 0)
        {
            await _comidaRepository.ObtenerCaracteristicasComidas();
            if (id is 0) return PartialView();
            else
            {
                Comida comida = await _comidaRepository.ObtenerPorId(id);
                ViewBag.caracteristicas2 = await _comidaRepository.ObtenerCaracteristicasPorComidaID(id);
                return PartialView(comida);
            }
        }


        [HttpGet]
        public async Task<IActionResult> VerMenu()
        {
            var listaComidas = await _comidaRepository.ObtenerComidas();
            return View("VerMenu", listaComidas);
        }

        #endregion

        #region Editar Menú

        //Vista de modificar el menú en general
        //TODO: Agregar restricciones de roles
        [Authorize(Roles = "Chef, Administrador")]
        public async Task<IActionResult> EditarMenu()
        {
            var comidas = await _comidaRepository.ObtenerComidas();
            ViewBag.caracteristicas = await _comidaRepository.ObtenerCaracteristicasComidas();
            ViewBag.modeloValido = true;
            ViewBag.modo = "Nada"; //Para la vista parcial
            return View(comidas);
        }



        //Vista parcial offcanvas del editar menú
        [Authorize(Roles = "Chef, Administrador")]
        public async Task<IActionResult> _ModificarComida(int id = 0)
        {
            ViewBag.caracteristicas = await _comidaRepository.ObtenerCaracteristicasComidas();
            ViewBag.modeloValido = true;
            ViewBag.anteriorImagen = "";
            ViewBag.listcaract = "[]";

            if (id is 0) return PartialView();
            else
            {
                Comida comida = await _comidaRepository.ObtenerPorId(id);


                ViewBag.comidaCarac = await _comidaRepository.ObtenerCaracteristicasPorComidaID(id);


                if (comida != null) return PartialView(comida);
                else return NotFound();
            }
        }



        //Para editar el stock de la comida seleccionada
        [Authorize(Roles = "Chef, Administrador")]
        public async Task<IActionResult> EditarStockComida(int idc, int nuevoStock)
        {
            Comida comida = await _comidaRepository.ObtenerPorId(idc);
            comida.Stock = nuevoStock;
            await _comidaRepository.EditarComida(comida);
            return RedirectToAction("EditarMenu");
        }


        //Para eliminar la comida seleccionada
        [Authorize(Roles = "Chef, Administrador")]
        public async Task<IActionResult> EliminarComida(int idcomida)
        {
            Comida comida = await _comidaRepository.ObtenerPorId(idcomida);
            _comidaRepository.EliminarImagen(comida.Imagen, _webHostEnvironment);
            await _comidaRepository.EliminarComida(comida);
            return RedirectToAction("EditarMenu");
        }


        //Para la acción de editar la comida seleccionada
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Chef, Administrador")]
        public async Task<IActionResult> EditarComida(
            [Bind("ID, Nombre", "Descripcion", "Categoria", "Precio",
            "MenuDelDia", "Stock")] Comida comida, string listaIndicescarac = "", 
            string urlant = "", string ChangeImage = "si")
        {
            ViewBag.caracteristicas = await _comidaRepository.ObtenerCaracteristicasComidas(); //Obtiene el listado general de caracteristicas, para los select
            ViewBag.modeloValido = true; //Para hacer aparecer automaticamente el offcanvas
            ViewBag.modo = "Editar"; //Para la vista parcial


            //El modelo es valido
            if (ModelState.IsValid)
            {
                if (ChangeImage == "si")
                {
                    comida.Imagen = _comidaRepository.CargarImagen(HttpContext, _webHostEnvironment);
                    _comidaRepository.EliminarImagen(urlant, _webHostEnvironment);
                }
                else comida.Imagen = urlant;
                

                await _comidaRepository.EditarComida(comida, listaIndicescarac);
                await _comidaRepository.Guardar();
                

                ViewBag.modeloValido = true;
                return RedirectToAction("EditarMenu");
            }
            ViewBag.modeloValido = false;
            ViewBag.anteriorImagen = urlant;
            ViewBag.listcaract = listaIndicescarac;
            ViewBag.comidaCarac = await _comidaRepository.ObtenerCaracteristicasPorComidaID(comida.ID);
            var comidas = await _comidaRepository.ObtenerComidas();

            return View("EditarMenu", comidas);

        }



        //Para agregar la comida a la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Chef, Administrador")]
        public async Task<IActionResult> AgregarComida(
            [Bind("Nombre", "Descripcion", "Categoria", "Precio",
            "MenuDelDia", "Stock")] Comida comida, string listaIndicescarac = "")
        {
            var comidas = await _comidaRepository.ObtenerComidas();
            ViewBag.caracteristicas = await _comidaRepository.ObtenerCaracteristicasComidas(); //Obtiene el listado general de caracteristicas, para los select
            ViewBag.modeloValido = true; //Para hacer aparecer automaticamente el offcanvas
            ViewBag.modo = "Crear"; //Para la vista parcial


            //El modelo es valido
            if (ModelState.IsValid)
            {
                try //Para cuando la imagen no se manda
                {
                    comida.Imagen = _comidaRepository.CargarImagen(HttpContext, _webHostEnvironment);
                    await _comidaRepository.AgregarComida(comida, listaIndicescarac);
                    await _comidaRepository.Guardar();
                }
                catch (ArgumentOutOfRangeException)
                {
                    comida.Imagen = null;
                    ViewBag.imagenNula = "Debes subir un archivo";
                    ViewBag.modeloValido = false;
                }


                ViewBag.modeloValido = true;
                return RedirectToAction("EditarMenu");
            }
            ViewBag.modeloValido = false;
            return View("EditarMenu", comidas);
        }

        #endregion

        #region CaracteristicasComida
        [Authorize(Roles = "Chef, Administrador")]
        public async Task<IActionResult> EditarCaracteristica(int id)
        {
			CaracteristicaComida cc = await _comidaRepository.ObtenerCaracteristicaPorID(id);
			if (cc is not null) //Se encontró el elemento
			{
                return RedirectToAction("CaracteristicaComida", cc);
			}
			else return NotFound();

		}

        [Authorize(Roles = "Chef, Administrador")]
        public async Task<IActionResult> EliminarCaracteristica(int id)
        {
            CaracteristicaComida cc = await _comidaRepository.ObtenerCaracteristicaPorID(id);
            if (cc is not null) //Se encontró el elemento
            {
                await _comidaRepository.EliminarCaracteristica(cc);
				CookieOptions cookie = new CookieOptions();
				Response.Cookies.Append("mensaje", "La característica fue eliminada correctamente", cookie);
				return RedirectToAction("CaracteristicaComida");
            }
            else return NotFound();
        }

        //GET
        [Authorize(Roles = "Chef, Administrador")]
        public async Task<IActionResult> CaracteristicaComida(CaracteristicaComida cc = null)
        {
            ViewBag.caracteristicas = await _comidaRepository.ObtenerCaracteristicasComidas();
			string estado = Request.Cookies["mensaje"];
            if(estado is not null)
            {
                ViewBag.resultado = estado;
				Response.Cookies.Delete("mensaje");
			}


            if (cc.Nombre is null) //No editable, solo dirije al formulario para registrar datos nuevos
            {
                ViewBag.editable = false;
                return View();
            }
			else //Editable
			{
				ViewBag.editable = true;
				return View(model: cc);
            }
        }


        [HttpPost]
        [Authorize(Roles = "Chef, Administrador")]
        public async Task<IActionResult> CaracteristicaComida(
            [Bind("Nombre, Detalle, Precio")] CaracteristicaComida caracteristica, int idc=0, string edit = "False")
        {
			ViewBag.editable = edit; //Para que esté en un estado de editable o no editable
			bool modeloValido = true; //Para comprobar si el nombre se repite o no
            if (idc != 0) //Colocar id de manera auxiliar
            {
                caracteristica.Id = idc;
            }


            if(!(edit == "True"))
            {
                //Evitar que se repita el mismo nombre de caracteristica
                //Esto no aplica cuando el elemento va a ser editado
                try
                {
                    if (!await _comidaRepository.CaracteristicaNombreUnico(caracteristica.Nombre))
                    {
                        modeloValido = false;
                        ViewBag.nombreCaracteristicaRepetido = "El nombre debe ser único para cada característica";
                    }
                }
                catch(Exception ex)
                {

                }
			}



			if (ModelState.IsValid && modeloValido) //Modelo completamente válido
            {
                CookieOptions cookie = new CookieOptions();
                if (!(edit == "True")) //No editable mode
                {
                    //Agregar elemento
					await _comidaRepository.AgregarCaracteristica(caracteristica);
					ViewBag.caracteristicas = await _comidaRepository.ObtenerCaracteristicasComidas();
					Response.Cookies.Append("mensaje", "La característica fue registrada correctamente", cookie);
					return RedirectToAction("CaracteristicaComida");
				}
                else
                {
                    //Actualizar elemento
					await _comidaRepository.ActualizarCaracteristica(caracteristica);
					ViewBag.editable = false;
					ViewBag.caracteristicas = await _comidaRepository.ObtenerCaracteristicasComidas();
					Response.Cookies.Append("mensaje", "La característica fue editada correctamente", cookie);
					return RedirectToAction("CaracteristicaComida");
				}

			}
			ViewBag.caracteristicas = await _comidaRepository.ObtenerCaracteristicasComidas();


            if (edit == "True") return View(caracteristica);
            else return View();
        }

		#endregion

	}
}
