using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestResto.Logic.Services.Definitions;
using MySql.Data.MySqlClient;

namespace GestResto.Logic.Services.MySql
{
    public class MysqlAuthentificationService : IAuthentificationService
    {
        private MySqlConnection connexion;
    }
}
