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

        //Estamos instanciando el driver que nos sirve para conectarnos a postgres, pasandole como argumento 
        //en el constructor(seguramente recibe la conexión directamente al ser instanciada) nuestra variable 
        //global que será asignada cuando instanciemos la clase, y usando el método ConnectionString de NpgsqlConnection.
        //En dicho método estará el ConnectionString que definimos en Program en el método Connect de PostgreSqlConfiguration.
        //En donde este tiene acceso y usa la configuración o las variables de conexión a la base de datos
        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection("Server = 127.0.0.1; Port = 5432; Database = interbanking_transfers; User Id = postgres; Password = hola1606;");
        }


        APIContext context;

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


            //var db = dbConnection();

            //string query = @$"

            // INSERT INTO clientes (cedula, tipo_doc, nombre_apellido)
            // VALUES(@cedula, @tipo_doc, @nombre_apellido)
            // ";


            //var filterClient = await getClientbyCedula(clientParam.cedula);

            //if(filterClient != null)
            //{
            //    throw new Exception("Cliente ya se encuentra registrado");

            //}


            ////para hacer peticiones de tipo POST nos viene bien usar ExecuteAsync. Esto devuelve un int, si todo salió bien 
            ////debemos revolver: response > 0. Ya que, devolverá 1 si se ejecuto correctamente la query o si al menos una 
            ////fila fue afectada
            //var response = await db.ExecuteAsync(query, new { clientParam.cedula, clientParam.tipo_doc, clientParam.nombre_apellido, 
            //});

            var client = context.Clients.Add(clientParam);

            context.SaveChangesAsync();

            return true;

        }

        public async Task<bool> deleteClient(string cedula)
        {
         
            var client = context.Clients.Find(cedula);

            var request = context.Clients.Remove(client);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<Client> getClientbyCedula(string cedula_cliente)
        {
            var request = context.Clients.Find(cedula_cliente);
   
            return request;
        }

        public async Task<bool> updateClient(Client clientParam)
        {
            //var db = dbConnection();

            //string query = @$"

            // UPDATE clientes
            // SET tipo_doc = @tipo_doc,
            // nombre_apellido = @nombre_apellido
            // WHERE cedula = @cedula
            // ";

            //var response = await db.ExecuteAsync(query, new { clientParam.cedula, clientParam.tipo_doc, clientParam.nombre_apellido });

            //return response > 0;

            return true;
        }
    }
}
