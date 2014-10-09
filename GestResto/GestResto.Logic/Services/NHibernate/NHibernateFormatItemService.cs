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
    public class NHibernateFormatItemService : IFormatItemService
    {
        private ISession session = NHibernateConnexion.OpenSession();

        public void Create(FormatItem formatItem)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(formatItem);
                transaction.Commit();
            }
        }

        public IList<FormatItem> RetrieveAll()
        {
            return session.Query<FormatItem>().ToList();
        }

        public FormatItem Retrieve(RetrieveFormatItemArgs args)
        {
            var result = from c in session.Query<FormatItem>()
                         where c.idFormatItem == args.IdFormatItem
                         select c;

            return result.FirstOrDefault();
        }

        public void Update(FormatItem formatItem)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(formatItem);
                transaction.Commit();
            }
        }
    }
}
