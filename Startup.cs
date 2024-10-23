using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalProjects.Function.Repositories;
using PersonalProjects.Function.Services;
using System.IO;

namespace PersonalProjects.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Cargar la configuración
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Startup>() // Carga los secretos del usuario
                .Build();

            // Obtener la cadena de conexión de los secretos
            string connectionString = configuration["ConnectionString"];
            
            // Registra el repositorio
            //builder.Services.AddSingleton<IUserRepository>(new UserRepository(connectionString));
            
            builder.Services.AddSingleton<IUserService, UserService>();
        }
    }
}