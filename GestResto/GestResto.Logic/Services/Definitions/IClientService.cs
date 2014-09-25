using GestResto.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Services.Definitions
{
    interface IFormatService
    {
        void Create(Client client);
        IList<Client> RetrieveAll();
        Client Retrieve(int pIdClient);
        void Update(Client client);
        void Delete(Client client);
    }
}
