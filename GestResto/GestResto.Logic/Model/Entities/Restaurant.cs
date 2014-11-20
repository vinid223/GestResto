//
//
//  
//
//  Projet : GestResto
//  Nom du fichier : Restaurant.cs
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
    /// Classe contenant toutes les informations d'un restaurant
    /// </summary>
    public class Restaurant : BaseEntity
    {
        #region Liste des variables de la classe

        public virtual int? IdRestaurant { get; set; }
        private string _nom;
        private string _adresse;
        private string _telephone;
        private string _fax;
        private string _ville;
        private string _codePostal;
        private DateTime _dateCreation;
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
        public virtual string Adresse
        {
            get
            {
                return _adresse;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _adresse = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual string Telephone
        {
            get
            {
                return _telephone;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _telephone = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual string Fax
        {
            get
            {
                return _fax;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _fax = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual string Ville
        {
            get
            {
                return _ville;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _ville = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual string CodePostal
        {
            get
            {
                return _codePostal;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _codePostal = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual DateTime DateCreation
        {
            get
            {
                return _dateCreation;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _dateCreation = value;
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

        #region Tous les constructeurs de la classe
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Restaurant()
        {
            IdRestaurant=null;
            Nom="";
            Adresse="";
            Telephone = "";
            Fax = "";
            Ville = "";
            CodePostal = "";
            DateCreation = DateTime.Now;
        }

        /// <summary>
        /// Constructeur paramétré
        /// </summary>
        /// <param name="pIdRestaurant">l'id du restaurant</param>
        /// <param name="pNom">Le nom du restaurant</param>
        /// <param name="pAdresse">L'adresse du restaurant</param>
        /// <param name="pTelephone">Le numéro de téléphone du restaurant</param>
        /// <param name="pFax">Le numéro de fax du restaurant</param>
        /// <param name="pVille">La ville du restaurant</param>
        /// <param name="pCodePostal">Le code postal du rstaurant</param>
        /// <param name="pDateCreation">La date de création</param>
         public Restaurant(int pIdRestaurant, string pNom, string pAdresse, string pTelephone, string pFax, string pVille, string pCodePostal, DateTime pDateCreation)
        {
            IdRestaurant=pIdRestaurant;
            Nom=pNom;
            Adresse=pAdresse;
            Telephone = pTelephone;
            Fax = pFax;
            Ville = pVille;
            CodePostal = pCodePostal;
            DateCreation = pDateCreation;
        }
        #endregion
    }
}