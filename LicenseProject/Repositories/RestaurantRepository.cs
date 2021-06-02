using BookloversApplication.Repositories;
using LicenseProject.Models;
using LicenseProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Repositories
{
    public class RestaurantRepository:RepositoryBase<Restaurant> , IRestaurantRepository
    {
        public RestaurantRepository(Context context) : base(context) { }
    
    }
}
