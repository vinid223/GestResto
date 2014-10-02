using GestResto.Logic.Model.Args;
using GestResto.Logic.Model.Entities;
using GestResto.Logic.Services.Definitions;
using GestResto.Logic.Services.Helpers;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Services.NHibernate
{
    class NHibernateClientService : IClientService
    {
        private ISession session = NHibernateConnexion.OpenSession();

        #region IClientService Membres

        #endregion

        public void Create(Client client)
        {
            throw new NotImplementedException();
        }

        public IList<Client> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public Client Retrieve(int pIdClient)
        {
            throw new NotImplementedException();
        }

        public void Update(Client client)
        {
            throw new NotImplementedException();
        }

        public void Delete(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
