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
        [RegularExpression("^([A-Z]|[a-z]).*", ErrorMessage = "Must start with a Letter")]
        public string Name { get; set; }

        [Required, StringLength(255)]
        [RegularExpression("^([A-Z]|[a-z]).*", ErrorMessage = "Must start with a Letter")]
        public string Location { get; set; }
        public CuisineType Cuisine { get; set; }
    }
}
