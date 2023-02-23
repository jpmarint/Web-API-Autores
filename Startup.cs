using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApiAutores.Sevicios;

namespace WebApiAutores
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {



            services.AddControllers().AddJsonOptions(u => 
                u.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
                );

            //services.AddTransient<IServicio, ServicioA>  //Nueva instancia de servicioA así sea en el mismo contexto
            //AddScoped //Cambia el tiempo de vida del servicio, dentro del mismo contexto la misma instancia, pero diferente instancia por client
            //AddSingleton //Misma instancia para todos los usuarios

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
