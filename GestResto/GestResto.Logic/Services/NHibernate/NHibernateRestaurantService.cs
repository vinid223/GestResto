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

        public void Create(Restaurant item)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Save(item);
                transaction.Commit();
            }
        }

        public Restaurant Retrieve(RetrieveRestaurantArgs args)
        {
            var result = from r in session.Query<Restaurant>()
                         where r.IdRestaurant == args.IIdRestaurant
                         select r;

            return result.FirstOrDefault();
        }

        public void Update(Restaurant item)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(item);
                transaction.Commit();
            }
        }
    }
}
