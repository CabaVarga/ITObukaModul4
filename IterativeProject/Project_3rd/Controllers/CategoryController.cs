using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Project_3rd.Models;
using Project_3rd.Repositories;
using Project_3rd.Services;

namespace Project_3rd.Controllers
{
    public class CategoryController : ApiController
    {
        private IUnitOfWork db;
        private ICategoryService categoryService;

        public CategoryController(IUnitOfWork db, ICategoryService categoryService)
        {
            this.db = db;
            this.categoryService = categoryService;
        }

        // GET: project/categories
        // ZADATAK 2.2.3
        [Route("project/categories")]
        [HttpGet]
        public IEnumerable<CategoryModel> GetcategoryModels()
        {
            return categoryService.GetAllCategories();
        }

        // GET: project/categories/4
        // ZADATAK 2.2.7
        [Route("project/categories/{id}", Name = "SingleCategoryById")]
        [ResponseType(typeof(CategoryModel))]
        public IHttpActionResult GetCategoryModel(int id)
        {
            CategoryModel categoryModel = categoryService.GetCategory(id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            return Ok(categoryModel);
        }

        // PUT: project/categories/4
        // ZADATAK 2.2.5
        [Route("project/categories/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategoryModel(int id, CategoryModel categoryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Id check in controller, again
            if (id != categoryModel.id)
            {
                return BadRequest();
            }

            // The following code is flawed
            CategoryModel updatedCategory = categoryService.UpdateCategory(categoryModel);

            if (updatedCategory == null)
            {
                return NotFound();
            }

            return Ok(updatedCategory);

            // return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: project/categories
        // ZADATAK: 2.2.4
        [Route("project/categories")]
        [HttpPost]
        [ResponseType(typeof(CategoryModel))]
        public IHttpActionResult PostCategoryModel(CategoryModel categoryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CategoryModel createdCategory = categoryService.CreateCategory(categoryModel);

            // THIS IS IMPORTANT!!! YOU NEED THE CREATED ID, use the fetched entity, not the provided one...
            return CreatedAtRoute("SingleCategoryById", new { id = createdCategory.id }, createdCategory);
        }

        // DELETE: project/categories
        // ZADATAK: 2.2.6
        [Route("project/categories/{id}")]
        [HttpDelete]
        [ResponseType(typeof(CategoryModel))]
        public IHttpActionResult DeleteCategoryModel(int id)
        {
            CategoryModel categoryModel = categoryService.DeleteCategory(id);

            if (categoryModel == null)
            {
                return NotFound();
            }

            return Ok(categoryModel);
        }
    }
}