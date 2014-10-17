using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Model.Args
{
    /// <summary>
    /// Classe définissant les arguments de sélection de la classe Table
    /// </summary>
    public class RetrieveTableArgs
    {
        public int IIdTable { get; set; }
        public int INoTable { get; set; }
        public bool BActif { get; set; }
        public bool BAssigne { get; set; }
    }
}
