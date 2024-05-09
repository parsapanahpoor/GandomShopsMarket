using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;
using GandomShopsMarket.Application;
using GandomShopsMarket.Presentation.TokenValidator;
using GandomShopsMarket.IoC;
using GandomShopsMarket.Infrastructure.ApplicationDbContext;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace GandomShopsMarket.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Services

            var builder = WebApplication.CreateBuilder(args);

            #region MVC

            builder.Services.AddControllers();
            {
                builder.Services.RegisterApplicationServices();
            }

            #endregion

            #region IoC Container

            //Token Validator
            builder.Services.AddScoped<ITokenValidator, TokenValidate>();

            WebAPI_DependencyContainer.ConfigureDependencies(builder.Services);

            #endregion

            #region Add DBContext

            builder.Services.AddDbContext<GandomShopsMarketDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("GandomShopsMarketDbContextConnection"));
            });

            #endregion

            #region Api Versioning

            builder.Services.AddApiVersioning(Options =>
            {
                Options.AssumeDefaultVersionWhenUnspecified = true;//ای پی آی های قبلی باورژن دیفالت ست بشوند
                Options.DefaultApiVersion = new ApiVersion(1, 0);//معرفی ورژن دیفالت
                Options.ReportApiVersions = true; //افزودن اطلاعات ورژن جاری به هدر جواب درخواست کاربر
            });

            #endregion

            #region Swagger

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GandomShopsMarket", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "GandomShopsMarket", Version = "v2" });

                // برای نمایش سامری ها و داکیومنتیشن
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                // Configure Swagger to use tags for grouping
                //c.TagActionsBy(api => new List<string> { (api.ActionDescriptor.RouteValues["controller"].StartsWith("Admin") ? "Admin Panel Side" : "Site Side") });

                c.TagActionsBy(api =>
                {
                    // Extract area name from route values
                    var areaName = GetAreaNameFromRouteValues(api.ActionDescriptor);
                    var controllerName = api.ActionDescriptor.RouteValues["controller"];

                    // Use area name and controller name for tagging
                    return new List<string> { areaName };
                });

                c.DocInclusionPredicate((doc, apiDescription) =>
                {
                    if (!apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var version = methodInfo.DeclaringType
                        .GetCustomAttributes<ApiVersionAttribute>(true)
                        .SelectMany(attr => attr.Versions);

                    return version.Any(v => $"v{v.ToString()}" == doc);
                });

                var security = new OpenApiSecurityScheme
                {
                    Name = "JWT Auth",
                    Description = "توکن خود را وارد کنید- دقت کنید فقط توکن را وارد کنید",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(security.Reference.Id, security);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { security , new string[]{ } }
                });

            });


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #endregion

            #region  Authentication

            //Add and Set JWT for Authentication of this Project
            builder.Services.AddAuthentication(Options =>
            {
                Options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            //JWT Configurations
            .AddJwtBearer(configureOptions =>
            {
                configureOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = builder.Configuration["JWtConfig:issuer"],
                    ValidAudience = builder.Configuration["JWtConfig:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWtConfig:Key"])),
                    ValidateIssuerSigningKey = true,//امضای توکن را چک میکند
                    ValidateLifetime = true,//توکن هایی که تایم آن ها گذشته است را رد میکند
                };

                configureOptions.SaveToken = true; // HttpContext.GetTokenAsunc(); با این دستور میتوانید هرجایی توکن رو بدست بیاورید

                //لیست ایونت هایی که میتوانید از آن ها استفاده کنید
                configureOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context => { return Task.CompletedTask; },

                    OnTokenValidated = context =>
                    {
                        //log
                        var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidator>();
                        return tokenValidatorService.Execute(context);
                    },

                    OnChallenge = context => { return Task.CompletedTask; },

                    OnMessageReceived = context => { return Task.CompletedTask; },

                    OnForbidden = context => { return Task.CompletedTask; },
                };

            });

            #endregion

            #endregion

            #region Middlewares

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //بااستفاده از رفلکشن تمام ای پی آی های مان را پیدا میکند
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
                    options.RoutePrefix = "swagger";
                    //options.AddHierarchySupport();
                });
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("ApiCORS");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.Run();

            #endregion
        }

        private static string GetAreaNameFromRouteValues(ActionDescriptor actionDescriptor)
        {
            // Attempt to extract the area name from route values
            if (actionDescriptor.RouteValues.TryGetValue("area", out var areaName))
            {
                if (!string.IsNullOrEmpty(areaName))
                {
                    return areaName;
                }
            }

            // Default to "Site" if area is not specified
            return "Site Side";
        }
    }

}