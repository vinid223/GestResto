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
    public class NHibernateEmployeService : IEmployeService
    {
        private ISession session = NHibernateConnexion.OpenSession();

        #region IEmployeService Membres

        public void Create(Employe employe)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(employe);
                transaction.Commit();
            }
        }

        public IList<Employe> RetriveAll()
        {
            return session.Query<Employe>().ToList();
        }

        public Employe Retrive(RetrieveEmployeArgs args)
        {
            session = NHibernateConnexion.OpenSession();
            if (args.MDP != null && args.NoEmploye != null)
            {
                var result = from c in session.Query<Employe>()
                             where c.MotDePasse == args.MDP &&
                             c.NoEmploye == args.NoEmploye
                             select c;
                return result.FirstOrDefault();
            }
            else
            {
                var result = from c in session.Query<Employe>()
                             where c.IdEmploye == args.IIdEmploye
                             select c;
                return result.FirstOrDefault();
            }
        }

        public void Update(Employe employe)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(employe);
                transaction.Commit();
            }
        }

        #endregion
    }
}
