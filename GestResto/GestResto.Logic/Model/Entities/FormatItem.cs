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
        public virtual int? IdFormatItem { get; set; }
        public virtual float Prix { get; set; }
        public virtual Format FormatAssocie { get; set; }
        public virtual Item ItemAssocie { get; set; }
        #endregion

        #region Constructeurs

        public FormatItem()
        {
            IdFormatItem = null;
            Prix = 0;

        }

        public FormatItem(int pIdFormatItem, float pPrix, Format pFormat)
        {
            IdFormatItem = pIdFormatItem;
            Prix = pPrix;
            FormatAssocie = pFormat;
        }

        public FormatItem(float pPrix, Format pFormat)
        {
            Prix = pPrix;
            FormatAssocie = pFormat;
        }

        #endregion


        #region Redéfinition de fonctions de bases

        public override string ToString()
        {
            return ItemAssocie.Nom + " " + FormatAssocie.Libelle + " " + Prix;
        }
        /// <summary>
        /// Permet de comparer deux catégories.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            FormatItem p = obj as FormatItem;

            if (p == null)
            {
                return false;
            }

            return this.IdFormatItem == p.IdFormatItem;
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
