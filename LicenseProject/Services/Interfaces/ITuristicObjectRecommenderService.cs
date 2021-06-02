using LicenseProject.Models;
using NReco.CF.Taste.Impl.Model;
using System.Collections.Generic;

namespace LicenseProject.Services
{
    public interface ITuristicObjectRecommenderService
    {
        GenericDataModel GetItemBasedDataModel();
        long[] GetNearestNeighborsUsersRecommendations(int numNeighbours, int userId);
        List<int> GetTuristicObjectsRecommendations(int userId);
        List<TuristicObject> GetTuristicObjectsNotReviewed(int userId, List<TuristicObject> TuristicObjects);
        List<TuristicObject> GetTuristicObjectsRecommended(List<int> TuristicObjectIds);
        GenericDataModel GetUserBasedDataModel();
    }
}