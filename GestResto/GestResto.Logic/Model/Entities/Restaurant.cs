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
        public virtual List<Employe> ListeEmployes { get; set; }
        public virtual List<Item> ListeItem { get; set; }

        #endregion

        #region Tous les constructeurs de la classe
        /// <summary>
        /// Constructeur par d�faut
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
            ListeEmployes = new List<Employe>();
            ListeItem = new List<Item>();
        }

        /// <summary>
        /// Constructeur param�tr�
        /// </summary>
        /// <param name="pIdRestaurant">l'id du restaurant</param>
        /// <param name="pNom">Le nom du restaurant</param>
        /// <param name="pAdresse">L'adresse du restaurant</param>
        /// <param name="pTelephone">Le num�ro de t�l�phone du restaurant</param>
        /// <param name="pFax">Le num�ro de fax du restaurant</param>
        /// <param name="pVille">La ville du restaurant</param>
        /// <param name="pCodePostal">Le code postal du rstaurant</param>
        /// <param name="pDateCreation">La date de cr�ation</param>
        /// <param name="pListeEmploye">La liste d'employ�s</param>
        /// <param name="pListeItem">La liste des items</param>
         public Restaurant(int pIdRestaurant, string pNom, string pAdresse, string pTelephone, string pFax, string pVille, string pCodePostal, DateTime pDateCreation, List<Employe> pListeEmploye, List<Item> pListeItem)
        {
            IdRestaurant=pIdRestaurant;
            Nom=pNom;
            Adresse=pAdresse;
            Telephone = pTelephone;
            Fax = pFax;
            Ville = pVille;
            CodePostal = pCodePostal;
            DateCreation = pDateCreation;
            ListeEmployes = pListeEmploye;
            ListeItem = pListeItem;
        }
        #endregion
    }
}