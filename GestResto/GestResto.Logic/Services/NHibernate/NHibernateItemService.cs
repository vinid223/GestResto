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

        public void Create(Item item)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(item);
                transaction.Commit();
            }
        }

        public IList<Item> RetrieveAll(bool sansActif = false)
        {
            if (sansActif)
            {
                var result = from i in session.Query<Item>()
                             where i.EstActif == true
                             select i;
                IList<Item> listeTemp = result.ToList();
                return listeTemp;
            }
            else
            {
                var result = from i in session.Query<Item>()
                             orderby i.EstActif descending
                             select i;
                IList<Item> listeTemp = result.ToList();
                return listeTemp;
            }
        }

        public Item Retrieve(RetrieveItemArgs args)
        {
            var result = from i in session.Query<Item>()
                         where i.IdItem == args.IIdItem
                         select i;
            Item itemTemp = result.FirstOrDefault();
            return itemTemp;
        }

        public void Update(Item item)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(item);
                transaction.Commit();
            }
        }

        public void Delete(FormatItem formatItem)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Delete(formatItem);
                transaction.Commit();
            }
        }

        public void ClearSession()
        {
            session.Clear();
        }
        #endregion
    }
}
