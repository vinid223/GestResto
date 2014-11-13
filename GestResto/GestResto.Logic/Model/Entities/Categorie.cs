//
//
//  
//
//  Projet : GestResto
//  Nom du fichier : Categorie.cs
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
    /// Classe Cat�gorie
    /// </summary>
    public class Categorie : BaseEntity
    {
        #region Liste des variables de la classe

        public virtual int? IdCategorie { get; set; }

        private bool _estComplementaire;

        public virtual bool EstComplementaire
        {
            get
            {
                return _estComplementaire;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _estComplementaire = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }

        private bool _estActif;

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

        private bool _estModifie;

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

        private string _nom;

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

                EstModifie = true;

                _nom = value;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }

        #endregion

        #region Tous les constructeurs de la classe
        /// <summary>
        /// Constructeur par d�faut de Cat�gorie
        /// </summary>
        public Categorie()
        {
            IdCategorie = null;
            Nom = "";
            EstActif = true;
            EstComplementaire = false;
            EstModifie = false;
        }

        /// <summary>
        /// Constructeur param�tr� de Cat�gorie
        /// </summary>
        /// <param name="pIdCategorie">Id de la cat�gorie � cr�er</param>
        /// <param name="pNom">Nom de la cat�gorie</param>
        /// <param name="pEstActif">Statut de la cat�gorie par true ou false pour actif ou non</param>
        /// <param name="pEstComplementaire">Statut indiquant si la cat�gorie est une cat�gorie compl�mentaire par un true ou false</param>
        public Categorie(int pIdCategorie, string pNom, bool pEstActif, bool pEstComplementaire)
        {
            IdCategorie = pIdCategorie;
            Nom = pNom;
            EstActif = pEstActif;
            EstComplementaire = pEstComplementaire;
            EstModifie = false;
        }

        /// <summary>
        /// Constructeur param�tr� de cat�gorie sans le param�tre de l'id puisque nous avons besoin de cr�er une cat�gorie sans d'id
        /// </summary>
        /// <param name="pNom">Nom de la cat�gorie</param>
        /// <param name="pEstActif">Statut de la cat�gorie par true ou false pour actif ou non</param>
        /// <param name="pEstComplementaire">Statut indiquant si la cat�gorie est une cat�gorie compl�mentaire par un true ou false</param>
        public Categorie(string pNom, bool pEstActif, bool pEstComplementaire)
        {
            Nom = pNom;
            EstActif = pEstActif;
            EstComplementaire = pEstComplementaire;
            EstModifie = false;
        }
        #endregion

        #region Red�finition de fonctions de bases
        /// <summary>
        /// Permet de comparer deux cat�gories.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Categorie p = obj as Categorie;

            if (p != null && p.IdCategorie == -1)
                return true;

            if (p == null)
            {
                return false;
            }

            return this.IdCategorie == p.IdCategorie;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}