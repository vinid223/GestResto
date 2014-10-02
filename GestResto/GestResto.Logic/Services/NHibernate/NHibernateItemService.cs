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
    class NHibernateItemService : IItemService
    {
        private ISession session = NHibernateConnexion.OpenSession();

        #region IItemService Membres

        #endregion

        public void Create(Item item)
        {
            throw new NotImplementedException();
        }

        public IList<Item> RetrieveAll()
        {
            throw new NotImplementedException();
        }

        public Item Retrieve(int pIdItem)
        {
            throw new NotImplementedException();
        }

        public void Update(Item item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
