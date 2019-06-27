using Project_3rd.Models;
using Project_3rd.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd.Services
{
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork db;

        public CategoryService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<CategoryModel> GetAllCategories()
        {
            return db.CategoriesRepository.Get();
        }

        public CategoryModel GetCategory(int id)
        {
            return db.CategoriesRepository.GetByID(id);
        }

        public CategoryModel CreateCategory(CategoryModel category)
        {
            db.CategoriesRepository.Insert(category);
            db.Save();

            return category;
        }

        public CategoryModel UpdateCategory(CategoryModel category)
        {
            db.CategoriesRepository.Update(category);
            db.Save();

            return category;
        }

        public CategoryModel DeleteCategory(int id)
        {
            CategoryModel category = db.CategoriesRepository.GetByID(id);

            if (category != null)
            {
                db.CategoriesRepository.Delete(category);
                db.Save();
            }

            return category;
        }
    }
}