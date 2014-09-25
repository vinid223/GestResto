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
    ///    Classe contenant toutes les informations d'un employé
    /// </summary>
    public class Employe 
    {
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
	    private float sSalaire;
	    private string sTelephone;
	    private bool bActif;
	    private List<Commande> ListeCommandes;

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
                sSalaire = 0;
                sTelephone = "";
                bActif = false;
                ListeCommandes = new List<Commande>();
        }

        /// <summary>
        /// Constructeur paramétré de la classe Employe
        /// </summary>
        /// <param name="pIdEmploye"></param>
        /// <param name="pIdTypeEmploye"></param>
        /// <param name="pNom"></param>
        /// <param name="pPrenom"></param>
        /// <param name="pNoEmploye"></param>
        /// <param name="pMotPasse"></param>
        /// <param name="pAdresse"></param>
        /// <param name="pVille"></param>
        /// <param name="pCodePostal"></param>
        /// <param name="pNAS"></param>
        /// <param name="pSalaire"></param>
        /// <param name="pTelephone"></param>
        Employe(int pIdEmploye, string pNom, string pPrenom, int pNoEmploye, string pMotPasse,
        string pAdresse, string pVille, string pCodePostal, string pNAS, float pSalaire, string pTelephone)
        {
            iIdEmploye = pIdEmploye;
            sNom = pNom;
            sPrenom = pPrenom;
            iNoEmploye = pNoEmploye;
            sMotPasse = pMotPasse;
            sAdresse = pAdresse;
            sVille = pVille;
            sCodePostal = pCodePostal;
            sNAS = pNAS;
            fSalaire = pSalaire;
            sTelephone = pTelephone;
        }
    }
}