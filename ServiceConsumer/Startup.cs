using GreenPipes;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using AutoMapper;
using ServiceConsumer.Consumer;
using ServiceConsumer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ServiceConsumer
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
            services.AddAutoMapper(typeof(Startup));
            services.AddMassTransit(x =>
            {
                x.AddConsumer<UserConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cur =>
                {
                    cur.UseHealthCheck(provider);
                    cur.Host(new Uri("rabbitmq://rabbitmq"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cur.ReceiveEndpoint("test-queue", oq =>
                    {
                        oq.PrefetchCount = 20;
                        oq.UseMessageRetry(r => r.Interval(2, 100));
                        oq.ConfigureConsumer<UserConsumer>(provider);
                    });
                }));
            });


            services.AddDbContext<DataContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("DefaultConnection"),
                  b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName)));
            services.AddMassTransitHostedService();
            services.AddControllers();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IDataContext, DataContext>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ServiceConsumer",
                    Version = "v1",
                    Description = "Test backend task"
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ServiceConsumer v1"));
            }

            if (env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ServiceConsumer v1"));
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
