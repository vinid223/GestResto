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

namespace GestResto.Logic.Classes
{
    /// <summary>
    /// Classe contenant toutes les informations d'un restaurant
    /// </summary>
    public class Restaurant 
    {
	    private int? idRestaurant ;
	    private string sNom ;
	    private string sAdresse ;
	    private string sTelephone ;
	    private string sFax ;
	    private string sVille ;
	    private string sCodePostal ;
	    private DateTime dateCreation ;
	    private List<Employe> listeEmployes ;
	    private List<Item> listeItem ;

        /// <summary>
        /// Constructeur par d�faut
        /// </summary>
        public Restaurant()
        {
            idRestaurant=null;
            sNom="";
            sAdresse="";
            sTelephone = "";
            sFax = "";
            sVille = "";
            sCodePostal = "";
            dateCreation = DateTime.Now;
            listeEmployes = new List<Employe>();
            listeItem = new List<Item>();
        }

        /// <summary>
        /// Constructeur param�tr�
        /// </summary>
        /// <param name="pIdRestaurant">l'id du restaurant</param>
        /// <param name="pSNom">Le nom du restaurant</param>
        /// <param name="pSAdresse">L'adresse du restaurant</param>
        /// <param name="pSTelephone">Le num�ro de t�l�phone du restaurant</param>
        /// <param name="pSFax">Le num�ro de fax du restaurant</param>
        /// <param name="pSVille">La ville du restaurant</param>
        /// <param name="pSCodePostal">Le code postal du rstaurant</param>
        /// <param name="pDateCreation">La date de cr�ation</param>
        /// <param name="pListeEmploye">La liste d'employ�s</param>
        /// <param name="pListeItem">La liste des items</param>
         public Restaurant(int pIdRestaurant, string pSNom, string pSAdresse, string pSTelephone, string pSFax, string pSVille, string pSCodePostal, DateTime pDateCreation, List<Employe> pListeEmploye, List<Item> pListeItem)
        {
            idRestaurant=pIdRestaurant;
            sNom=pSNom;
            sAdresse=pSAdresse;
            sTelephone = pSTelephone;
            sFax = pSFax;
            sVille = pSVille;
            sCodePostal = pSCodePostal;
            dateCreation = pDateCreation;
            listeEmployes = pListeEmploye;
            listeItem = pListeItem;
        }

    }
}