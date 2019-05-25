using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.Controllers
{
    public class CategoryController : ApiController
    {
        private List<CategoryModel> GetDB()
        {
            return new List<CategoryModel>
            {
                new CategoryModel
                {
                    id = 11,
                    categoryName = "vehicles",
                    categoryDescription = "automotive vehicles, motorcycles, trucks"
                },
                new CategoryModel
                {
                    id = 21,
                    categoryName = "books",
                    categoryDescription = "novels, non-fiction, poetry"
                },
                new CategoryModel
                {
                    id = 31,
                    categoryName = "food",
                    categoryDescription = "pizza, burgers, grilled cheese"
                }
            };
        }

        [Route("project/categories")]
        [HttpGet]
        public List<CategoryModel> GetCategories()
        {
            return GetDB();
        }

        [Route("project/categories")]
        [HttpPost]
        public CategoryModel PostNewCategory([FromBody]CategoryModel category)
        {
            List<CategoryModel> categories = GetDB();
            categories.Add(new CategoryModel
            {
                id = category.id,
                categoryName = category.categoryName,
                categoryDescription = category.categoryDescription
            });

            return categories.Find(x => x.id == category.id);
        }

        // Zadatak 2.5
        [Route("project/categories/{id}")]
        [HttpPut]
        public CategoryModel PutUpdateCategory(int id, [FromBody]CategoryModel category)
        {
            List<CategoryModel> categories = GetDB();
            CategoryModel cm = categories.Find(x => x.id == id);
            if (cm != null)
            {
                if (category.categoryName != null)
                {
                    cm.categoryName = category.categoryName;
                }
                if (category.categoryDescription != null)
                {
                    cm.categoryDescription = category.categoryDescription;
                }
            }
            return cm;
        }

        // Zadatak 2.6
        [Route("project/categories/{id}")]
        [HttpDelete]
        public CategoryModel DeleteCategory(int id)
        {
            CategoryModel cm = GetDB().Find(x => x.id == id);
            if (cm != null)
            {
                GetDB().Remove(cm);
            }
            return cm;
        }

        // Zadatak 2.7
        [Route("project/categories/{id}")]
        [HttpGet]
        public CategoryModel GetCategory(int id)
        {
            return GetDB().Find(x => x.id == id);
        }

        // Zadatak 2.8

    
    }
}
