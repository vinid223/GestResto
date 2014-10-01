using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestResto.Logic.Model.Entities;

namespace GestResto.Logic.Model.Args
{
    public class RetrieveItemArgs
    {
        public int IIdItem { get; set; }
        public Format format { get; set; }
        public Categorie categorie { get; set; }
    }
}
