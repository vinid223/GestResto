﻿using System;
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
    public class NHibertateCategorieService : ICategorieService
    {
        private ISession session = NHibernateConnexion.OpenSession();

        #region ICategorieService Membres

        public void Create(Categorie categorie)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(categorie);
                transaction.Commit();
            }
        }

        public IList<Categorie> RetrieveAll()
        {
            return session.Query<Categorie>().ToList();
        }

        public Categorie Retrieve(RetrieveCategorieArgs args)
        {
            var result = from c in session.Query<Categorie>()
                        where c.IdCategorie == args.idCategorie
                        select c;

            return result.FirstOrDefault();
        }

        public void Update(Categorie categorie)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(categorie);
                transaction.Commit();
            }
        }

        #endregion
    }
}
