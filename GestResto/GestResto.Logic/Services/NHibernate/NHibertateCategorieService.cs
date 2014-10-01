using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestResto.Logic.Model.Args;
using GestResto.Logic.Model.Entities;
using GestResto.Logic.Services.Defenitions;
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
            throw new NotImplementedException();
        }

        public IList<Categorie> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public Categorie Retrieve(RetrieveCategorieArgs args)
        {
            var result = from c in session.Query<Categorie>()
                        where c.idCategorie == args.idCategorie
                        select c;

            return result.FirstOrDefault();
        }

        public void Update(Categorie categorie)
        {
            throw new NotImplementedException();
        }

        public void Delete(Categorie categorie)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
