using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestResto.Logic.Model.Args;
using GestResto.Logic.Model.Entities;
using GestResto.Logic.Services.Definitions;
using GestResto.Logic.Services.Helpers;
using NHibernate;
using NHibernate.Linq;

namespace GestResto.Logic.Services.NHibernate
{
    public class NHibernateFormatItemClientFacture : IFormatItemClientFactureService
    {
        private ISession session = NHibernateConnexion.OpenSession();

        public void Create(FormatItemClientFacture formatItemClientFacture)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(formatItemClientFacture);
                transaction.Commit();
            }
        }

        public IList<FormatItemClientFacture> RetrieveAll()
        {
            var result = from i in session.Query<FormatItemClientFacture>()
            select i;
            
            IList<FormatItemClientFacture> listeTemp = result.ToList();
            return listeTemp;
        }

        public FormatItemClientFacture Retrieve(RetrieveFormatItemClientFactureArgs args)
        {
            var result = from i in session.Query<FormatItemClientFacture>()
                where i.IdFormatItemClientFacture == args.IIdFormatItemClientFacture
                select i;
            
            FormatItemClientFacture formatItemClientFactureTest =null;
            return formatItemClientFactureTest;
        }

        
        public void Update(FormatItemClientFacture formatItemClientFacture)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(formatItemClientFacture);
                transaction.Commit();
            }
        }

        
        public void Delete(FormatItemClientFacture formatItemClientFacture)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(formatItemClientFacture);
                transaction.Commit();
            }
        }
    }
}
