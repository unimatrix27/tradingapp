using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using trading.Persistence;
using AutoMapper;
using Hangfire;
using NJsonSchema;
using NSwag.AspNetCore;
using trading.Background;
using Serilog;
using Trading.Persistence;
using Trading.Persistence.Interfaces;

namespace WebApplicationBasic
{

    public class ServiceProviderJobActivator : JobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderJobActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override object ActivateJob(Type type)
        {
            return _serviceProvider.GetRequiredService(type);
        }
    }
   
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfire(config => 
                {config.UseSqlServerStorage(Configuration["ConnectionStrings:Default"]);
                //    config.UseDefaultActivator();
                });
            services.AddTransient<YahooDownloader>();
            services.AddTransient<ScreenerDownloader>();
            services.AddTransient<ScreenerParser>();
            services.AddTransient<ScreenerParseScheduler>();
            services.AddTransient<SignalProcessor>();


            services.AddAutoMapper();
            services.AddDbContext<TradingDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Default"]),ServiceLifetime.Transient);
            //services.AddTransient<ITransientTradingDbContext, TradingDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Default"]));
            services.AddTransient<IUnitOfWork,UnitOfWork>();
            // Add framework services.
            services.AddMvc().AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            ); 

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddSerilog("\\\\192.168.2.2\\familie\\07_Trading\\001_Core\\Logs\\trading_{Date}.txt");
            //loggerFactory.AddDebug();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel
                .Information()
                .WriteTo.Seq("http://localhost:5341/")
                .CreateLogger();

            GlobalConfiguration.Configuration.UseActivator(new ServiceProviderJobActivator(app.ApplicationServices));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            var options = new BackgroundJobServerOptions { WorkerCount = 1 };
            app.UseStaticFiles();
            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly,new SwaggerUiSettings{DefaultPropertyNameHandling = PropertyNameHandling.CamelCase});
            app.UseHangfireServer(options);
            app.UseHangfireDashboard();

            //BackgroundJob.Enqueue<YahooDownloader>(x => x.Download(JobCancellationToken.Null));
            RecurringJob.AddOrUpdate<YahooDownloader>(x => x.Download(JobCancellationToken.Null), Cron.MinuteInterval(180));
            RecurringJob.AddOrUpdate<ScreenerDownloader>(x => x.Download(JobCancellationToken.Null), Cron.MinuteInterval(15));
            BackgroundJob.Schedule<ScreenerParseScheduler>(x => x.Schedule(JobCancellationToken.Null),TimeSpan.FromSeconds(1));
            RecurringJob.AddOrUpdate<SignalProcessor>(x => x.ProcessAllSignals(JobCancellationToken.Null), Cron.MinuteInterval(15));



            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

        }
    }
}
