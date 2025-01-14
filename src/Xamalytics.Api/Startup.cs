using System.Collections.ObjectModel;
using System.Data;
using System.Net;
using MediatR;
using Xamalytics.Api.DI;
using Xamalytics.Api.Services;
using Xamalytics.Common.Helpers;
using Xamalytics.Services.Interface.Common;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Xamalytics.Common;

namespace Xamalytics.Api
{
    public class Startup
    {
        private const string AllowSpecificOrigins = "_AllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //Logging
            var connectionString = Configuration.GetConnectionString("Xamalytics");
            var columnOptions = new ColumnOptions
            {
                AdditionalColumns = new Collection<SqlColumn>
               {
                   new SqlColumn("UserName", SqlDbType.NVarChar)
               }
            };
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.MSSqlServer(connectionString, sinkOptions: new MSSqlServerSinkOptions { TableName = "ApplicationError", SchemaName = "adt" }
                , null, null, LogEventLevel.Information, columnOptions: columnOptions)
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSetting>
                        (Configuration.GetSection("AppSetting"));

            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMediatR(AppDomain.CurrentDomain.Load("Xamalytics.Services.Interface"));
            services.AddMediatR(AppDomain.CurrentDomain.Load("Xamalytics.Services.Implementation"));
            services.AddMediatR(AppDomain.CurrentDomain.Load("Xamalytics.Application"));


            services.AddInfrastructure(Configuration);
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddSingleton(Log.Logger);
            services.AddHttpContextAccessor();
            services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = options.DefaultPolicy;
            });

            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOrigins,
                                      policy =>
                                      {
                                          policy.WithOrigins(Configuration.GetSection(Constants.CorsOrigins).Get<string[]>())
                                                              .AllowAnyHeader()
                                                              .AllowAnyMethod()
                                                              .AllowCredentials();
                                      });
            });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var swaggerPrefix = "/xamalytics";
            if(env.IsDevelopment()) swaggerPrefix = string.Empty;
                        
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DefaultModelsExpandDepth(-1);
                c.SwaggerEndpoint($"{swaggerPrefix}/swagger/v1/swagger.json", "Xamalytics API v1");
            });

            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    // write error in http response
                    if (error != null)
                    {
                        context.Response.AddApplicationError(error.Error.Message);
                        await context.Response.WriteAsync(error.Error.Message);
                    }
                });
            });


            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors(builder => builder
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseAuthentication();            
            app.UseAuthorization();


            app.Use(async (httpContext, next) =>
            {
                var userName = httpContext.User.Identity is {IsAuthenticated: true} ? httpContext.User.Identity.Name : "Guest";
                LogContext.PushProperty("Username", userName); 
                await next.Invoke();
            }
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
