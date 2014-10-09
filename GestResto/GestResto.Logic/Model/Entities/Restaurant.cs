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
    public class Restaurant 
    {
        #region Liste des variables de la classe

        public virtual int? IdRestaurant { get; set; }
        public virtual string Nom { get; set; }
        public virtual string Adresse { get; set; }
        public virtual string Telephone { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Ville { get; set; }
        public virtual string CodePostal { get; set; }
        public virtual DateTime DateCreation { get; set; }

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