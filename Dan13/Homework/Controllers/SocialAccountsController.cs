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
using Homework.Models;

namespace Homework.Controllers
{
    public class SocialAccountsController : ApiController
    {
        private DataAccessContext db = new DataAccessContext();

        // GET: api/SocialAccounts
        public IQueryable<SocialAccount> GetSocialAccounts()
        {
            return db.SocialAccounts;
        }

        // GET: api/SocialAccounts/5
        [ResponseType(typeof(SocialAccount))]
        public IHttpActionResult GetSocialAccount(int id)
        {
            SocialAccount socialAccount = db.SocialAccounts.Find(id);
            if (socialAccount == null)
            {
                return NotFound();
            }

            return Ok(socialAccount);
        }

        // PUT: api/SocialAccounts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSocialAccount(int id, SocialAccount socialAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != socialAccount.Id)
            {
                return BadRequest();
            }

            db.Entry(socialAccount).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocialAccountExists(id))
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

        // POST: api/SocialAccounts
        [ResponseType(typeof(SocialAccount))]
        public IHttpActionResult PostSocialAccount(SocialAccount socialAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SocialAccounts.Add(socialAccount);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = socialAccount.Id }, socialAccount);
        }

        // DELETE: api/SocialAccounts/5
        [ResponseType(typeof(SocialAccount))]
        public IHttpActionResult DeleteSocialAccount(int id)
        {
            SocialAccount socialAccount = db.SocialAccounts.Find(id);
            if (socialAccount == null)
            {
                return NotFound();
            }

            db.SocialAccounts.Remove(socialAccount);
            db.SaveChanges();

            return Ok(socialAccount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SocialAccountExists(int id)
        {
            return db.SocialAccounts.Count(e => e.Id == id) > 0;
        }
    }
}