//
//
//  
//
//  Projet : GestResto
//  Nom du fichier : Item.cs
//  Date : 2014-09-25
//  Auteurs : Tommy Demers, Vincent Desrosiers et Simon Turcotte
//
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Model.Entities
{
    /// <summary>
    /// Classe Item
    /// </summary>
    public class Item : BaseEntity
    {
        #region Variables de la classe
        public virtual int? IdItem { get; set; }
        private string _nom;
        private Categorie _categories;
        private IList<FormatItem> _formats;
        private bool _estActif;
        private bool _estModifie;

        #endregion

        #region Propriété

        public virtual string Nom
        {
            get
            {
                return _nom;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _nom = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual Categorie Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _categories = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual IList<FormatItem> Formats
        {
            get
            {
                return _formats;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _formats = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual bool EstActif
        {
            get
            {
                return _estActif;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _estActif = value;
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

        #region Constructeur de la classe Item
        /// <summary>
        /// Constructeur par défaut d'item
        /// </summary>
        public Item()
        {
            IdItem = null;
            Nom = "";
            Formats = new List<FormatItem>();
            Categories = new Categorie();
          //  ListeComplements = new List<Item>();
            EstActif = true;
        }

        /// <summary>
        /// Constructeur paramétré de la classe Item
        /// </summary>
        /// <param name="pIdItem">Id de l'item</param>
        /// <param name="pNom">Nom de l'item</param>
        /// <param name="pFormatItem">Objet format de l'item</param>
        /// <param name="pCategories">Objet catégrie de l'item</param>
        public Item(int pIdItem, string pNom, List<FormatItem> pFormatItem, Categorie pCategories, bool pEstActif)
            : this()
        {
            IdItem = pIdItem;
            Nom = pNom;
            Formats = pFormatItem;
            Categories = pCategories;
            EstActif = pEstActif;
        }

        /// <summary>
        /// Permet de inséré des items dans la BD sans le ID.
        /// </summary>
        /// <param name="pNom">Nom de l'item</param>
        /// <param name="pFormatItem">Objet format de l'item</param>
        /// <param name="pCategories">Objet catégrie de l'item</param>
        public Item(string pNom, List<FormatItem> pFormatItem, Categorie pCategories, bool pEstActif)
        {
            Nom = pNom;
            Formats = pFormatItem;
            Categories = pCategories;
            EstActif = pEstActif;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Item p = obj as Item;

            if (p == null)
            {
                return false;
            }

            return this.IdItem == p.IdItem;
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}