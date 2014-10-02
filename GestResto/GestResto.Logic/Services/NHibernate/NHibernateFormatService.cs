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
    public class NHibernateFormatService : IFormatService
    {
        private ISession session = NHibernateConnexion.OpenSession();

        #region IFormatService Membres

        #endregion

        public void Create(Format format)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(format);
                transaction.Commit();
            }
        }

        public IList<Format> RetrieveAll()
        {
            return session.Query<Format>().ToList();
        }

        public Format Retrieve(RetrieveFormatArgs args)
        {
            var result = from c in session.Query<Format>()
                         where c.IdFormat == args.IIdFormat
                         select c;

            return result.FirstOrDefault();
        }

        public void Update(Format format)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(format);
                transaction.Commit();
            }
        }
    }
}
