﻿using GestResto.Logic.Model.Args;
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
    class NHibernateAuthentificationService : IAuthentificationService
    {
        private ISession session = NHibernateConnexion.OpenSession();

        public Employe Retrieve(int pNumEmploye, string pMDP)
        {
            throw new NotImplementedException();
        }
    }
}