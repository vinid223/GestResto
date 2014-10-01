using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestResto.Logic.Model.Entities;
using GestResto.Logic.Services.Definitions;

namespace GestResto.Logic.Services.MySql
{
    public class MysqlAuthentificationService : IAuthentificationService
    {
        private MySqlConnection connexion;

        #region IAuthentificationService Membres

        public Employe Retrieve(int pNumEmploye, string pMDP)
        {
            connexion = new MySqlConnection();

            string query = "SELECT * FROM Employes WHERE Em"
        }

        #endregion
    }
}
