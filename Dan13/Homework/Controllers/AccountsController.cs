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
    public class AccountsController : ApiController
    {
        private DataAccessContext db = new DataAccessContext();

        // GET: api/SocialAccounts
        public IQueryable<Account> GetSocialAccounts()
        {
            return db.Accounts;
        }

        // GET: api/SocialAccounts/5
        [ResponseType(typeof(Account))]
        public IHttpActionResult GetSocialAccount(int id)
        {
            Account socialAccount = db.Accounts.Find(id);
            if (socialAccount == null)
            {
                return NotFound();
            }

            return Ok(socialAccount);
        }

        // PUT: api/SocialAccounts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSocialAccount(int id, Account socialAccount)
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
        [ResponseType(typeof(Account))]
        public IHttpActionResult PostSocialAccount(Account socialAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Accounts.Add(socialAccount);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = socialAccount.Id }, socialAccount);
        }

        // DELETE: api/SocialAccounts/5
        [ResponseType(typeof(Account))]
        public IHttpActionResult DeleteSocialAccount(int id)
        {
            Account socialAccount = db.Accounts.Find(id);
            if (socialAccount == null)
            {
                return NotFound();
            }

            db.Accounts.Remove(socialAccount);
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
            return db.Accounts.Count(e => e.Id == id) > 0;
        }
    }
}