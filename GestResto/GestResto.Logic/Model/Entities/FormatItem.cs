using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Model.Entities
{
    /// <summary>
    /// Cette classe est nécessaire pour l'ORM, sinon c'est impossible de configurer le xml.
    /// </summary>
    public class FormatItem
    {
        #region Variables de la classe
        public virtual float Prix { get; set; }
        public virtual Format Formats { get; set; }
        public virtual Item Items { get; set; }
        #endregion

        #region Constructeurs

        public FormatItem()
        {
        }


        #endregion
    }
}
