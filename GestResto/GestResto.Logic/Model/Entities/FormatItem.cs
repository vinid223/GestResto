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
        public virtual int? idFormatItem { get; set; }
        public virtual float Prix { get; set; }
        #endregion

        #region Constructeurs

        public FormatItem()
        {
            idFormatItem = null;
            Prix = 0;

        }

        public FormatItem(int pIdFormatItem, float pPrix)
        {
            idFormatItem = pIdFormatItem;
            Prix = pPrix;
        }

        public FormatItem(float pPrix)
        {
            Prix = pPrix;
        }

        #endregion
    }
}
