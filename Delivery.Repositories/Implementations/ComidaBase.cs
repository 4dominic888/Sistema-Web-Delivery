using Delivery.Domain.Food;
using Delivery.Persistence.Data;
using Delivery.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<List<Comida_CaracteristicaPedido>> DeserealizarJSONPedidoCliente(string JSON)
        {
            //Deserealizar el dato JSON
            var listado = JsonConvert.DeserializeObject<Dictionary<int, List<int[]>>>(JSON);

            if (listado.IsNullOrEmpty()) return new List<Comida_CaracteristicaPedido>();

            //Inicializar el retorno
            var retorno = new List<Comida_CaracteristicaPedido>();


            //Inicialización de variables
            int idComida = 0;
            List<int[]> valores;
            Comida comidaAux = new Comida();
            CaracteristicaComida caracAux = new CaracteristicaComida();
            Comida_CaracteristicaPedido comida_caractAux = new Comida_CaracteristicaPedido();

            //Variable creada para separar comidas iguales con distintas caracteristicas
            int idAgrupamiento = 1;

            //Recorrer cada comida pedida
            foreach(var kvp in listado)
            {
                idComida = kvp.Key;
                valores = kvp.Value;

                comidaAux = await _context.Comidas.Where(x => x.ID == idComida).FirstOrDefaultAsync();

                //Recorrer listado de caracteristicas de comidas repetidas
                foreach (int[] v in valores)
                {
                    //Si la comida elegida no tiene características
                    if (v.IsNullOrEmpty())
                    {
                        comida_caractAux.IdComida = idComida;
                        comida_caractAux.Comida = comidaAux;
                        //TODO: Agregar id del cliente
                        comida_caractAux.agrupamiento = idAgrupamiento;

                        retorno.Add(comida_caractAux);
                        comida_caractAux = new Comida_CaracteristicaPedido();
                        idAgrupamiento++;
                        continue;
                    }

                    //Agrega las caracteristicas una por una
                    foreach (int idCaracteristica in v)
                    {
                        comida_caractAux.IdComida = idComida;
                        comida_caractAux.Comida = comidaAux;
                        comida_caractAux.IdCaracteristicaComida = idCaracteristica;
                        comida_caractAux.CaracteristicaComida =
                            await _context.CaracteristicaComidas.Where(x => x.Id == idCaracteristica).FirstOrDefaultAsync();
                        //TODO: Agregar id del cliente
                        comida_caractAux.agrupamiento = idAgrupamiento;

                        retorno.Add(comida_caractAux);
                        comida_caractAux = new Comida_CaracteristicaPedido();
                    }

                    idAgrupamiento++;
                }
            }

            return retorno;
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

            var listado = await _context.Comida_CaracteristicasMenu.Where(x => x.IdCaracteristicaComida == id).ToListAsync();
            _context.Comida_CaracteristicasMenu.RemoveRange(listado);

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
            List<Comida_CaracteristicaMenu> listado = await _context.Comida_CaracteristicasMenu.Where(x => x.IdComida == id).ToListAsync();
            listado.ForEach(x =>
            {
                listaIndices.Add(x.CaracteristicaComida);
            });
            return listaIndices;
        }
        public async Task AgregarRelacionCaracteristicaComida(int idComida, int idCaracteristica)
        {
            await _context.Comida_CaracteristicasMenu.AddAsync(new Comida_CaracteristicaMenu(idComida, idCaracteristica));
        }

        #endregion

        #region Comida

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
            var listaIndicesBorrar = await _context.Comida_CaracteristicasMenu.Where(x => x.IdComida == comida.ID).ToListAsync();

            _context.Comida_CaracteristicasMenu.RemoveRange(listaIndicesBorrar); //Borrar registros previos
            foreach(var item in listaIndices)
            {
                await AgregarRelacionCaracteristicaComida(comida.ID, item);
            }
        }
        public async Task EditarComida(Comida comida)
        {
            _context.Comidas.Update(comida);
            await Guardar();
        }
        public async Task EliminarComida(Comida comida)
        {
            var listaIndicesBorrar = await _context.Comida_CaracteristicasMenu.Where(x => x.IdComida == comida.ID).ToListAsync();
            _context.Comida_CaracteristicasMenu.RemoveRange(listaIndicesBorrar);

            _context.Comidas.Remove(comida);
            await Guardar();
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

        #endregion
    }
}
