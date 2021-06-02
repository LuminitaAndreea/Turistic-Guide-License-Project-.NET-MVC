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
    public class TuristicObjectRecommenderService : ITuristicObjectRecommenderService
    {
        private readonly IProjectWrapper _wrapper;
        private readonly Context _context;
        private readonly IReviewService _review;
        public TuristicObjectRecommenderService(IProjectWrapper wrapper, Context context,IReviewService review)
        {
            _wrapper = wrapper;
            _context = context;
            _review = review;

        }

        public List<int> GetTuristicObjectsRecommendations(int userId)
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

            var TuristicObjectIds = recommendedItems.Select(ri => (int)ri.GetItemID()).ToList();

            return TuristicObjectIds;
        }
        public List<TuristicObject> GetTuristicObjectsRecommended(List<int> TuristicObjectIds)
        {
            return (from TuristicObject in _wrapper.TuristicObject.Get()
                    where TuristicObjectIds.Contains(TuristicObject.TuristicObjectId)
                    select new TuristicObject
                    {
                        Name = TuristicObject.Name,
                        Description = TuristicObject.Description,
                        VisitingPrice = TuristicObject.VisitingPrice,
                        Program = TuristicObject.Program,
                        Url = TuristicObject.Url,
                        TelephoneNumber = TuristicObject.TelephoneNumber,
                        ExactLocation = TuristicObject.ExactLocation,
                        City = TuristicObject.City,
                        Country = TuristicObject.Country,
                        MainImage = TuristicObject.MainImage,
                        Image1 = TuristicObject.Image1,
                        Image2 = TuristicObject.Image2,
                        Image3 = TuristicObject.Image3,
                        Image4 = TuristicObject.Image4,
                        Image5 = TuristicObject.Image5,
                        AverageRating = TuristicObject.AverageRating,
                        Reviews = TuristicObject.Reviews,
                        TuristicObjectsCategories = TuristicObject.TuristicObjectsCategories
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

            IEnumerable<Review> allTuristicObjectReviews = _review.GetAllTuristicObjects();

            var everyReviewsTuristicObjectId = allTuristicObjectReviews.GroupBy(b => b.TuristicObject.TuristicObjectId).Select(x => x.Key).ToList();


            foreach (int TuristicObjectId in everyReviewsTuristicObjectId)
            {
                List<Review> TuristicObjectReviewsForATuristicObject = (from userReviews in allTuristicObjectReviews
                                                                        where userReviews.TuristicObject.TuristicObjectId == TuristicObjectId
                                                                        select userReviews).ToList();
                List<IPreference> listOfPreferences = new List<IPreference>();

                foreach (Review review in TuristicObjectReviewsForATuristicObject)
                {
                    int rating = review.Rate;
                    int reviewUserId = review.ApplicationUser.Id;
                    GenericPreference pref = new GenericPreference(reviewUserId, TuristicObjectId, rating); /// userId,  itemid, valueId

                    listOfPreferences.Add(pref);
                }

                GenericItemPreferenceArray dataArray = new GenericItemPreferenceArray(listOfPreferences);
                data.Put(TuristicObjectId, dataArray);
            }

            return new GenericDataModel(data);
        }
        public GenericDataModel GetUserBasedDataModel()
        {
            FastByIDMap<IPreferenceArray> data = new FastByIDMap<IPreferenceArray>();

            IEnumerable<Review> allTuristicObjectReviews = _review.GetAllTuristicObjects();

            var everyReviewsUserId = allTuristicObjectReviews.GroupBy(b => b.ApplicationUser.Id).Select(x => x.Key).ToList();

            foreach (int userId in everyReviewsUserId)
            {
                List<Review> TuristicObjectReviewsForATuristicObject = (from userReviews in allTuristicObjectReviews
                                                                        where userReviews.ApplicationUser.Id == userId
                                                                        select userReviews).ToList();
                List<IPreference> listOfPreferences = new List<IPreference>();

                foreach (Review review in TuristicObjectReviewsForATuristicObject)
                {
                    if (review.TuristicObject != null)
                    {
                        int rating = review.Rate;
                        int TuristicObjectId = review.TuristicObject.TuristicObjectId;
                        GenericPreference pref = new GenericPreference(userId, TuristicObjectId, rating);

                        listOfPreferences.Add(pref);
                    }
                    else continue;
                }

                GenericUserPreferenceArray dataArray = new GenericUserPreferenceArray(listOfPreferences);
                data.Put(userId, dataArray);
            }

            return new GenericDataModel(data);
        }

        public List<TuristicObject> GetTuristicObjectsNotReviewed(int userId, List<TuristicObject> TuristicObjects)
        {
            List<TuristicObject> TuristicObjectNotReviewedByUser = new List<TuristicObject>();
            List<int> userTuristicObjectReviewsTuristicObjectIdList = (from reviews in _review.GetAllTuristicObjects() where reviews.ApplicationUser.Id == userId select reviews.Restaurant.RestaurantId).ToList();

            foreach (TuristicObject TuristicObject in TuristicObjects)
            {
                int TuristicObjectId = TuristicObject.TuristicObjectId;

                if (!userTuristicObjectReviewsTuristicObjectIdList.Contains(TuristicObjectId))
                    TuristicObjectNotReviewedByUser.Add(TuristicObject);
            }

            return TuristicObjectNotReviewedByUser;
        }
    }
}
