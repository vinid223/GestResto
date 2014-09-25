//
//
//  
//
//  Projet : GestResto
//  Nom du fichier : Employe.cs
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
    /// Classe contenant toutes les informations d'un employé
    /// </summary>
    public class Employe
    {
        #region Variables appartenant à la classe Employe
        
        private int? idEmploye;
	    private string sTypeEmploye;
	    private string sNom;
	    private string sPrenom;
	    private int noEmploye;
	    private string sMotDePasse;
	    private string sAdresse;
	    private string sVille;
	    private string sCodePostal;
	    private string sNAS;
	    private float fSalaire;
	    private string sTelephone;
	    private bool bActif;
	    private List<Commande> ListeCommandes;

        #endregion

        #region Tous les constructeurs de la classe Employes
        /// <summary>
        /// Constructeur par défaut de la classe Employé. Elle initialise toutes les variables pour une valeur null ou vide.
        /// </summary>
        public Employe()
        {
                idEmploye = null;
                sTypeEmploye = "";
                sNom = "";
                sPrenom = "";
                noEmploye = 0;
                sMotDePasse = "";
                sAdresse = "";
                sVille = "";
                sCodePostal = "";
                sNAS = "";
                fSalaire = 0;
                sTelephone = "";
                bActif = false;
                ListeCommandes = new List<Commande>();
        }

        /// <summary>
        /// Constructeur paramétré de la classe Employe
        /// </summary>
        /// <param name="pIdEmploye">id de l'employe</param>
        /// <param name="pIdTypeEmploye">Id du type d'employe</param>
        /// <param name="pNom">Nom de l'employe</param>
        /// <param name="pPrenom">Prenom de l'employe</param>
        /// <param name="pNoEmploye">Numero de l'employe</param>
        /// <param name="pMotPasse">Mot de passe de l'employe</param>
        /// <param name="pAdresse">Adresse de l'employe</param>
        /// <param name="pVille">Ville de l'employe</param>
        /// <param name="pCodePostal">Code postal de l'employe</param>
        /// <param name="pNAS">NAS de l'employe</param>
        /// <param name="pSalaire">Salaire de l'employe</param>
        /// <param name="pTelephone">Telephone de l'employe</param>
        Employe(int pIdEmploye, string pNom, string pPrenom, int pNoEmploye, string pMotPasse, string pAdresse, string pVille, string pCodePostal, string pNAS, float pSalaire, string pTelephone)
        {
            idEmploye = pIdEmploye;
            sNom = pNom;
            sPrenom = pPrenom;
            noEmploye = pNoEmploye;
            sMotDePasse = pMotPasse;
            sAdresse = pAdresse;
            sVille = pVille;
            sCodePostal = pCodePostal;
            sNAS = pNAS;
            fSalaire = pSalaire;
            sTelephone = pTelephone;
        }

        #endregion
    }
}