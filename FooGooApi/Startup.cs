using Autofac;
using FooGooBusiness;
using FooGooDapper;
using FooGooEf;
using FooGooMongoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;

namespace FooGooApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FooGooApi", Version = "v1" });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new FooGooBusinessModule());

            //var connectionString = Configuration["ConnectionStrings:MongoDbDefault"];
            //builder.RegisterModule(new MongoDbApplicationModule(connectionString));

            //var connectionString = Configuration["ConnectionStrings:EfDefault"];
            //builder.RegisterModule(new EfApplicationModule(connectionString));

            var connectionString = Configuration["ConnectionStrings:DapperDefault"];
            builder.RegisterModule(new DapperApplicationModule(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FooGooApi V1");
            });

            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
        }
    }
}