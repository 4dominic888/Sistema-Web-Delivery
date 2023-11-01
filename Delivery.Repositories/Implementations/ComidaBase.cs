using Delivery.Domain.Food;
using Delivery.Persistence.Data;
using Delivery.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
		public async Task ActualizarCaracteristica(CaracteristicaComida categoriaComida)
		{
			_Comidacontext.CaracteristicaComidas.Update(categoriaComida);
			await _context.SaveChangesAsync();
		}
		public async Task EliminarCaracteristica(CaracteristicaComida caracteristicaComida)
		{
            int id = caracteristicaComida.Id;

            var listado = await _context.Comida_Caracteristicas.Where(x => x.IdCaracteristicaComida == id).ToListAsync();
            _context.Comida_Caracteristicas.RemoveRange(listado);

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
        public async Task<IEnumerable<CaracteristicaComida>> ObtenerCaracteristicasPorComidaID(int id)
        {
            List<CaracteristicaComida> listaIndices = new List<CaracteristicaComida>();
            var listado = await _context.Comida_Caracteristicas.Where(x => x.IdComida == id).ToListAsync();
            listado.ForEach(x =>
            {
                listaIndices.Add(x.CaracteristicaComida);
            });
            return listaIndices;
        }
        public async Task AgregarRelacionCaracteristicaComida(int idComida, int idCaracteristica)
        {
            await _context.Comida_Caracteristicas.AddAsync(new Comida_Caracteristica(idComida, idCaracteristica));
        }

        #endregion


        public async Task AgregarComida(Comida comida, string listaIndicescarac)
        {
            await Agregar(comida);
            await Guardar(); //Para que se genere su id o algo asi
            //Agregar la relación de características de comidas con Comidas
            //No se agrega nada si no hay nada en la lista
            List<int> listaIndices = JsonConvert.DeserializeObject<List<int>>(listaIndicescarac);
            foreach (int i in listaIndices)
            {
                await AgregarRelacionCaracteristicaComida(comida.ID, i);
            }
        }
        public async Task EditarComida(Comida comida, string listaIndicescara)
        {
            _context.Comidas.Update(comida);
            var listaIndices = JsonConvert.DeserializeObject<List<int>>(listaIndicescara);
            var listaIndicesBorrar = await _context.Comida_Caracteristicas.Where(x => x.IdComida == comida.ID).ToListAsync();

            _context.Comida_Caracteristicas.RemoveRange(listaIndicesBorrar); //Borrar registros previos
            foreach(var item in listaIndices)
            {
                await AgregarRelacionCaracteristicaComida(comida.ID, item);
            }
        }
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
        public void EliminarImagen(string productURL, IWebHostEnvironment _webHostEnvironment)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            string upload = webRootPath + @"\imagenes\Comida\";
            string ruta = upload + productURL;
            Console.WriteLine(ruta);
            System.IO.File.Delete(ruta);
        }

    }
}
