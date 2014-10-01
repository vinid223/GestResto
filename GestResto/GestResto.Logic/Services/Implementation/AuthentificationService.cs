using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestResto.Logic.Services.Definitions;

namespace GestResto.Logic.Services.Implementation
{
    class AuthentificationService : IAuthentificationService
    {
        #region IAuthentificationService Membres

        public Model.Entities.Employe Retrieve(int pNumEmploye, string pMDP)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
