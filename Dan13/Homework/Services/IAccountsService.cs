using Homework.Models;
using Homework.Models.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.Services
{
    public interface IAccountsService
    {
        #region CRUD
        IEnumerable<Account> GetAllAccounts();

        Account GetAccount(int id);

        Account CreateAccount(Account account);

        Account UpdateAccount(int id, Account account);

        Account DeleteAccount(int id);
        #endregion

        IEnumerable<PublicAccountDTO> GetAllAccountsPublic();
        IEnumerable<PrivateAccountDTO> GetAllAccountsPrivate();
        IEnumerable<AdminAccountDTO> GetAllAccountsAdmin();
    }
}