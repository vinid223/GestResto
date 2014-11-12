using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestResto.Logic.Model.Args;
using GestResto.Logic.Model.Entities;
using GestResto.Logic.Services.Definitions;
using GestResto.Logic.Services.Helpers;
using NHibernate;
using NHibernate.Linq;

namespace GestResto.Logic.Services.NHibernate
{
    public class NHibernateCategorieService : ICategorieService
    {
        private ISession session = NHibernateConnexion.OpenSession();
        private ISession sessionLazy = NHibernateConnexion.OpenSession();

        #region ICategorieService Membres

        public void Create(Categorie categorie)
        {
            session = NHibernateConnexion.OpenSession();
            using (var transaction = session.BeginTransaction())
            {
                session.Save(categorie);
                transaction.Commit();
            }
            session.Close();
        }

        public IList<Categorie> RetrieveAll()
        {
            sessionLazy = NHibernateConnexion.OpenSession();

            var result = from c in sessionLazy.Query<Categorie>()
                         orderby c.EstActif descending
                         select c;

            IList<Categorie> listeTemp = result.ToList();
            sessionLazy.Close();
            return listeTemp;
        }

        public Categorie Retrieve(RetrieveCategorieArgs args)
        {
            sessionLazy = NHibernateConnexion.OpenSession();
            var result = from c in sessionLazy.Query<Categorie>()
                        where c.IdCategorie == args.idCategorie
                        select c;

            return result.FirstOrDefault();
        }

        public void Update(Categorie categorie)
        {
            session = NHibernateConnexion.OpenSession();
            using (var transaction = session.BeginTransaction())
            {
                session.Update(categorie);
                transaction.Commit();
            }
            session.Close();
        }

        #endregion
    }
}
