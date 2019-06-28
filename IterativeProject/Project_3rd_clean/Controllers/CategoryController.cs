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
using Project_3rd_clean.Models;
using Project_3rd_clean.Repositories;
using Project_3rd_clean.Services;

namespace Project_3rd_clean.Controllers
{
    public class CategoryController : ApiController
    {
        private IUnitOfWork db;
        private ICategoriesService categoryService;
        private IBillsService billService;
        private IOffersService offerService;

        public CategoryController(IUnitOfWork db, ICategoriesService categoryService, IBillsService billService, IOffersService offerService)
        {
            this.db = db;
            this.categoryService = categoryService;
            this.billService = billService;
            this.offerService = offerService;
        }

        // GET: project/categories
        // ZADATAK 2.2.3
        [Route("project/categories")]
        [HttpGet]
        public IEnumerable<Category> GetcategoryModels()
        {
            return categoryService.GetAllCategories();
        }

        // GET: project/categories/4
        // ZADATAK 2.2.7
        [Route("project/categories/{id}", Name = "SingleCategoryById")]
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategoryModel(int id)
        {
            Category categoryModel = categoryService.GetCategory(id);
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
        public IHttpActionResult PutCategoryModel(int id, Category categoryModel)
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
            Category updatedCategory = categoryService.UpdateCategory(categoryModel);

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
        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategoryModel(Category categoryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Category createdCategory = categoryService.CreateCategory(categoryModel);

            // THIS IS IMPORTANT!!! YOU NEED THE CREATED ID, use the fetched entity, not the provided one...
            return CreatedAtRoute("SingleCategoryById", new { id = createdCategory.id }, createdCategory);
        }

        // DELETE: project/categories
        // ZADATAK: 2.2.6
        [Route("project/categories/{id}")]
        [HttpDelete]
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategoryModel(int id)
        {
            // Possible bug here....
            // Ne dozvoliti brisanje ako povezani racuni i ponude nisu istekli
            if (billService.GetBillsByCategoryAndNotExpired(id, DateTime.UtcNow).Count() != 0 ||
                offerService.GetOffersByCategoryAndNotExpired(id, DateTime.UtcNow).Count() != 0)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            Category categoryModel = categoryService.DeleteCategory(id);

            if (categoryModel == null)
            {
                return NotFound();
            }

            return Ok(categoryModel);
        }
    }
}