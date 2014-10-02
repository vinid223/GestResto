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
    public class NHibernateItemService : IItemService
    {
        private ISession session = NHibernateConnexion.OpenSession();

        #region IItemService Membres

        #endregion

        public void Create(Item item)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(item);
                transaction.Commit();
            }
        }

        public IList<Item> RetrieveAll()
        {
            return session.Query<Item>().ToList();
        }

        public Item Retrieve(RetrieveItemArgs args)
        {
            var result = from i in session.Query<Item>()
                         where i.IdItem == args.IIdItem
                         select i;

            return result.FirstOrDefault();
        }

        public void Update(Item item)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(item);
                transaction.Commit();
            }
        }
    }
}
