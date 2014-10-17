using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Model.Args
{
    /// <summary>
    /// Classe définissant les arguments de sélection de la classe Employe
    /// </summary>
    public class RetrieveEmployeArgs
    {
        public int IIdEmploye { get; set; }
        public string NoEmploye { get; set; }
        public string MDP { get; set; }
    }
}
