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
        private ISession sessionLazy = NHibernateConnexion.OpenSession();

        #region IFormatService Membres

        #endregion

        public void Create(Format format)
        {
            session = NHibernateConnexion.OpenSession();
            using (var transaction = session.BeginTransaction())
            {
                session.Save(format);
                transaction.Commit();
            }
            session.Close();
        }

        public IList<Format> RetrieveAll()
        {
            sessionLazy = NHibernateConnexion.OpenSession();
            IList<Format> listeTemp = sessionLazy.Query<Format>().ToList();
            sessionLazy.Close();
            return listeTemp;
        }

        public Format Retrieve(RetrieveFormatArgs args)
        {
            sessionLazy = NHibernateConnexion.OpenSession();
            var result = from c in session.Query<Format>()
                         where c.IdFormat == args.IIdFormat
                         select c;

            sessionLazy.Close();
            return result.FirstOrDefault();
        }

        public void Update(Format format)
        {
            session = NHibernateConnexion.OpenSession();
            using (var transaction = session.BeginTransaction())
            {
                session.Update(format);
                transaction.Commit();
            }
            session.Close();
        }
    }
}
