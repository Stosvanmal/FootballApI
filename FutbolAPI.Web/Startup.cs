using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FutbolAPI.Business.API;
using FutbolAPI.Business.Models;
using FutbolAPI.Business.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FutbolAPI.Web
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
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddDbContext<FutbolAPIContext>(options => 
                            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            services.AddScoped<IRepository<Manager>, Repository<Manager>>();
            services.AddScoped<IRepository<Player>, Repository<Player>>();
            services.AddScoped<IRepository<Match>, Repository<Match>>();
            services.AddScoped<IRepository<Referee>, Repository<Referee>>();
            services.AddScoped<IRepository<MatchPlayerAway>, Repository<MatchPlayerAway>>();
            services.AddScoped<IRepository<MatchPlayerHome>, Repository<MatchPlayerHome>>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IManagerAPI, ManagerAPI>();
            services.AddScoped<IPlayerAPI, PlayerAPI>();
            services.AddScoped<IMatchAPI, MatchAPI>();
            services.AddScoped<IRefereeAPI, RefereeAPI>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
