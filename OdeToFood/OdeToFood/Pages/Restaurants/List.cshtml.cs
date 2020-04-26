using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IRestaurantData restaurantData;
        private readonly ILogger<ListModel> logger;

        public string Message { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }

        /*
         * sle note: Binds the html button, whose name = 'SearchTerm' to this attribute
         * Another way is to use the httpcontext.Request.QueryString in the OnGet() and assign to attribute
         * Also, as a parameter to OnGet() (as in MVC 1.0) works with the 'asp-for' binder in the razor page.
         */
        
        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }
        /*
         * sle note: Dependency injection is built into .NET Core.
         * 
         * The extensions: IConfiguration and ILogger come for free, give access to system.diagnostic (I think) and the Appsetting.json.
         * The IRestaurantData type is part of this solution. It finds its way into the constructore through: - Configure Services method
         *    //services.AddScoped<IRestaurantData, SqlRestaurantData>(); in the Startup.cs
         *    
         * sle note: classes passed in through the constructor are known as Services. They are normally then placed
         * into local attributes for general referencing (via @Model.xx on the page)
         */
        public ListModel(IConfiguration config, 
                         IRestaurantData restaurantData,
                         ILogger<ListModel> logger)
        {
            this.config = config;
            this.restaurantData = restaurantData;
            this.logger = logger;
        }

        /*
         * sle note: the .net.core equivalent of the OnLoad() method in asp.net.
         * This C# method is called when the page is loaded.
         * The database is called to provide a list of resturants to populate the 
         * Grid. Defaults to and empty search term attribute.
         */
        public void OnGet()
        {
            logger.LogError("Executing ListModel");
            Message = config["Message"];
            Restaurants = restaurantData.GetRestaurantsByName(SearchTerm);
        }
    }
}