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
    class NHibernateFactureService : IFactureService
    {
        private ISession session = NHibernateConnexion.OpenSession();

        #region IFactureService Membres

        #endregion

        public void Create(Facture facture)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(facture);
                transaction.Commit();
            }
        }

        public IList<Facture> RetriveAll()
        {
            return session.Query<Facture>().ToList();
        }

        public Facture Retrive(RetrieveFactureArgs args)
        {
            var result = from f in session.Query<Facture>()
                         where f.IdFacture == args.IIdFacture
                         select f;

            return result.FirstOrDefault();
        }

        public void Update(Facture facture)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(facture);
                transaction.Commit();
            }
        }
    }
}
