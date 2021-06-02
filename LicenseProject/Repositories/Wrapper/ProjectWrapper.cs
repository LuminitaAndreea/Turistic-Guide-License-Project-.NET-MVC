using LicenseProject.Models;
using LicenseProject.Repositories;
using LicenseProject.Repositories.Interfaces;
using LicenseProject.Wrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Wrapper
{
    public class ProjectWrapper : IProjectWrapper
    {
        private Context _context;
        private IRestaurantRepository _restaurant;
        private ITuristicObjectRepository _turisticObject;
        private ICategoryRepository _category;
        private IReviewRepository _review;
        private IRestaurantCategoryRepository _restaurantCategory;
        private ITuristicObjectCategoryRepository _turisticObjectCategory;
        public ProjectWrapper(Context context)
        {
            _context = context;
        }


        public IRestaurantRepository Restaurant
        {
            get
            {
                if (_restaurant == null)
                {
                    _restaurant = new RestaurantRepository(_context);
                }

                return _restaurant;
            }
        }

        public ITuristicObjectRepository TuristicObject
        {
            get
            {
                if (_turisticObject == null)
                {
                    _turisticObject = new TuristicObjectRepository(_context);
                }

                return _turisticObject;
            }
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_context);
                }

                return _category;
            }
        }

        public IReviewRepository Review
        {
            get
            {
                if (_review == null)
                {
                    _review = new ReviewRepository(_context);
                }

                return _review;
            }
        }

        public IRestaurantCategoryRepository RestaurantCategory 
        {
            get
            {
                if (_restaurantCategory == null)
                {
                    _restaurantCategory = new RestaurantCategoryRepository(_context);
                }

                return _restaurantCategory;
            }
        }

        public ITuristicObjectCategoryRepository TuristicObjectCategory 
        {
            get
            {
                if (_turisticObjectCategory == null)
                {
                    _turisticObjectCategory = new TuristicObjectCategoryRepository(_context);
                }

                return _turisticObjectCategory;
            }

        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
