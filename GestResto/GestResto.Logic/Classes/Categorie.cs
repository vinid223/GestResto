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
    /// Classe Catégorie
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
        /// Constructeur par défaut de Catégorie
        /// </summary>
        public Categorie()
        {
            idCategorie = null;
            sNom = "";
            bActif = true;
            bComplementaire = false;
        }

        /// <summary>
        /// Constructeur paramétré de Catégorie
        /// </summary>
        /// <param name="pIdCategorie">Id de la catégorie à créer</param>
        /// <param name="pNom">Nom de la catégorie</param>
        /// <param name="pActif">Statut de la catégorie par true ou false pour actif ou non</param>
        /// <param name="pComplementaire">Statut indiquant si la catégorie est une catégorie complémentaire par un true ou false</param>
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