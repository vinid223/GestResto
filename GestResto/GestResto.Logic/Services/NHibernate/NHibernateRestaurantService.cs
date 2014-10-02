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
    class NHibernateRestaurantService : IRestaurantService
    {
        private ISession session = NHibernateConnexion.OpenSession();

        public void Create(Restaurant item)
        {
            throw new NotImplementedException();
        }

        public Restaurant Retrieve(int pIdRestaurant)
        {
            throw new NotImplementedException();
        }

        public void Update(Restaurant item)
        {
            throw new NotImplementedException();
        }
    }
}
