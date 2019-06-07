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
using Projekat.Models;

namespace Projekat.Controllers
{
    public class CategoryController : ApiController
    {
        private DataAccessContext db = new DataAccessContext();

        // GET: project/categories
        // ZADATAK 2.2.3
        [Route("project/categories")]
        [HttpGet]
        public IQueryable<CategoryModel> GetcategoryModels()
        {
            return db.categoryModels;
        }

        // GET: project/categories/4
        // ZADATAK 2.2.7
        [Route("project/categories/{id}", Name = "SingleCategoryById")]
        [ResponseType(typeof(CategoryModel))]
        public IHttpActionResult GetCategoryModel(int id)
        {
            CategoryModel categoryModel = db.categoryModels.Find(id);
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

            if (id != categoryModel.id)
            {
                return BadRequest();
            }

            db.Entry(categoryModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
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

            db.categoryModels.Add(categoryModel);
            db.SaveChanges();

            return CreatedAtRoute("SingleCategoryById", new { id = categoryModel.id }, categoryModel);
        }

        // DELETE: project/categories
        // ZADATAK: 2.2.6
        [Route("project/categories/{id}")]
        [HttpDelete]
        [ResponseType(typeof(CategoryModel))]
        public IHttpActionResult DeleteCategoryModel(int id)
        {
            CategoryModel categoryModel = db.categoryModels.Find(id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            db.categoryModels.Remove(categoryModel);
            db.SaveChanges();

            return Ok(categoryModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryModelExists(int id)
        {
            return db.categoryModels.Count(e => e.id == id) > 0;
        }
    }
}