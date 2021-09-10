using Common;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service
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
            //services.AddRazorPages();
            services.AddMvc();

            string redis_host = Configuration["RabbitMQ:Host"]; //Environment.GetEnvironmentVariable("redis_host"); //"amqp://10.200.4.47";
            string redis_user = Configuration["RabbitMQ:User"];// Environment.GetEnvironmentVariable("redis_user"); //"gYKGIwPQ8LbWA-q--Ary5SscJvu0S7D3"
            string redis_pass = Configuration["RabbitMQ:Pass"];//Environment.GetEnvironmentVariable("redis_pass"); //"yWBtb4jkpbq0wVQAQ1iZwe6NUn9aIRN-"

            services.AddMassTransit(x =>
            {
               
               


                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(redis_host), h =>
                     {
                         h.Username(redis_user);
                         h.Password(redis_pass);
                     });

                    cfg.ConfigureEndpoints(context);
                });

                x.AddRequestClient<CheckOrderStatus>();
            });

            services.AddMassTransitHostedService();
            //services.AddHostedService<Worker>()
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
