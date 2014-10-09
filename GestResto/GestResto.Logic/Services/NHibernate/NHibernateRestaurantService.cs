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
    public class NHibernateRestaurantService : IRestaurantService
    {
        private ISession session = NHibernateConnexion.OpenSession();
        private ISession sessionLazy = NHibernateConnexion.OpenSession();

        public Restaurant Retrieve(RetrieveRestaurantArgs args)
        {
            sessionLazy = NHibernateConnexion.OpenSession();
            var result = from r in sessionLazy.Query<Restaurant>()
                         where r.IdRestaurant == args.IIdRestaurant
                         select r;

            return result.FirstOrDefault();
            sessionLazy.Close();
        }

        public void Update(Restaurant item)
        {
            session = NHibernateConnexion.OpenSession();
            using (var transaction = session.BeginTransaction())
            {
                session.Update(item);
                transaction.Commit();
            }
            session.Close();
        }
    }
}
