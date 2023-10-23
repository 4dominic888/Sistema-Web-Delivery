using Delivery.Domain.Food;
using Delivery.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;
using System.Web;

namespace Delivery.Controllers
{
    public class ComidaController : Controller
    {
        private readonly IComidaRepository _comidaRepository;
        public ComidaController(IComidaRepository comidaRepository)
        {
            _comidaRepository = comidaRepository;
        }
        public async Task<IActionResult> EditarMenu()
        {
            ViewBag.Comidas = await _comidaRepository.ObtenerComidas();
            return View();
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


            if (cc.Id == 0) //No editable, solo dirije al formulario para registrar datos nuevos
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
			ViewBag.editable = edit;
			bool modeloValido = true;
            if (idc != 0)
            {
                caracteristica.Id = idc;
            }

            if(caracteristica.Nombre is null) return View();


            if(!(edit == "True"))
            {
                //Evitar que se repita el mismo nombre de caracteristica
                //Esto no aplica cuando el elemento va a ser editado
				if (!await _comidaRepository.CaracteristicaNombreUnico(caracteristica.Nombre))
				{
					modeloValido = false;
					ViewBag.nombreCaracteristicaRepetido = "Esta caracteristica ya ha sido registrada";
				}
			}

			if (ModelState.IsValid && modeloValido)
            {
                CookieOptions cookie = new CookieOptions();
                if (!(edit == "True")) //No editable mode
                {
                    //Agregar elemento
					await _comidaRepository.AgregarCaracteristica(caracteristica);
					Response.Cookies.Append("mensaje", "La característica fue registrada correctamente", cookie);
					return RedirectToAction("CaracteristicaComida");
				}
                else
                {
                    //Actualizar elemento
					await _comidaRepository.ActualizarCategoria(caracteristica);
					ViewBag.editable = false;
					Response.Cookies.Append("mensaje", "La característica fue editada correctamente", cookie);
					return RedirectToAction("CaracteristicaComida", null);
				}

			}
			ViewBag.caracteristicas = await _comidaRepository.ObtenerCaracteristicasComidas();


			return View();
        }

		#endregion

	}
}
