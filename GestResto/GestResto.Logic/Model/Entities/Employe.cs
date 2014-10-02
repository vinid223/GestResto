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
    /// Classe contenant toutes les informations d'un employ�
    /// </summary>
    public class Employe
    {
        #region Variables appartenant � la classe Employe

        public virtual int? IdEmploye { get; set; }
        public virtual string Nom { get; set; }
        public virtual string Prenom { get; set; }
        public virtual int NoEmploye { get; set; }
        public virtual string MotDePasse { get; set; }
        public virtual string Adresse { get; set; }
        public virtual string Ville { get; set; }
        public virtual string CodePostal { get; set; }
        public virtual string NAS { get; set; }
        public virtual float Salaire { get; set; }
        public virtual string Telephone { get; set; }
        public virtual bool EstActif { get; set; }
        public virtual TypeEmploye Type { get; set; }
        public virtual List<Commande> ListeCommandes { get; set; }

        #endregion

        #region Tous les constructeurs de la classe Employes
        /// <summary>
        /// Constructeur par d�faut de la classe Employ�. Elle initialise toutes les variables pour une valeur null ou vide.
        /// </summary>
        public Employe()
        {
                IdEmploye = null;
                Nom = "";
                Prenom = "";
                NoEmploye = 0;
                MotDePasse = "";
                Adresse = "";
                Ville = "";
                CodePostal = "";
                NAS = "";
                Salaire = 0;
                Telephone = "";
                EstActif = false;
                Type = null;
                ListeCommandes = new List<Commande>();
        }

        /// <summary>
        /// Constructeur param�tr� de la classe employ�
        /// </summary>
        /// <param name="pIdEmploye">id de l'employ�</param>
        /// <param name="pNom">Nom de l'employ�</param>
        /// <param name="pPrenom">Prenom de l'employ�</param>
        /// <param name="pNoEmploye">Numero de l'employ�</param>
        /// <param name="pMotPasse">Mot de passe de l'employ�</param>
        /// <param name="pAdresse">Adresse de l'employ�</param>
        /// <param name="pVille">Ville de l'employ�</param>
        /// <param name="pCodePostal">Code postal de l'employ�</param>
        /// <param name="pNAS">NAS de l'employ�</param>
        /// <param name="pSalaire">Salaire de l'employ�</param>
        /// <param name="pTelephone">Telephone de l'employ�</param>
        Employe(int pIdEmploye, string pNom, string pPrenom, int pNoEmploye, string pMotPasse, string pAdresse, string pVille, string pCodePostal, string pNAS, float pSalaire, string pTelephone, bool pEstActif, TypeEmploye pType)
        {
            IdEmploye = pIdEmploye;
            Nom = pNom;
            Prenom = pPrenom;
            NoEmploye = pNoEmploye;
            MotDePasse = pMotPasse;
            Adresse = pAdresse;
            Ville = pVille;
            CodePostal = pCodePostal;
            NAS = pNAS;
            Salaire = pSalaire;
            Telephone = pTelephone;
            EstActif = pEstActif;
            Type = pType;
        }

        #endregion
    }
}