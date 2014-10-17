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
    public class NHibernateTypeEmployeService : ITypeEmployeService
    {
        private ISession session = NHibernateConnexion.OpenSession();

        public TypeEmploye Retrieve(RetrieveTypeEmployeArgs args)
        {
            var result = from c in session.Query<TypeEmploye>()
                         where c.IdTypeEmploye == args.IIdTypeEmploye
                         select c;
            return result.FirstOrDefault();
        }
    }
}
