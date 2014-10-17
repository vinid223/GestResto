using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestResto.Logic.Model.Entities;

namespace GestResto.Logic.Model.Args
{
    /// <summary>
    /// Classe définissant les arguments de sélection de la classe Item
    /// </summary>
    public class RetrieveItemArgs
    {
        public int IIdItem { get; set; }
        public Format format { get; set; }
        public Categorie categorie { get; set; }
    }
}
