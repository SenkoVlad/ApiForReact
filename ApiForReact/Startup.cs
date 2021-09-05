using ApiForReact.Data;
using ApiForReact.Repositories.Implementations;
using ApiForReact.Repositories.Intarfaces;
using ApiForReact.Services;
using ApiForReact.Services.Implementations;
using ApiForReact.Services.Intarfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace ApiForReact
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins(new string[] { "http://localhost:3000",
                                                                         "https://apiforreactdocker.azurewebsites.net",
                                                                         "https://senkovlad.github.io",
                                                                         "https://senkovlad.github.io/react-first-app"})
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowCredentials();
                                  });
            });

            services.AddControllers();
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.Cookie.Name = "react-web-cookie";
                options.ExpireTimeSpan = System.TimeSpan.FromDays(10);
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = redirectContext =>
                    {
                        redirectContext.HttpContext.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddAuthorization();

            services.AddSingleton<ITextGeneratorService, TextGeneratorService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDialogRepository, DialogRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDialogService, DialogService>();
            services.AddScoped<IMessageService, MessageService>();

            services.AddHttpContextAccessor();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseLoggerFactory(LoggerFactory.Create(
                    builder =>
                    {
                        builder.AddConsole();
                    }
                ));
                options.UseSqlite("Data Source=Database.db").EnableSensitiveDataLogging();
            });

            services.AddSwaggerGen();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            
            DbInitializerExtention.InitializeAsync(app);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                                    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images")),
                RequestPath = new PathString("/images")
            });
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            { 
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Start project");
                }); 
            });
        }
    }
}
