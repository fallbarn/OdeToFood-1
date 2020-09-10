using OdeToFood.Core;
using System.Collections.Generic;


namespace OdeToFood.Data
{
    // sle note: Used by in memory database and sql database 

    // sle note: the term: 'build out some extractions'
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int id);
        Restaurant Update(Restaurant updatedRestaurant);
        Restaurant Add(Restaurant newRestaurant);
        Restaurant Delete(int id);
        int GetCountOfRestaurants();
        int Commit();
    }  
}
