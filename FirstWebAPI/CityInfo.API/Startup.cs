using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CityInfo.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {                
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) => {
                   await context.Response.WriteAsync($"Hello from Middleware 1 {Environment.NewLine}");
                   await next();
            });

            

            app.Map("/test",action =>{
                action.Use(async (context,next) =>{
                  await  context.Response.WriteAsync($"Hello from Map middleware Use 1 {Environment.NewLine}");
                   await next();
                });
                
                action.Use(async (context,next) =>{
                   await context.Response.WriteAsync($"Hello from Map middleware Run 1 {Environment.NewLine}");
                   await next();
                });

                action.MapWhen(context => context.Request.Query.ContainsKey("name"),action1 =>{
                action1.Use(async (context,next) =>{
                   await context.Response.WriteAsync($"Hello from MapWhen middleware Use 2 {Environment.NewLine}");
                   await next();
                });
                
                action1.Run(async context =>{
                  await  context.Response.WriteAsync($"Hello from MapWhen middleware Run 2 {Environment.NewLine}");
                });
            });
            });

            app.MapWhen(context => context.Request.Query.ContainsKey("name"),action =>{
                action.Use(async (context,next) =>{
                   await context.Response.WriteAsync($"Hello from MapWhen middleware Use 1 {Environment.NewLine}");
                   await next();
                });
                
                action.Run(async context =>{
                  await  context.Response.WriteAsync($"Hello from MapWhen middleware Run 1 {Environment.NewLine}");
                });
            });

            app.Run(async context => {
               await context.Response.WriteAsync($"Hello from Middleware 2 {Environment.NewLine}");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Hello World! {Environment.NewLine}");
            });
        }
    }
}
