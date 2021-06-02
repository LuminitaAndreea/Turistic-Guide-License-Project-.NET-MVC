using LicenseProject.Models;
using System.Collections.Generic;

namespace LicenseProject.Services
{
    public interface IReviewService
    {
        IEnumerable<Review> Reviews { get; }

        void Create(Review review);
        void Delete(int? id);
        public List<Review> GetAllRestaurants();
        public List<Review> GetAllTuristicObjects();
        Review GetReviewById(int? id);
        void Update(Review review);
    }
}