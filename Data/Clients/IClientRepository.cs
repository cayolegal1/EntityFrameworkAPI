﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITransferencias.Models
{
    public interface IClientRepository
    {
        //Task se utiliza para métodos asíncronos. IEnumerable es para devolver varios elementos en un Task
        Task<IEnumerable<Client>> getAllClients();

        //En el caso de que querramos devolver solo un elemento no hace falta usar el método enumerable de task
        Task<Client> getClientbyCedula(string cedula);


        Task<bool> createClient(Client clientParam);

        Task<bool> updateClient(Client cedula);

        Task<bool> deleteClient(string clientParam);
    }
}
