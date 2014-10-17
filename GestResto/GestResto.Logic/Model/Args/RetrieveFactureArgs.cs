using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Model.Args
{
    /// <summary>
    /// Classe définissant les arguments de sélection de la classe Facture
    /// </summary>
    public class RetrieveFactureArgs
    {
        public int IIdFacture { get; set; }
        public DateTime DTdateCreation { get; set; }
    }
}
