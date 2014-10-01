using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestResto.Logic.Services.Definitions;
using GestResto.Logic.Services.Helpers;

namespace GestResto.Logic.Services.MySql
{
    class MySqlEmployeService : IEmployeService
    {
        private MySqlConnexion connexion;


        #region IEmployeService Membres

        public void Create(Model.Entities.Employe employe)
        {
            throw new NotImplementedException();
        }

        public IList<Model.Entities.Employe> RetriveAll()
        {
            throw new NotImplementedException();
        }

        public Model.Entities.Employe Retrive(int pIdEmploye)
        {
            throw new NotImplementedException();
        }

        public void Update(Model.Entities.Employe employe)
        {
            throw new NotImplementedException();
        }

        public void Delete(Model.Entities.Employe employe)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
