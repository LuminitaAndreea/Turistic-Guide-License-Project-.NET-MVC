using LicenseProject.Models;
using System.Collections.Generic;

namespace LicenseProject.Services
{
    public interface IRestaurantService
    {
        IEnumerable<Restaurant> Restaurants { get; }
        void Create(Restaurant restaurant);
        void Delete(int? id);
        List<Restaurant> Get();
        Restaurant GetRestaurantById(int? id);
        void Update(Restaurant restaurant);
    }
}