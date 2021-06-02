using LicenseProject.Models;
using LicenseProject.Wrapper.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IProjectWrapper _wrapper;
        private readonly Context _context;
        public ReviewService(IProjectWrapper wrapper, Context context)
        {
            _wrapper = wrapper;
            _context = context;

        }
        public IEnumerable<Review> Reviews => _context.Reviews.Include(c => c.Restaurant).Include(c => c.TuristicObject).Include(c=>c.ApplicationUser);
        public List<Review> GetAllRestaurants()
        {

            var reviews = _wrapper.Review.GetAll().Include(r => r.Restaurant).Include(c => c.TuristicObject).Include(c => c.ApplicationUser).Where(r => r.Restaurant != null).ToList();
            return reviews;
        }
        public List<Review> GetAllTuristicObjects()
        {
            var reviews=_wrapper.Review.GetAll().Include(r => r.Restaurant).Include(c => c.TuristicObject).Include(c => c.ApplicationUser).Where(r => r.TuristicObject != null).ToList();
            return reviews;
        }
        public Review GetReviewById(int? id)
        {
            return _wrapper.Review.FindByCondition(r => r.ReviewId.Equals(id))
           .FirstOrDefault();
        }

        public void Create(Review review)
        {
            _wrapper.Review.Create(review);
            _wrapper.Save();

        }
        public void Delete(int? id)
        {
            var review = _wrapper.Review.Get().FirstOrDefault(m => m.ReviewId == id);
            _wrapper.Review.Delete(review);
            _wrapper.Save();

        }

        public void Update(Review review)
        {

            _wrapper.Review.Update(review);
            _wrapper.Save();
        }
    }
}
