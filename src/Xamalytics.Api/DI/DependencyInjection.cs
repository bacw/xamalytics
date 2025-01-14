using System.Reflection;
using System.Text;
using AutoMapper;
using FluentValidation;
using MediatR;
using Xamalytics.Api.Helpers;
using Xamalytics.Data.Context;
using Xamalytics.Services.Implementation;
using Xamalytics.Services.Implementation.Common;
using Xamalytics.Services.Implementation.Common.Behaviours;
using Xamalytics.Services.Interface;
using Xamalytics.Services.Interface.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Xamalytics.Services.Implementation.Common.Identity;

namespace Xamalytics.Api.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Xamalytics API", Version = "v1" });
                c.CustomSchemaIds(type => type.ToString());
            });
                    

            var config = configuration["ConnectionStrings:Xamalytics"];
           
            // //Database
            services.AddScoped<IXamalyticsContext>(provider => provider.GetService<XamalyticsContext>() ?? throw new InvalidOperationException());

            services.AddDbContext<XamalyticsContext>(
                 options => options.UseSqlServer(config, sqlServerOptionsAction: sqlOptions => {
                     sqlOptions.EnableRetryOnFailure();
                 }));
          
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services
                .AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<XamalyticsContext>();


            //Services
            services.AddScoped<IXamalyticsContext, XamalyticsContext>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IClaimStatusService, ClaimStatusesService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuditUserService, AuditUserService>();
            services.AddScoped<IDateTimeService, DateTimeService>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

           
            services.AddControllers();

            return services;
        }
    }
}
