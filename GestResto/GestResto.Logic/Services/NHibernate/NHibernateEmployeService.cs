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
        private ISession sessionLazy = NHibernateConnexion.OpenSession();

        #region IEmployeService Membres

        public void Create(Employe employe)
        {
            session = NHibernateConnexion.OpenSession();
            using (var transaction = session.BeginTransaction())
            {
                session.Save(employe);
                transaction.Commit();
            }
            session.Close();
        }

        public IList<Employe> RetriveAll()
        {
            sessionLazy = NHibernateConnexion.OpenSession();
            IList<Employe> listeTemp = sessionLazy.Query<Employe>().ToList();

            // On boucle dans tous nos résultat pour décrypter le mot de passe
            foreach (var employe in listeTemp)
            {
                employe.MotDePasse = Employe.Decrypt(employe.MotDePasse);
            }

            return listeTemp;
        }

        public Employe Retrive(RetrieveEmployeArgs args)
        {
            session = NHibernateConnexion.OpenSession();
            Employe employeTemp;

            if (args.MDP != null && args.NoEmploye != null)
            {
                var result = from c in session.Query<Employe>()
                             where c.MotDePasse == args.MDP &&
                             c.NoEmploye == args.NoEmploye
                             select c;
                employeTemp = result.FirstOrDefault();
            }
            else
            {
                var result = from c in session.Query<Employe>()
                             where c.IdEmploye == args.IIdEmploye
                             select c;
                employeTemp = result.FirstOrDefault();
            }

            session.Close();
            return employeTemp;
        }

        public void Update(Employe employe)
        {
            employe.MotDePasse = Employe.Encrypt(employe.MotDePasse);
            session = NHibernateConnexion.OpenSession();
            using (var transaction = session.BeginTransaction())
            {
                session.Update(employe);
                transaction.Commit();
            }
            session.Close();
        }

        #endregion
    }
}
