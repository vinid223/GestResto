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
    public class NHibernateTableService : ITableService
    {
        private ISession session = NHibernateConnexion.OpenSession();
        

        #region ITableService Membres

        #endregion

        public void Create(Table table)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(table);
                transaction.Commit();
            }
        }

        /// <summary>
        /// Retourne les tables en ordre de EstActif
        /// </summary>
        /// <returns>La liste de tables</returns>
        public IList<Table> RetrieveAll()
        {
            var result = from i in session.Query<Table>()
                         orderby i.EstActif descending
                         select i;

            IList<Table> listeTemp = result.ToList();

            return listeTemp;
        }

        public Table Retrieve(RetrieveTableArgs args)
        {
            var result = from c in session.Query<Table>()
                         where c.IdTable == args.IIdTable
                         select c;

            return result.FirstOrDefault();
        }

        public void Update(Table table)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(table);
                transaction.Commit();
            }
        }
    }
}
