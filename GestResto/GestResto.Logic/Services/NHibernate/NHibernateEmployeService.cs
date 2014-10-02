﻿using GestResto.Logic.Model.Args;
using GestResto.Logic.Model.Entities;
using GestResto.Logic.Services.Defenitions;
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
    /// <summary>
    /// 
    /// </summary>
    public class NHibernateEmployeService : IEmployeService
    {
        private ISession session = NHibernateConnexion.OpenSession();

        #region IEmployeService Membres

        public void Create(Employe employe)
        {
            throw new NotImplementedException();
        }

        public IList<Employe> RetriveAll()
        {
            return session.Query<Employe>().ToList();
        }

        public Employe Retrive(RetrieveEmployeArgs args)
        {
            var result = from c in session.Query<Employe>()
                         where c.idEmploye == args.IIdEmploye
                         select c;

            return result.FirstOrDefault();
        }

        public void Update(Employe employe)
        {
            throw new NotImplementedException();
        }

        public void Delete(Employe employe)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
