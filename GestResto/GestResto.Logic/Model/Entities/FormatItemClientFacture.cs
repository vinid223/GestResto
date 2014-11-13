using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Model.Entities
{
    public class FormatItemClientFacture
    {   
        #region Variables de la classe
        public virtual int? IdFormatItemClientFacture { get; set; }
        public virtual float Prix { get; set; }
        public virtual Facture facture { get; set; }
        public virtual Client client { get; set; }
        public virtual FormatItem FormatItemAssocie { get; set; }
        public virtual IList<FormatItemClientFacture> ListFicf { get; set; }
        #endregion

        #region Constructeurs
        public FormatItemClientFacture()
        {
            IdFormatItemClientFacture = null;
            Prix = 0;
            ListFicf = new List<FormatItemClientFacture>();
        }
        
        public FormatItemClientFacture(int pIdFormatItemClientFacture, float pPrix, FormatItem pFormatItem)
        {
            IdFormatItemClientFacture = pIdFormatItemClientFacture;
            Prix = pPrix;
            FormatItemAssocie = pFormatItem;
            ListFicf = new List<FormatItemClientFacture>();
        }

        public FormatItemClientFacture(float pPrix, FormatItem pFormatItem)
        {
            Prix = pPrix;
            FormatItemAssocie = pFormatItem;
            ListFicf = new List<FormatItemClientFacture>();
        }
        #endregion

    }
}
