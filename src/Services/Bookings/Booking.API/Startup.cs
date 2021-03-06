using Accounts.Grpc.Protos;
using Booking.API.Data;
using Booking.API.GrpcServices;
using Booking.API.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Booking.API
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Booking.API", Version = "v1" });
            });

            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IBookingContext, BookingContext>();

            services.AddGrpcClient<ActivitiesProtoService.ActivitiesProtoServiceClient>
                (x => x.Address = new Uri(Configuration["GrpcSettings:ActivitiesUri"]));

            services.AddScoped<ActivitiesGrpcService>();

            //services.AddMassTransit(config =>
            //{
            //    config.UsingRabbitMq((ctx, cfg)=>
            //    {
            //        cfg.Host(")
            //    })
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
