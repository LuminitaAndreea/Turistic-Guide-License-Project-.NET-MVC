using LicenseProject.Models;
using LicenseProject.Wrapper.Interfaces;
using Microsoft.EntityFrameworkCore;
using NReco.CF.Taste.Impl.Common;
using NReco.CF.Taste.Impl.Model;
using NReco.CF.Taste.Impl.Neighborhood;
using NReco.CF.Taste.Impl.Recommender;
using NReco.CF.Taste.Impl.Similarity;
using NReco.CF.Taste.Model;
using NReco.CF.Taste.Neighborhood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Services
{
    public class RestaurantRecommenderService : IRestaurantRecommenderService
    {
        private readonly IProjectWrapper _wrapper;
        private readonly IReviewService _review;
        private readonly Context _context;

        public RestaurantRecommenderService(IProjectWrapper wrapper, Context context,IReviewService review)
        {
            _wrapper = wrapper;
            _context = context;
            _review = review;

        }

        public List<int> GetRestaurantsRecommendations(int userId)
        {
            GenericDataModel model = GetUserBasedDataModel();

            EuclideanDistanceSimilarity similarity = new EuclideanDistanceSimilarity(
                model);

            IUserNeighborhood neighborhood = new NearestNUserNeighborhood(
                15, similarity, model);

            long[] neighbors = neighborhood.GetUserNeighborhood(userId);

            var recommender =
                new GenericUserBasedRecommender(model, neighborhood, similarity);
            var recommendedItems = recommender.Recommend(userId, 1);

            var RestaurantIds = recommendedItems.Select(ri => (int)ri.GetItemID()).ToList();

            return RestaurantIds;
        }
        public List<Restaurant> GetRestaurantsRecommended(List<int> restaurantIds)
        {
            return (from Restaurant in _wrapper.Restaurant.Get()
                    where restaurantIds.Contains(Restaurant.RestaurantId)
                    select new Restaurant
                    {
                        Name = Restaurant.Name,
                        PriceRange = Restaurant.PriceRange,
                        Program = Restaurant.Program,
                        Url = Restaurant.Url,
                        TelephoneNumber=Restaurant.TelephoneNumber,
                        ExactLocation=Restaurant.ExactLocation,
                        City=Restaurant.City,
                        Country=Restaurant.Country,
                        MainImage=Restaurant.MainImage,
                        Image1=Restaurant.Image1,
                        Image2=Restaurant.Image2,
                        Image3=Restaurant.Image3,
                        Image4=Restaurant.Image4,
                        Image5=Restaurant.Image5,
                        AverageRating=Restaurant.AverageRating,
                        Reviews=Restaurant.Reviews,
                        RestaurantsCategories=Restaurant.RestaurantsCategories
                    }).ToList();
        }
        public long[] GetNearestNeighborsUsersRecommendations(int numNeighbours, int userId)
        {
            GenericDataModel model = GetUserBasedDataModel();

            EuclideanDistanceSimilarity similarity = new EuclideanDistanceSimilarity(
                model);

            IUserNeighborhood neighborhood = new NearestNUserNeighborhood(
                20, similarity, model);

            long[] neighbors = neighborhood.GetUserNeighborhood(userId);

            var recommender =
                new GenericUserBasedRecommender(model, neighborhood, similarity);
            var recommendedItems = recommender.Recommend(userId, 1);

            return neighbors;
        }


        public GenericDataModel GetItemBasedDataModel()
        {
            FastByIDMap<IPreferenceArray> data = new FastByIDMap<IPreferenceArray>();

            IEnumerable<Review> allRestaurantReviews = _review.GetAllRestaurants();

            var everyReviewsRestaurantId = allRestaurantReviews.GroupBy(b => b.Restaurant.RestaurantId).Select(x => x.Key).ToList();


            foreach (int RestaurantId in everyReviewsRestaurantId)
            {
                List<Review> RestaurantReviewsForARestaurant = (from userReviews in allRestaurantReviews
                                                                where userReviews.Restaurant.RestaurantId == RestaurantId
                                                                select userReviews).ToList();
                List<IPreference> listOfPreferences = new List<IPreference>();

                foreach (Review review in RestaurantReviewsForARestaurant)
                {
                    int rating = review.Rate;
                    int reviewUserId = review.ApplicationUser.Id;
                    GenericPreference pref = new GenericPreference(reviewUserId, RestaurantId, rating); /// userId,  itemid, valueId

                    listOfPreferences.Add(pref);
                }

                GenericItemPreferenceArray dataArray = new GenericItemPreferenceArray(listOfPreferences);
                data.Put(RestaurantId, dataArray);
            }

            return new GenericDataModel(data);
        }
        public GenericDataModel GetUserBasedDataModel()
        {
            FastByIDMap<IPreferenceArray> data = new FastByIDMap<IPreferenceArray>();

            IEnumerable<Review> allRestaurantReviews = _review.GetAllRestaurants();

            var everyReviewsUserId = allRestaurantReviews.GroupBy(b => b.ApplicationUser.Id).Select(x => x.Key).ToList();

            foreach (int userId in everyReviewsUserId)
            {
                List<Review> RestaurantReviewsForARestaurant = (from userReviews in allRestaurantReviews
                                                          where userReviews.ApplicationUser.Id == userId
                                                          select userReviews).ToList();
                List<IPreference> listOfPreferences = new List<IPreference>();

                foreach (Review review in RestaurantReviewsForARestaurant)
                {
                    if (review.Restaurant != null)
                    {
                        int rating = review.Rate;
                        int RestaurantId = review.Restaurant.RestaurantId;
                        GenericPreference pref = new GenericPreference(userId, RestaurantId, rating);
                        listOfPreferences.Add(pref);
                    }
                    else continue;
                }

                GenericUserPreferenceArray dataArray = new GenericUserPreferenceArray(listOfPreferences);
                data.Put(userId, dataArray);
            }

            return new GenericDataModel(data);
        }
        
        public List<Restaurant> GetRestaurantsNotReviewed(int userId, List<Restaurant> restaurants)
        {
            List<Restaurant> RestaurantsNotReviewedByUser = new List<Restaurant>();
            List<int> userRestaurantReviewsRestaurantIdList = (from reviews in _review.GetAllRestaurants() where reviews.ApplicationUser.Id == userId select reviews.Restaurant.RestaurantId).ToList();

            foreach (Restaurant restaurant in restaurants)
            {
                int RestaurantId = restaurant.RestaurantId;

                if (!userRestaurantReviewsRestaurantIdList.Contains(RestaurantId))
                    RestaurantsNotReviewedByUser.Add(restaurant);
            }

            return RestaurantsNotReviewedByUser;
        }
    }
}
