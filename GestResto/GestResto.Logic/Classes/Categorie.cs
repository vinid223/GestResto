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

namespace GestResto.Logic.Classes
{
    /// <summary>
    /// Classe Cat�gorie
    /// </summary>
    public class Categorie
    {
        #region Liste des variables de la classe

        private int? idCategorie;
	    private string sNom;
	    private bool bActif;
	    private bool bComplementaire;

        #endregion

        #region Tous les constructeurs de la classe
        /// <summary>
        /// Constructeur par d�faut de Cat�gorie
        /// </summary>
        public Categorie()
        {
            idCategorie = null;
            sNom = "";
            bActif = true;
            bComplementaire = false;
        }

        /// <summary>
        /// Constructeur param�tr� de Cat�gorie
        /// </summary>
        /// <param name="pIdCategorie">Id de la cat�gorie � cr�er</param>
        /// <param name="pNom">Nom de la cat�gorie</param>
        /// <param name="pActif">Statut de la cat�gorie par true ou false pour actif ou non</param>
        /// <param name="pComplementaire">Statut indiquant si la cat�gorie est une cat�gorie compl�mentaire par un true ou false</param>
        public Categorie(int pIdCategorie, string pNom, bool pActif, bool pComplementaire)
        {
            idCategorie = pIdCategorie;
            sNom = pNom;
            bActif = pActif;
            bComplementaire = pComplementaire;
        }
        #endregion
    }
}