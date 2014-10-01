using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Model.Args
{
    public class RetrieveCommandeArgs
    {
        public int IIdCommande { get; set; }
        public DateTime DTdateDebut { get; set; }
    }
}
