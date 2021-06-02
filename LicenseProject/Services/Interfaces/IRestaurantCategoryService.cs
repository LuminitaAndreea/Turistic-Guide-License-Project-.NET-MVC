using LicenseProject.Models;
using System.Collections.Generic;

namespace LicenseProject.Services
{
    public interface IRestaurantCategoryService
    {
        IEnumerable<RestaurantCategory> RestaurantCategories { get; }

        void Create(RestaurantCategory RestaurantCategory);
        void Delete(int? id);
        List<RestaurantCategory> Get();
        RestaurantCategory GetRestaurantCategoryById(int? id);
        void Update(RestaurantCategory RestaurantCategory);
    }
}