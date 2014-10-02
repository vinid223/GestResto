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
            using (var transaction = session.BeginTransaction())
            {
                session.Save(client);
                transaction.Commit();
            }
        }

        public IList<Client> RetrieveAll()
        {
            return session.Query<Client>().ToList();
        }

        public Client Retrieve(RetrieveClientArgs args)
        {
            var result = from c in session.Query<Client>()
                         where c.IdClient == args.IIdClient
                         select c;

            return result.FirstOrDefault();
        }

        public void Update(Client client)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(client);
                transaction.Commit();
            }
        }

        public void Delete(Client client)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Delete(client);
                transaction.Commit();
            }
        }
    }
}
