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
    class NHibernateTableService : ITableService
    {
        private ISession session = NHibernateConnexion.OpenSession();

        #region ITableService Membres

        #endregion

        public void Create(Table table)
        {
            throw new NotImplementedException();
        }

        public IList<Table> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public Table Retrieve(int pIdTable)
        {
            throw new NotImplementedException();
        }

        public void Update(Table table)
        {
            throw new NotImplementedException();
        }

        public void Delete(Table table)
        {
            throw new NotImplementedException();
        }
    }
}
