using Delivery.Domain.Food;
using Delivery.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

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
        public async Task<IActionResult> EditarMenu()
        {
            ViewBag.Comidas = await _comidaRepository.ObtenerComidas();
            ViewBag.caracteristicas = await _comidaRepository.ObtenerCaracteristicasComidas();
            ViewBag.modeloValido = true;
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarComida(
            [Bind("Nombre", "Descripcion", "Categoria", "Precio",
            "MenuDelDia", "Stock")] Comida comida, string listaIndicescarac = "")
        {
            
            ViewBag.Comidas = await _comidaRepository.ObtenerComidas(); //Obtiene el listado general de comidas
            ViewBag.caracteristicas = await _comidaRepository.ObtenerCaracteristicasComidas(); //Obtiene el listado general de caracteristicas, para los select
            ViewBag.modeloValido = true; //Para hacer aparecer automaticamente el offcanvas
            bool modeloValido = true;


            //El modelo es valido
            if (ModelState.IsValid && modeloValido)
            {
                try //Para cuando la imagen no se manda
                {
                    comida.Imagen = _comidaRepository.CargarImagen(HttpContext, _webHostEnvironment);
                }
                catch (ArgumentOutOfRangeException)
                {
                    comida.Imagen = null;
                    modeloValido = false;
                    ViewBag.imagenNula = "Debes subir un archivo";
                    ViewBag.modeloValido = false;
                }

                await _comidaRepository.Agregar(comida);
                await _comidaRepository.Guardar();

                //Agregar la relación de características de comidas con Comidas
                //No se agrega nada si no hay nada en la lista
                List<int> listaIndices = JsonConvert.DeserializeObject<List<int>>(listaIndicescarac);
                foreach(int i in listaIndices)
                {
                    await _comidaRepository.AgregarRelacionCaracteristicaComida(comida.ID, i);
                }
                await _comidaRepository.Guardar();


                ViewBag.modeloValido = true;
                return RedirectToAction("EditarMenu");
            }
            ViewBag.modeloValido = false;

            return View("EditarMenu");
        }


		#region CaracteristicasComida

		public async Task<IActionResult> EditarCaracteristica(int id)
        {
			CaracteristicaComida cc = await _comidaRepository.ObtenerCaracteristicaPorID(id);
			if (cc is not null) //Se encontró el elemento
			{
                return RedirectToAction("CaracteristicaComida", cc);
			}
			else return NotFound();

		}
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
					await _comidaRepository.ActualizarCategoria(caracteristica);
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
