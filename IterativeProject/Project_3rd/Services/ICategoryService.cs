using Project_3rd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryModel> GetAllCategories();
        CategoryModel GetCategory(int id);
        CategoryModel CreateCategory(CategoryModel category);
        CategoryModel UpdateCategory(CategoryModel category);
        CategoryModel DeleteCategory(int id);
    }
}