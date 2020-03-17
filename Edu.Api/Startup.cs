using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Atom.EF.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Autofac;
using Atom.EF.Base;
using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using Edu.Svc;
using Edu.Svc.Interface;
using Microsoft.AspNetCore.Mvc;
using Edu.Repo;
using Edu.Repo.Interface;
using Edu.Api.Infrastructure;
using Edu.Api.Infrastructure.Filters;
using Edu.Api.Infrastructure.Formatters;
using Edu.Api.Infrastructure.Authorizes;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Edu.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "领路人教育接口文档",
                    Description = ".net Core 领路人教育接口文档",
                    Version = "v1.0"
                });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "登录返回的Jwt-Token",
                    Name = "Jwt-Token",

                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });



                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);

            });

            //controller属性注册要加AddControllersAsServices()
            services.AddControllers().AddControllersAsServices();
            services.AddRouting();
            services.AddMvc(option =>
            {
                option.Filters.Add(typeof(GlobalExceptionFilter));
                option.Filters.Add<CheckParamsAttribute>();
            });

            //参数自定义检查去掉自带的验证
            services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //注册controller 注册之后才可以在controller里使用Autofac属性注入
            var controllersTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes()
            .Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToArray();
            builder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired();

            //注册DbContext为Scope模式
            var connStr = Configuration.GetValue<string>("ConnectionStrings:DbConn");
            builder.RegisterType<AContext>().As<IAContext>().WithParameter(new TypedParameter(typeof(DbContextOption), new DbContextOption
            {
                ConnectionString = connStr,
                ModelAssemblyName = "Edu.Entity"
            })).PropertiesAutowired().InstancePerLifetimeScope();

            //注册Service,Repository
            Assembly service = Assembly.Load("Edu.Svc");
            Assembly repository = Assembly.Load("Edu.Repo");
            builder.RegisterAssemblyTypes(service).Where(t => t.Name.EndsWith("Svc")).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterAssemblyTypes(repository).Where(t => t.Name.EndsWith("Repo")).AsImplementedInterfaces().PropertiesAutowired();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            //自动创建数据库
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<IAContext>();
                context.EnsureCreated();
            }

            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthorization();

            app.UseSwagger(opt => { });
            app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "领路人教育接口文档"));
            app.UseEndpoints(endpoints => endpoints.MapControllers());


        }
    }
}
