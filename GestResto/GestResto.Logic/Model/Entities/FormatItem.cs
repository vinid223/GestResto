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
    public class FormatItem : BaseEntity
    {
        #region Variables de la classe
        public virtual int? IdFormatItem { get; set; }
        private float _prix;
        private Format _formatAssocie;
        private Item _itemAssocie;
        private bool _estModifie;
        
        #endregion

        #region Propriété

        public virtual float Prix
        {
            get
            {
                return _prix;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _prix = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual Format FormatAssocie
        {
            get
            {
                return _formatAssocie;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _formatAssocie = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual Item ItemAssocie 
        { 
            get
            {
                return _itemAssocie;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _itemAssocie = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }

        public virtual bool EstModifie
        {
            get
            {
                return _estModifie;
            }
            set
            {
                RaisePropertyChanging();
                _estModifie = value;
                RaisePropertyChanged();
            }
        }

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

        public virtual string FormatComplet
        {
            get
            {
                return FormatAssocie.Nom + " - " + Prix + "$";
            }
            
        }

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
