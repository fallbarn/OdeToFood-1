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
            // sle note: dependency injection (info is injected throught a razor pages constructor, then into a local attribute, then via @Model.myAttribute on to the cshtml page
            //

            //
            // Add options to associate with OdeToFoodContext. These are magically passed to the OdeToFoodContext when
            // created by IOC
            //

            // sle note: the AddDbContext acts as an abstract factory and creates both a DbContextOptions type instance and
            // an OdeToFoodDbContext type instance. Here you configure the DbContextOptions instance with the connection string.
            services.AddDbContextPool<OdeToFoodDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("OdeToFoodDb"));
            });

            // sle note SqlRestaurant is added to injection dependency, which in turn instantiates OdeToFoodDbContect just above.
            // This is what is actually specified in the startup.cs 
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
            // SLE note: only called at startup to set up the pipeline

            // Remember the bi-directional Summer Corn analogy: this section sets up a pipeline of HTTP configuration tasks to call on startup.
            // These are tasks to undertake between the Webserver starting and displaying the first webpage.
            // The Request Context maybe passed from from the first task, thru to the last task. 
            // Each task can decide if it goes to the next task.
            // Each task, if it gets called, can: - 1. Do something, then return to previous; 2. Do something, go to next, return to previous; 3. Do someting, go to next, Do something, return to previous
            
            // The ' app.Use(SayHelloMiddleware);'
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
                    await ctx.Response.WriteAsync("SLE Test - Before: Hello Middleware");
                }
                else
                {
                    await next(ctx);

                    await ctx.Response.WriteAsync("SLE Test - After: Hello Middleware!");
                }
            };
        }
    }
}
