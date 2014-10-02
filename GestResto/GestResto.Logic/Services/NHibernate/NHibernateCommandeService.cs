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
    class NHibernateCommandeService : ICommandeService
    {
        private ISession session = NHibernateConnexion.OpenSession();

        #region ICommandeService Membres

        #endregion

        public void Create(Commande categorie)
        {
            throw new NotImplementedException();
        }

        public IList<Commande> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public Commande Retrieve(int pIdCategorie)
        {
            throw new NotImplementedException();
        }

        public void Update(Commande categorie)
        {
            throw new NotImplementedException();
        }

        public void Delete(Commande categorie)
        {
            throw new NotImplementedException();
        }
    }
}
