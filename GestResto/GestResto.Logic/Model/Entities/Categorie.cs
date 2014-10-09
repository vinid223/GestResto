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
    public class Categorie
    {
        #region Liste des variables de la classe

        public virtual int? IdCategorie { get; set; }
        public virtual string Nom { get; set; }
        public virtual bool EstActif { get; set; }
        public virtual bool EstComplementaire { get; set; }

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
        }
        #endregion
    }
}