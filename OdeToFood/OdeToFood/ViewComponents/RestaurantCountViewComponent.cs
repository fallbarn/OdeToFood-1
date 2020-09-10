using Microsoft.AspNetCore.Mvc;
using OdeToFood.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.ViewComponents
{
    //
    // sle note: this component is used in _Layout.cshtml. In BuildUserInterface::Rendering a view component.
    // It is used '<vc:restaurant-count></vc:restaurant-count>' like so, in special node helper: VS HTML. 
    // Note: K-bob case 'restaurant-Count',
    // which VS will look for this 'RestaurantCountViewComponent' in ~OdeToFood/Pages/Shared/Component/'RestaurantCount'. A VisStud 'BY CONVENTION' mechanism;
    // _ViewImports.cshtml must have @addTagHelper *, OdeToFood
    //
    public class RestaurantCountViewComponent
         : ViewComponent
    {
        private readonly IRestaurantData restaurantData;

        public RestaurantCountViewComponent(IRestaurantData restaurantData)
        {
            // Data model is passed in via dependecy injection.
            this.restaurantData = restaurantData;
        }

        /*
         * Must be call Invoke 
         */
        public IViewComponentResult Invoke()
        {
            
            var count = restaurantData.GetCountOfRestaurants();

            /*
             * MVC technology. Engine will look for a cshtml page in'Pages\Shared\Component\RestaurantCount' (name of this class minus ViewComponent suffix), and pass 'count' as parameter.
             */
            //return View(count); // Looks for a csthml file call Default.cshtml
            return View("SimonCount", count);
        }

    }
}
