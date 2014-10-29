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
        private ISession sessionLazy = NHibernateConnexion.OpenSession();

        public void Create(FormatItem formatItem)
        {
            session = NHibernateConnexion.OpenSession();
            using (var transaction = session.BeginTransaction())
            {
                session.Save(formatItem);
                transaction.Commit();
            }
            session.Close();
        }

        public IList<FormatItem> RetrieveAll()
        {
            sessionLazy = NHibernateConnexion.OpenSession();
            return sessionLazy.Query<FormatItem>().ToList();
        }

        public FormatItem Retrieve(RetrieveFormatItemArgs args)
        {
            session = NHibernateConnexion.OpenSession();
            var result = from c in session.Query<FormatItem>()
                         where c.IdFormatItem == args.IdFormatItem
                         select c;
            FormatItem formatItemTemp = result.FirstOrDefault();
            session.Close();
            return formatItemTemp;
        }

        public void Update(FormatItem formatItem)
        {
            session = NHibernateConnexion.OpenSession();
            using (var transaction = session.BeginTransaction())
            {
                session.Update(formatItem);
                transaction.Commit();
            }
            session.Close();
        }

        public void Delete(FormatItem formatItem)
        {
            session = NHibernateConnexion.OpenSession();
            using (var transaction = session.BeginTransaction())
            {
                session.Delete(formatItem);
                transaction.Commit();
            }
            session.Close();
        }
    }
}
