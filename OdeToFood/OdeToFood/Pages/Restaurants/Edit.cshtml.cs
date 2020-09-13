using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;


        [BindProperty]  // sle note: Incoming Form values are validated via Annotations applied (here) to the Resturant type. (see defintion of Restaurant
        public Restaurant Restaurant { get; set; }

        // sle note: .net core doesn't have drag and drop corresponding server-side types that correspond with the asp control types
        // on the page side. This is done manually by creating them as model attributes and populating them via the inject dependency
        // method via the contructor. Here, the htmlhelper class creates the c# equivalant classes to the clientside asp controls is used.
        public IEnumerable<SelectListItem> Cuisines { get; set; } // sle note: SelectListItem  type is a name value type populated typically from an enum type 

        public EditModel(IRestaurantData restaurantData,
                         IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }

        // sle note: GET verb - the previous ASP OnLoad is split into two (MVC style) methods OnGet() and OnPost()
        // This allows allows (MVC style) unit testing facility.
        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>(); // use htmlhelper to create the c# equivalant classes to the clientside asp controls is used.
            if (restaurantId.HasValue)
            {
                Restaurant = restaurantData.GetById(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }
            if(Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        // sle note: POST verb - the previous ASP OnLoad is split into two (MVC style) methods OnGet() and OnPost()
        // This allows allows (MVC style) unit testing facility.
        public IActionResult OnPost()
        {               
            if(!ModelState.IsValid) // ModelState collects details of all the bindings that go on between the client and the form during the POST verb action. Mainly used here to collect errors
            {                       // Is also uses in the cshtlm 'asp-validation-for' tags to format error information to the html.
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();                
            }

            if (Restaurant.Id > 0)
            {
                restaurantData.Update(Restaurant);
            }
            else
            {
                restaurantData.Add(Restaurant);
            }
            restaurantData.Commit();
            TempData["Message"] = "Restaurant saved!";
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });
        }
    }
}