using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace Presentation.Swagger
{
    public static class AddSwaggerToServices
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Sharp Counter API",
                    Description = "Simple Clean Analytics",
                    Contact = new OpenApiContact
                    {
                        Name = "Avaneesa",
                        Email = string.Empty,
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT"),
                    }
                });
                options.AddSecurityDefinition("Cookie Auth", new OpenApiSecurityScheme
                {
                    Description =
                   "Copy and paste your current cookie",
                    Name = "Cookie Auth",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Cookie"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Cookie Auth",
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });
            });

            return services;
        }
    }
}
