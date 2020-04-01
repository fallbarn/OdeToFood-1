using Microsoft.AspNetCore.Mvc;
using OdeToFood.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.ViewComponents
{
    //
    // sle note: this component is used in _Layout.cshtml. In BuildUserInterface::Rendering a view compontent.
    // It is used '<vc:restaurant-count></vc:restaurant-count>' like so, in special VS HTML. 
    // Note: K-bob case 'restaurant-Count',
    // which VS will look for in ~OdeToFood/Pages/Shared/Component/'RestaurantCount'. A VisStud 'BY CONVENTION' mechanism;
    // _ViewImports.cshtml must have @addTagHelper *, OdeToFood
    //
    public class RestaurantCountViewComponent
         : ViewComponent
    {
        private readonly IRestaurantData restaurantData;

        public RestaurantCountViewComponent(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        public IViewComponentResult Invoke()
        {
            var count = restaurantData.GetCountOfRestaurants();
            return View(count);
        }

    }
}
