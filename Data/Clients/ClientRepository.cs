using Npgsql;
using Microsoft.EntityFrameworkCore;
using FluentValidation.Results;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITransferencias.Models
{
    public class ClientRepository : IClientRepository

    {
        private APIContext context;

        public ClientRepository(APIContext apiContext)
        {
            context = apiContext;
        }

        public async Task<IEnumerable<Client>> getAllClients()
        {
            return context.Clients;
        }

        public async Task<bool> createClient(Client clientParam)
        {

            var request = context.Clients.Find(clientParam.cedula);

            if (request != null)
            {
                throw new Exception("Cliente ya existe");
            }
            context.Add(clientParam);

            await context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> deleteClient(string cedula)
        {
         
            var client = context.Clients.Find(cedula);

            if(client == null)
            {
                throw new Exception("Cliente no existe");
            }

            var request = context.Clients.Remove(client);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<Client> getClientbyCedula(string cedula_cliente)
        {   
            var request = context.Clients.Find(cedula_cliente);

            if(request == null)
            {
                throw new Exception("Cliente no existe");
            }
   
            return request;
        }

        public async Task<bool> updateClient(Client clientParam)
        {

            var client = context.Clients.Find(clientParam.cedula);

            if (client == null)
            {
                throw new Exception("Cliente no existe");
            } 
            
            client.tipo_doc = clientParam.tipo_doc;
            client.nombre_apellido = clientParam.nombre_apellido;

            await context.SaveChangesAsync();

            return true;
        }
    }
}
