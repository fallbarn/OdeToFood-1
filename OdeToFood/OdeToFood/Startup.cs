using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OdeToFood.Data;

namespace OdeToFood
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
            //
            // sle note: dependency injection 
            //

            //
            // Add options to associate with OdeToFoodContext. These are magically passed to the OdeToFoodContext when
            // created by IOC
            //

            // sle note: the AddDbContext acts as an abstract factory and creates both a DbContextOptions type instance and
            // a OdeToFoodDbContext type instance. Here you configure the DbContextOptions instance with the connection string.
            services.AddDbContextPool<OdeToFoodDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("OdeToFoodDb"));
            });

            services.AddScoped<IRestaurantData, SqlRestaurantData>();
            //services.AddSingleton<IRestaurantData, InMemoryRestaurantData>(); // sle note: using in memory

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            // aspnetcore30
            services.AddRazorPages();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // sle note: This section sets up a pipeline of HTTP configuration tasks to call on startup.
            // These are tasks to do between the Webserver starting this webapp and displaying the first webpage.
            // The Request Context is passed from from the first task, thru to the last task. the ' app.Use(SayHelloMiddleware);'
            // sets-up a custom task that is positioned between two standard tasks.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            
            app.Use(SayHelloMiddleware);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseNodeModules();

            // aspnetcore30
            app.UseRouting();            
            app.UseEndpoints(e =>
            {
                e.MapRazorPages();
                e.MapControllers();
            });
        }

        private RequestDelegate SayHelloMiddleware(
                                    RequestDelegate next)
        {
            // sle note: remember, ctx is the parameter of the 'RequestDelegate' that is to be returned. In javascript, it would go like:
            // return async function(ctx) {
            //   if ( ...
            //   else ( ...
            //  }
            // }
            return async ctx =>
            {

                if (ctx.Request.Path.StartsWithSegments("/hello"))
                {
                    await ctx.Response.WriteAsync("Hello, World!");
                }
                else
                {
                    await next(ctx);                    
                }
            };
        }
    }
}
