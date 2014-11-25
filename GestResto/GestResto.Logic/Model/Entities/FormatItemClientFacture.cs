using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Model.Entities
{
    public class FormatItemClientFacture : BaseEntity
    {   
        #region Variables de la classe
        public virtual int? IdFormatItemClientFacture { get; set; }
        public virtual float Prix { get; set; }
        public virtual Facture facture { get; set; }
        public virtual Client client { get; set; }
        public virtual FormatItem FormatItemAssocie { get; set; }
        public virtual IList<FormatItemClientFacture> ListFicf { get; set; }

        private bool _estComplementaire;
       
        #endregion

        #region Propriétés

        public virtual bool EstComplementaire
        {
            get
            {
                return _estComplementaire;
            }
            set
            {
                RaisePropertyChanging();
                _estComplementaire = value;
                RaisePropertyChanged();
            }
        }

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

        #region Redefinition des fonctions de bases



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

            FormatItemClientFacture p = obj as FormatItemClientFacture;

            if (p == null)
            {
                return false;
            }

            return this.IdFormatItemClientFacture == p.IdFormatItemClientFacture;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

    }
}
