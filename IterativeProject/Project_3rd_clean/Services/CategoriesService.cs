using Project_3rd_clean.Models;
using Project_3rd_clean.Models.DTOs.Category;
using Project_3rd_clean.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd_clean.Services
{
    public class CategoriesService : ICategoriesService
    {
        private IUnitOfWork db;

        public CategoriesService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return db.CategoriesRepository.Get();
        }

        public Category GetCategory(int id)
        {
            return db.CategoriesRepository.GetByID(id);
        }

        public Category CreateCategory(Category category)
        {
            db.CategoriesRepository.Insert(category);
            db.Save();

            return category;
        }

        public Category UpdateCategory(Category category)
        {
            db.CategoriesRepository.Update(category);
            db.Save();

            return category;
        }

        public Category DeleteCategory(int id)
        {
            Category category = db.CategoriesRepository.GetByID(id);

            // 2.1 Ne dozvoliti brisanje onih kategorija za koje postoje neistekle ponude i racuni

            // da li ovde ili u kontroleru????

            if (category != null)
            {
                db.CategoriesRepository.Delete(category);
                db.Save();
            }

            return category;
        }

        public IEnumerable<PublicCategoryDTO> GetAllCategoriesPublic()
        {
            return db.CategoriesRepository.Get()
                .Select(c =>
                {
                    return new PublicCategoryDTO()
                    {
                        Description = c.category_description,
                        Id = c.id,
                        Name = c.category_name
                    };
                });
        }

        public IEnumerable<PrivateCategoryDTO> GetAllCategoriesPrivate()
        {
            return db.CategoriesRepository.Get()
                .Select(c =>
                {
                    return new PrivateCategoryDTO()
                    {
                        Description = c.category_description,
                        Id = c.id,
                        Name = c.category_name
                    };
                });
        }

        public IEnumerable<AdminCategoryDTO> GetAllCategoriesAdmin()
        {
            return db.CategoriesRepository.Get()
                .Select(c =>
                {
                    return new AdminCategoryDTO()
                    {
                        Description = c.category_description,
                        Id = c.id,
                        Name = c.category_name
                    };
                });
        }
    }
}