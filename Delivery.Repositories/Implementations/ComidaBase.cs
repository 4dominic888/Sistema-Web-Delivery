using Delivery.Domain.Food;
using Delivery.Persistence.Data;
using Delivery.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Repositories.Implementations
{
	public class ComidaBase : RepositoryBase<Comida>, IComidaRepository
	{
		private readonly DeliveryDBContext _Comidacontext;
        public ComidaBase(DeliveryDBContext context) : base(context)
        {
			_Comidacontext = context;
        }


        #region Caracteristicas Comida
        public async Task AgregarCaracteristica(CaracteristicaComida caracteristica)
		{
			await _context.CaracteristicaComidas.AddAsync(caracteristica);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> CaracteristicaNombreUnico(string nombre)
		{

			CaracteristicaComida comprobar = await _context.CaracteristicaComidas.Where(c =>c.Nombre == nombre.Trim().ToLower()).FirstOrDefaultAsync();
			if (comprobar == null) return true;
			else return false;
		}

		public async Task ActualizarCategoria(CaracteristicaComida categoriaComida)
		{
			_Comidacontext.CaracteristicaComidas.Update(categoriaComida);
			await _context.SaveChangesAsync();
		}

		public async Task EliminarCaracteristica(CaracteristicaComida caracteristicaComida)
		{
			_Comidacontext.CaracteristicaComidas.Remove(caracteristicaComida);
			await _Comidacontext.SaveChangesAsync();
		}

		public async Task<CaracteristicaComida> ObtenerCaracteristicaPorID(int id)
		{
			return await _Comidacontext.CaracteristicaComidas.Where(c => c.Id == id).FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<CaracteristicaComida>> ObtenerCaracteristicasComidas()
		{
			return await _Comidacontext.CaracteristicaComidas.ToListAsync();
		}

        #endregion

        
		public async Task<IEnumerable<Comida>> ObtenerComidas()
        {
            return await _Comidacontext.Comidas.ToListAsync();
        }

        public string CargarImagen(HttpContext httpContext, IWebHostEnvironment _webHostEnvironment)
        {
            //Verificar la imagen enviada
            var files = httpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;

            string upload = webRootPath + @"\imagenes\Comida\";
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(files[0].FileName);

            //Cargar en el servidor
            using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }
            return fileName + extension;
        }

        public async Task AgregarRelacionCaracteristicaComida(int idComida, int idCaracteristica)
        {
			await _context.Comida_Caracteristicas.AddAsync(new Comida_Caracteristica(idComida, idCaracteristica));
        }
    }
}
