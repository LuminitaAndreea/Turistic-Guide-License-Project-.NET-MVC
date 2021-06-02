using LicenseProject.Models;
using NReco.CF.Taste.Impl.Model;
using System.Collections.Generic;

namespace LicenseProject.Services
{
    public interface IRestaurantRecommenderService
    {
        List<Restaurant> GetRestaurantsNotReviewed(int userId, List<Restaurant> restaurants);
        public List<Restaurant> GetRestaurantsRecommended(List<int> restaurantIds);
        GenericDataModel GetItemBasedDataModel();
        long[] GetNearestNeighborsUsersRecommendations(int numNeighbours, int userId);
        List<int> GetRestaurantsRecommendations(int userId);
        GenericDataModel GetUserBasedDataModel();
    }
}