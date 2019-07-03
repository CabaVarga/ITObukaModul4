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
using Homework.Models.DTOs.Account;
using Homework.Services;

namespace Homework.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : ApiController
    {
        private IAccountsService accountsService;

        public AccountsController(IAccountsService accountsService)
        {
            this.accountsService = accountsService;
        }

        // GET: api/Accounts
        public IQueryable<Account> GetAccounts()
        {
            return accountsService.GetAllAccounts().AsQueryable();
        }

        #region PPA Get all accounts
        [Route("public")]
        [ResponseType(typeof(PublicAccountDTO))]
        [HttpGet]
        public IHttpActionResult GetAllAccountsPublic()
        {
            return Ok(accountsService.GetAllAccountsPublic());
        }

        [Route("private")]
        [ResponseType(typeof(PublicAccountDTO))]
        [HttpGet]
        public IHttpActionResult GetAllAccountsPrivate()
        {
            return Ok(accountsService.GetAllAccountsPrivate());
        }

        [Route("admin")]
        [ResponseType(typeof(PublicAccountDTO))]
        [HttpGet]
        public IHttpActionResult GetAllAccountsAdmin()
        {
            return Ok(accountsService.GetAllAccountsAdmin());
        }
        #endregion



        // GET: api/Accounts/5
        [ResponseType(typeof(Account))]
        public IHttpActionResult GetAccount(int id)
        {
            Account account = accountsService.GetAccount(id);
            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // PUT: api/Accounts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAccount(int id, Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != account.Id)
            {
                return BadRequest();
            }

            Account updatedAccount = accountsService.UpdateAccount(id, account);

            if (updatedAccount == null)
            {
                return NotFound();
            }

            return Ok(updatedAccount);
        }

        // POST: api/Accounts
        [ResponseType(typeof(Account))]
        public IHttpActionResult PostAccount(Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            accountsService.CreateAccount(account);

            return CreatedAtRoute("DefaultApi", new { id = account.Id }, account);
        }

        // DELETE: api/Accounts/5
        [ResponseType(typeof(Account))]
        public IHttpActionResult DeleteAccount(int id)
        {
            Account account = accountsService.DeleteAccount(id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }
    }
}