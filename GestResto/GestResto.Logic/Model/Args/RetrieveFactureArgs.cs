using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Model.Args
{
    public class RetrieveFactureArgs
    {
        public int IIdFacture { get; set; }
        public DateTime DTdateCreation { get; set; }
    }
}
