using Homework.Models;
using Homework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.Services
{
    public class AccountsService : IAccountsService
    {
        private IUnitOfWork db;

        public AccountsService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public Account GetAccount(int id)
        {
            throw new NotImplementedException();
        }

        public Account CreateAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public Account UpdateAccount(int id, Account account)
        {
            throw new NotImplementedException();
        }

        public Account DeleteAccount(int id)
        {
            throw new NotImplementedException();
        }
    }
}