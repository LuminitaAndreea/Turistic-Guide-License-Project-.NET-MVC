using LicenseProject.Repositories;
using LicenseProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject.Wrapper.Interfaces
{
    public interface IProjectWrapper
    {
        IRestaurantRepository Restaurant { get; }
        ITuristicObjectRepository TuristicObject { get; }
        ICategoryRepository Category { get; }
        IReviewRepository Review { get; }
        IRestaurantCategoryRepository RestaurantCategory{ get; }
        ITuristicObjectCategoryRepository TuristicObjectCategory { get; }
        void Save();
    }
}
