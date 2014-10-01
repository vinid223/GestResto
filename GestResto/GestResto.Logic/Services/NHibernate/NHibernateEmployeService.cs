using GestResto.Logic.Services.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Services.NHibernate
{
    public class NHibernateEmployeService : IEmployeService
    {
        private ISession session = NHibernateConnexion.OpenSession();
    }
}
