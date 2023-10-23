using Delivery.Domain.Food;
using Delivery.Persistence.Data;
using Delivery.Repositories.Interfaces;
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

        public async Task<IEnumerable<Comida>> ObtenerComidas()
        {
            return await _Comidacontext.Comidas.ToListAsync();
        }


    }
}
