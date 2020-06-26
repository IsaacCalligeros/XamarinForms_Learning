using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Middle_Manager_API.Auth;
using Middle_Manager_API.Data;
using Middle_Manager_API.Data.Interfaces;
using Middle_Manager_API.Data.Repositories;
using Middle_Manager_API.Filters;
using Middle_Manager_API.Helpers;
using Middle_Manager_API.Helpers.Interfaces;
using Middle_Manager_API.Services;
using Middle_Manager_API.Services.Auth;
using Middle_Manager_API.Services.Tasks;
using SharedClassLibrary.Dtos;
using SharedClassLibrary.Helpers;
using SharedClassLibrary.Models;

namespace Middle_Manager_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole<int>>(config =>
                {
                    config.Password.RequiredLength = 5;
                    config.Password.RequireDigit = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                    config.SignIn.RequireConfirmedEmail = false;
                    config.SignIn.RequireConfirmedAccount = false;
                })
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            services.AddControllers().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddCors();
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddTransient<Seed>();

            //https://stackoverflow.com/questions/48554480/ef-core-get-authenticated-username-in-shadow-properties/48554738#48554738
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IEmailHelper, EmailHelper>();
            services.AddScoped<EntitySaveFilter>();

            //https://stackoverflow.com/questions/38138100/addtransient-addscoped-and-addsingleton-services-differences
            void AddTransient(Type t)
            {
                var tInter = t.GetInterfaces().FirstOrDefault();
                if (tInter != null) services.AddTransient(tInter, t);
            }
        
            void AddScoped(Type t)
            {
                var tInter = t.GetInterfaces().FirstOrDefault();
                if (tInter != null) services.AddScoped(tInter, t);
            }

            var serviceBaseHelperType = typeof(ServiceBaseHelper);
            var serviceHelperTypes = serviceBaseHelperType.Assembly
                .GetTypes()
                .Where(t => t != serviceBaseHelperType && serviceBaseHelperType.IsAssignableFrom(t))
                .ToArray();

            foreach (var serviceHelper in serviceHelperTypes)
            {
                AddTransient(serviceHelper);
            }

            //TODO: This is currently added by adding the Repository Wrapper instead through DBSets, going to leave this commented out for now

            //var models = typeof(User).Assembly
            //    .GetTypes()
            //    .Where(t => t.Namespace == typeof(User).Namespace && !t.IsInterface);

            //var genericRepo = typeof(Repository<>);
            //var genericIRepo = typeof(IRepository<>);

            //HashSet<Type> repositoryTypes = new HashSet<Type>();

            //foreach (var dataType in models)
            //{
            //    var combinedRepo = genericRepo.MakeGenericType(dataType);
            //    AddScoped(combinedRepo);
            //}

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer("OAuth", options =>
            {

                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Query.ContainsKey("mobileAccessToken"))
                        {
                            context.Token = context.Request.Query["mobileAccessToken"];
                        }
                        return Task.CompletedTask;
                    }
                };

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidIssuer = Configuration.GetSection("ClaimSettings:Issuer").Value,
                    ValidAudience = Configuration.GetSection("ClaimSettings:Audience").Value,
                };
            });

            services.AddAuthorization(config =>
            {
                ////TODO: Ready up on builder pattern more.
                //var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                //var defaultAuthPolicy = defaultAuthBuilder
                //    .RequireAuthenticatedUser()
                //    .Build();

                config.AddPolicy("Claim.DoB", policyBuilder =>
                {
                    //policyBuilder.AddRequirements(new CustomClaims(ClaimTypes.DateOfBirth));
                    policyBuilder.RequireCustomClaim(ClaimTypes.DateOfBirth);
                });
            });

            //Scoped for adding own services later potentially...
            services.AddScoped<IAuthorizationHandler, CustomRequireClaimHandler>();
            //Only adding for swagger during testing potentially drop for prod.
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo() { Title = "Middle Manager Api", Version = "openapi: 3.0.0" });
            });
        }

        public void Configure(IApplicationBuilder app,
         IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            var swagerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swagerOptions);
            app.UseSwagger(option => { option.RouteTemplate = swagerOptions.JsonRoute; });
            app.UseSwaggerUI(options => { options.SwaggerEndpoint(swagerOptions.UiEndpoint, swagerOptions.Description); });

            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Fallback");
            });

        }
    }
}
