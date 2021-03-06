﻿using System;
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
using Projekat.Repositories;

namespace Projekat.Controllers
{
    public class CategoryController : ApiController
    {
        private UnitOfWork db = new UnitOfWork();

        // GET: project/categories
        // ZADATAK 2.2.3
        [Route("project/categories")]
        [HttpGet]
        public IEnumerable<Category> GetcategoryModels()
        {
            return db.CategoryRepository.Get();
        }

        // GET: project/categories/4
        // ZADATAK 2.2.7
        [Route("project/categories/{id}", Name = "SingleCategoryById")]
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategoryModel(int id)
        {
            Category categoryModel = db.CategoryRepository.GetByID(id);
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

            if (id != categoryModel.id)
            {
                return BadRequest();
            }

            db.CategoryRepository.Update(categoryModel);
            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
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

            db.CategoryRepository.Insert(categoryModel);
            db.Save();

            return CreatedAtRoute("SingleCategoryById", new { id = categoryModel.id }, categoryModel);
        }

        // DELETE: project/categories
        // ZADATAK: 2.2.6
        [Route("project/categories/{id}")]
        [HttpDelete]
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategoryModel(int id)
        {
            Category categoryModel = db.CategoryRepository.GetByID(id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            db.CategoryRepository.Delete(categoryModel);
            db.Save();

            return Ok(categoryModel);
        }
    }
}