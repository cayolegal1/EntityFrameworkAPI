using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;


namespace APITransferencias.Models;

public class AccountRepository : IAccountRepository
{


    private APIContext context;

    public AccountRepository(APIContext apicontext)
    {
        this.context = apicontext;
    }

    public async Task<IEnumerable<Account>> getAllAccounts()
    {

        return context.Accounts;
    }


    public async Task<Account> getAccountByID(string id)
    {

        var account =  context.Accounts.Find(id);

        return account;
    }

    public async Task<bool> createAccount(Account accountInfo)
    {

        var account = context.Accounts.Find(accountInfo.num_cta);

        if (account != null) throw new Exception("Cuenta ya existente");

        var newAccount = context.Accounts.Add(accountInfo);

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> deleteAccount(Account accountInfo)
    {

       

        return 1 > 0;

    }


    public async Task<bool> updateAccount(Account accountInfo)
    {

        

        return 1 > 0;
    }

    //public async Task<Account> searchAccountByNumber(string num_cta)
    //{
    //    var db = dbConnection();

    //    string query = @"SELECT * FROM cuentas WHERE num_cta = @num_cta";

    //    var response = await db.QueryFirstOrDefaultAsync<Account>(query, new { num_cta });

    //    return response;
    //}

}
