using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestResto.Logic.Model.Entities;
using GestResto.Logic.Services.Definitions;
using GestResto.Logic.Services.Helpers;

namespace GestResto.Logic.Services.MySql
{
    public class MysqlAuthentificationService : IAuthentificationService
    {
        private MySqlConnexion connexion;

        #region IAuthentificationService Membres

        public Employe Retrieve(int pNumEmploye, string pMDP)
        {
            connexion = new MySqlConnexion();

            string query = "SELECT * FROM Employes WHERE Em"
        }

        #endregion
    }
}
