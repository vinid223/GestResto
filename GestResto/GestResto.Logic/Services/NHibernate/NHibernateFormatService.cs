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
    class NHibernateFormatService : IFormatService
    {
        private ISession session = NHibernateConnexion.OpenSession();

        #region IFormatService Membres

        #endregion

        public void Create(Format format)
        {
            throw new NotImplementedException();
        }

        public IList<Format> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public Format Retrieve(int pIdFormat)
        {
            throw new NotImplementedException();
        }

        public void Update(Format format)
        {
            throw new NotImplementedException();
        }

        public void Delete(Format format)
        {
            throw new NotImplementedException();
        }
    }
}
