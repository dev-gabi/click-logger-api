using Api;
using Bl;
using Bl.Auth;
using Dal;
using Entities.Configutation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace ClickLoggerSql
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IWebHostEnvironment _env;
        public IConfiguration Configuration { get; }
        readonly string CorsOrigins = "origins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy(name: CorsOrigins,
                                  builder =>
                                  {
                                      if (_env.IsDevelopment())
                                      {
                                          builder.WithOrigins("http://localhost:4200");
                                      }
                                      else
                                      {
                                          builder.WithOrigins("http://coolgift-001-site9.gtempurl.com/");

                                      }
                         
                                      builder.WithHeaders("Access-Control-Allow-Headers", "Access-Control-Allow-Origin", "Content-Type", "Authorization")
                                            .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS");
                                  });
            });

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = Configuration["JWTSettings:Audience"],
                    ValidIssuer = Configuration["JWTSettings:Issuer"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTSettings:EncKey"])),
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddDbContext<Dal.AppContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(GenericRepository<>));
            services.AddScoped<IActivityLogger, ActivityLogger>();
            services.AddScoped<IAuthService, AuthService>();

            services.Configure<JwtConfig>(Configuration.GetSection("JWTSettings"));

            services.AddControllers();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClickLoggerSql", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClickLoggerSql v1"));
            }

          //  app.UseHttpsRedirection();

            app.UseRouting();

          app.UseCors(CorsOrigins);

            app.UseMiddleware<JWTMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
