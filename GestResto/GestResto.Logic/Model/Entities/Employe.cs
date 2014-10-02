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

namespace GestResto.Logic.Model.Entities
{
    /// <summary>
    /// Classe contenant toutes les informations d'un employé
    /// </summary>
    public class Employe
    {
        #region Variables appartenant à la classe Employe
        
        public virtual int? idEmploye;
        public virtual string sNom;
        public virtual string sPrenom;
        public virtual int noEmploye;
        public virtual string sMotDePasse;
        public virtual string sAdresse;
        public virtual string sVille;
        public virtual string sCodePostal;
        public virtual string sNAS;
        public virtual float fSalaire;
        public virtual string sTelephone;
        public virtual bool bActif;
        public virtual typeEmploye type;
        public virtual List<Commande> ListeCommandes;

        #endregion

        #region Tous les constructeurs de la classe Employes
        /// <summary>
        /// Constructeur par défaut de la classe Employé. Elle initialise toutes les variables pour une valeur null ou vide.
        /// </summary>
        public Employe()
        {
                idEmploye = null;
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
                type = null;
                ListeCommandes = new List<Commande>();
        }

        /// <summary>
        /// Constructeur paramétré de la classe employé
        /// </summary>
        /// <param name="pIdEmploye">id de l'employé</param>
        /// <param name="pNom">Nom de l'employé</param>
        /// <param name="pPrenom">Prenom de l'employé</param>
        /// <param name="pNoEmploye">Numero de l'employé</param>
        /// <param name="pMotPasse">Mot de passe de l'employé</param>
        /// <param name="pAdresse">Adresse de l'employé</param>
        /// <param name="pVille">Ville de l'employé</param>
        /// <param name="pCodePostal">Code postal de l'employé</param>
        /// <param name="pNAS">NAS de l'employé</param>
        /// <param name="pSalaire">Salaire de l'employé</param>
        /// <param name="pTelephone">Telephone de l'employé</param>
        Employe(int pIdEmploye, string pNom, string pPrenom, int pNoEmploye, string pMotPasse, string pAdresse, string pVille, string pCodePostal, string pNAS, float pSalaire, string pTelephone, bool pActif, typeEmploye pType)
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
            bActif = pActif;
            type = pType;
        }

        #endregion
    }
}