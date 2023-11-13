using Delivery.Domain.Food;
using Delivery.Domain.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Persistence.Data
{
    public class DeliveryDBInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DeliveryDBContext>();
                context.Database.EnsureCreated();

                if(!context.Administradores.Any())
                {
                    context.Administradores.AddRange(new List<Administrador>()
                    {
                        new Administrador()
                        {
                            Surname= "Torres",
                            Name="Admin",
                            Phone="987654321",
                            Sexo=Sexo.Otros,
                            Email="admin@delivery.com",
                            Password="15e2b0d3c33891ebb0f1ef609ec419420c20e320ce94c65fbc8c3312448eb225", //123456789
                            DateBirth=new DateTime(1980,6,5),
                            Rol="Administrador"
                        }
                    });
                }

                context.SaveChanges();
            }

        }
    }
}
