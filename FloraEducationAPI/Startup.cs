using FloraEducationAPI.Context;
using FloraEducationAPI.Repository.Implementations;
using FloraEducationAPI.Repository.Interfaces;
using FloraEducationAPI.Service.Implementations;
using FloraEducationAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloraEducationAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Database connection
            //$"Host=ec2-34-246-86-78.eu-west-1.compute.amazonaws.com;Database=d3s6emd1cr2pkl;Username=blnbhlkjtaexph;Password=63bfac8895daee59c9a8482c2633f198e67b4d6b4a4a30b9383af970376989fb;SSL Mode=Require;Trust Server Certificate=true";

            var dbConnectionString = string.Empty;

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                dbConnectionString = Configuration.GetConnectionString("FloraEducationApiDatabase");
            }
            else
            {
                // Use connection string provided at runtime by Heroku.
                var connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
                //var connectionUrl = "postgres://blnbhlkjtaexph:63bfac8895daee59c9a8482c2633f198e67b4d6b4a4a30b9383af970376989fb@ec2-34-246-86-78.eu-west-1.compute.amazonaws.com:5432/d3s6emd1cr2pkl";

                connectionUrl = connectionUrl.Replace("postgres://", string.Empty);
                var userPassSide = connectionUrl.Split("@")[0];
                var hostSide = connectionUrl.Split("@")[1];

                var user = userPassSide.Split(":")[0];
                var password = userPassSide.Split(":")[1];
                var host = hostSide.Split("/")[0];
                var database = hostSide.Split("/")[1].Split("?")[0];

                dbConnectionString = $"Host={host};Database={database};Username={user};Password={password};SSL Mode=Require;Trust Server Certificate=true";
            }

            services.AddDbContext<FloraEducationDbContext>(options => options.UseNpgsql(dbConnectionString));

            // Repositories 
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IMiniQuizRepository, MiniQuizRepository>();
            services.AddScoped<IPlantRepository, PlantRepository>();


            // Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPlantService, PlantService>();
            services.AddTransient<IMiniQuizService, MiniQuizService>();
            services.AddTransient<IBadgeService, BadgeService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IUserLikedPlantsService, UserLikedPlantsService>();

            // JSON config
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // CORS
            services.AddCors();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                                           ForwardedHeaders.XForwardedProto;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseForwardedHeaders();

            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
