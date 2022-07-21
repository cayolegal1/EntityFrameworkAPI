using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace APITransferencias.Models
{ 
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> getAllAccounts();

        Task<Account> getAccountByID(string code);

        Task<bool> createAccount(Account accountInfo);

        Task<bool> updateAccount(Account accountInfo);

        Task<bool> deleteAccount(Account accountInfo);
    }
   }
