using LicenseProject.Models;
using System.Collections.Generic;

namespace LicenseProject.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> Categories { get; }

        void Create(Category category);
        void Delete(int? id);
        List<Category> Get();
        Category GetCategoryById(int? id);
        void Update(Category category);
    }
}