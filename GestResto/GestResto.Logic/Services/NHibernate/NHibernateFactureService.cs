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
            throw new NotImplementedException();
        }

        public IList<Facture> RetriveAll()
        {
            throw new NotImplementedException();
        }

        public Facture Retrive(int pIdFacture)
        {
            throw new NotImplementedException();
        }

        public void Update(Facture facture)
        {
            throw new NotImplementedException();
        }

        public void Delete(Facture facture)
        {
            throw new NotImplementedException();
        }
    }
}
