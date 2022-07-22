using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;


namespace APITransferencias.Models
{
    public class BankRepository : IBankRepository
    {

        APIContext context;

        public BankRepository(APIContext apicontext)
        {
            this.context = apicontext;
        }


        public async Task<IEnumerable<Bank>> getAllBanks()
        {

            return context.Banks;
        }


        //public async Task<Bank> getBankbyCode(string codigo_banco)
        public async Task<Bank> getBankbyCode(string codigo_banco)
        {
            var request = context.Banks.Find(codigo_banco);

            return request;
        }

        public async Task<bool> createBank(Bank bankInfo)
        {

            var validation = context.Banks.Find(bankInfo.codigo_banco);

            if (validation != null) throw new Exception("Código de banco ya existe");

            var newBank = context.Banks.Add(bankInfo);

            await context.SaveChangesAsync();

            return true;
        }



        public async Task<bool> deleteBank(Bank bankInfo)
        {
            

            var bank = context.Banks.Find(bankInfo.codigo_banco);

            if(bank == null)
            {
                throw new Exception("Banco no existe");
            }

            context.Banks.Remove(bank);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> updateBank(Bank bankInfo)
        {
            var bank = context.Banks.Find(bankInfo.codigo_banco);

            if (bank == null) throw new Exception("Código de banco inválido o no existe");

            bank.nombre_banco = bankInfo.nombre_banco;
            bank.direccion = bankInfo.direccion;

            await context.SaveChangesAsync();

            return true;
        }
    }
}
