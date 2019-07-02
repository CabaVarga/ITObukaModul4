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
            return db.AccountsRepository.Get();
        }

        public Account GetAccount(int id)
        {
            return db.AccountsRepository.GetByID(id); 
        }

        public Account CreateAccount(Account account)
        {
            db.AccountsRepository.Insert(account);
            db.Save();

            return account;
        }

        public Account UpdateAccount(int id, Account account)
        {
            Account updatedAccount = db.AccountsRepository.GetByID(id);

            if (updatedAccount != null)
            {
                updatedAccount.Description = account.Description;
                updatedAccount.Link = account.Description;
                updatedAccount.Link = account.Link;
                updatedAccount.UserId = account.UserId;

                db.AccountsRepository.Update(updatedAccount);
                db.Save();
            }

            return updatedAccount;
        }

        public Account DeleteAccount(int id)
        {
            Account account = db.AccountsRepository.GetByID(id);

            if (account != null)
            {
                db.AccountsRepository.Delete(account);
                db.Save();
            }

            return account;
        }
    }
}