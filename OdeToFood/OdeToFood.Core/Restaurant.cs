using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Core
{

    // sle note: define a class to represent a restaurant which will
    // be mapped to a database table by EF.
    public class Restaurant 
    {
        public int Id { get; set; }

        // SLE note: works inconjunction with binding to verifyinput data.
        [Required, StringLength(80)]
        public string Name { get; set; }

        [Required, StringLength(255)]
        public string Location { get; set; }
        public CuisineType Cuisine { get; set; }
    }
}
