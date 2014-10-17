using GestResto.Logic.Model.Entities;
using GestResto.Logic.Model.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Services.Definitions
{
    /// <summary>
    /// Interface permettant de définir les fonctions du service Client
    /// </summary>
    public interface IClientService
    {
        void Create(Client client);
        IList<Client> RetrieveAll();
        Client Retrieve(RetrieveClientArgs args);
        void Update(Client client);
        void Delete(Client client);
    }
}
