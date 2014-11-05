using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestResto.Logic.Model.Entities;

namespace GestResto.Logic.Model.Args
{
    public class RetrieveFormatItemClientFactureArgs
    {
        public int IIdFormatItemClientFacture { get; set; }
        public FormatItem FormatItemAssocie { get; set; }
    }
}
