using Project_3rd_clean.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd_clean.Services
{
    public interface ICategoriesService
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategory(int id);
        Category CreateCategory(Category category);
        Category UpdateCategory(Category category);
        Category DeleteCategory(int id);
    }
}