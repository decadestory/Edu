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
using Atom.Logger;
using Atom.ConfigCenter;
using Atom.Permissioner;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Atom.Starter;
using Atom.Logger.Ui;
using Atom.Starter.Ui;
using Atom.ConfigCenter.Ui;

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
                    Title = "��·�˽����ӿ��ĵ�",
                    Description = ".net Core ��·�˽����ӿ��ĵ�",
                    Version = "v1.0"
                });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "��¼���ص�Jwt-Token",
                    Name = "Jwt-Token",

                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                     new OpenApiSecurityScheme{Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme,Id = "Bearer"}},new string[] { }}
                 });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);

            });

            //controller����ע��Ҫ��AddControllersAsServices()
            services.AddControllers().AddControllersAsServices().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            }).AddNewtonsoftJson(options =>
            {
                //����ʱ���ʽ
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //����ѭ������
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //���ݸ�ʽ����ĸСд
                //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                //���ݸ�ʽ��ԭ�����
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //���Կ�ֵ
                //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddRouting();
            services.AddMvc(option =>
            {
                option.Filters.Add(typeof(GlobalExceptionFilter));
                //option.Filters.Add<CheckParamsAttribute>();

                // ʹ�� ServiceStack.Text �滻Ĭ�ϵ� JSON ����
                //option.InputFormatters.Clear();
                //option.InputFormatters.Add(new ServiceStackTextInputFormatter());
                //option.OutputFormatters.Clear();
                //option.OutputFormatters.Add(new ServiceStackTextOutputFormatter());
            });

            //�����Զ�����ȥ���Դ�����֤
            services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //ע��controller ע��֮��ſ�����controller��ʹ��Autofac����ע��
            var controllersTypesInAssembly = typeof(Startup).Assembly.GetExportedTypes().Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToArray();
            builder.RegisterTypes(controllersTypesInAssembly).PropertiesAutowired();

            //ע��EFCoreΪScopeģʽ
            //var connStr = Configuration.GetValue<string>("ConnectionStrings:DbConn");
            //builder.RegisterType<AContext>().As<IAContext>().WithParameter(new TypedParameter(typeof(DbContextOption), new DbContextOption
            //{
            //    ConnectionString = connStr,
            //    ModelAssemblyName = "Edu.Entity"
            //})).PropertiesAutowired().InstancePerLifetimeScope();

            //ע��EF6ΪScopeģʽ
            var connStr = Configuration.GetValue<string>("ConnectionStrings:DbConn");
            builder.RegisterType<AContextEF6>().As<IAContextEF6>().WithParameter(new TypedParameter(typeof(DbContextOption), new DbContextOption
            {
                ConnectionString = connStr,
                ModelAssemblyName = "Edu.Entity"
            })).PropertiesAutowired().InstancePerLifetimeScope();

            //ע��Service,Repository
            Assembly service = Assembly.Load("Edu.Svc");
            Assembly repository = Assembly.Load("Edu.Repo");
            builder.RegisterAssemblyTypes(service).Where(t => t.Name.EndsWith("Svc")).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterAssemblyTypes(repository).Where(t => t.Name.EndsWith("Repo")).AsImplementedInterfaces().PropertiesAutowired();

            //ע����־����
            var logConnStr = Configuration.GetValue<string>("ConnectionStrings:DbLogConn");
            var logSvcStr = Configuration.GetValue<string>("AppSetting:LoggerSvc");
            builder.Register(l => new ALogger(logConnStr, logSvcStr)).As<IALogger>().PropertiesAutowired().SingleInstance();
            builder.RegisterType(typeof(LoggerController)).PropertiesAutowired();


            //ע����������
            builder.Register(l => new AtomConfigCenter(connStr)).As<IAtomConfigCenter>().PropertiesAutowired().SingleInstance();
            builder.RegisterType(typeof(ConfigCenterController)).PropertiesAutowired();

            //ע��Ȩ�޹���
            builder.Register(l => new Permissioner(connStr)).As<IPermissioner>().PropertiesAutowired().SingleInstance();

            //ע��������Ŀ����
            builder.Register(l => new AStarter(connStr)).As<IAStarter>().PropertiesAutowired().SingleInstance();
            builder.RegisterType(typeof(AtomStarterController)).PropertiesAutowired();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            //EFCore�Զ��������ݿ�
            //using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetRequiredService<IAContext>();
            //    context.EnsureCreated();
            //}

            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthorization();

            app.UseSwagger(opt => { });
            app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "��·�˽����ӿ��ĵ�"));
            app.UseEndpoints(endpoints => endpoints.MapControllers());


        }
    }
}
