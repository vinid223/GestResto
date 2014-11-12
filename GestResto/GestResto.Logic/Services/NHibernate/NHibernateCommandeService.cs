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
    public class NHibernateCommandeService : ICommandeService
    {
        private ISession session = NHibernateConnexion.OpenSession();

        #region ICommandeService Membres

        #endregion
        public void Create(FormatItemClientFacture ficf)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(ficf);
                transaction.Commit();
            }
        }

        public void Create(Commande commande)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(commande);
                transaction.Commit();
            }
        }

        public IList<Commande> RetrieveAll(int id)
        {
            session = NHibernateConnexion.OpenSession();

            var result = from c in session.Query<Commande>()
                         where c.IdEmploye == id
                         select c;

            IList<Commande> listeTemp = result.ToList();
            return listeTemp;
        }

        public Commande Retrieve(RetrieveCommandeArgs args)
        {
            var result = from c in session.Query<Commande>()
                         where c.IdCommande == args.IIdCommande
                         select c;

            return result.FirstOrDefault();
        }

        public void Update(Commande commande)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(commande);
                
                transaction.Commit();
            }
        }

    }
}
