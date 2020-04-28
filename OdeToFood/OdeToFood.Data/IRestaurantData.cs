using OdeToFood.Core;
using System.Collections.Generic;


namespace OdeToFood.Data
{
    // sle note: Used by inmemory database and sql database 
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
