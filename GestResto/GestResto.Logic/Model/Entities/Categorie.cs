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
    /// Classe Catégorie
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
        /// Constructeur par défaut de Catégorie
        /// </summary>
        public Categorie()
        {
            IdCategorie = null;
            Nom = "";
            EstActif = true;
            EstComplementaire = false;
        }

        /// <summary>
        /// Constructeur paramétré de Catégorie
        /// </summary>
        /// <param name="pIdCategorie">Id de la catégorie à créer</param>
        /// <param name="pNom">Nom de la catégorie</param>
        /// <param name="pEstActif">Statut de la catégorie par true ou false pour actif ou non</param>
        /// <param name="pEstComplementaire">Statut indiquant si la catégorie est une catégorie complémentaire par un true ou false</param>
        public Categorie(int pIdCategorie, string pNom, bool pEstActif, bool pEstComplementaire)
        {
            IdCategorie = pIdCategorie;
            Nom = pNom;
            EstActif = pEstActif;
            EstComplementaire = pEstComplementaire;
        }

        /// <summary>
        /// Constructeur paramétré de catégorie sans le paramètre de l'id puisque nous avons besoin de créer une catégorie sans d'id
        /// </summary>
        /// <param name="pNom">Nom de la catégorie</param>
        /// <param name="pEstActif">Statut de la catégorie par true ou false pour actif ou non</param>
        /// <param name="pEstComplementaire">Statut indiquant si la catégorie est une catégorie complémentaire par un true ou false</param>
        public Categorie(string pNom, bool pEstActif, bool pEstComplementaire)
        {
            Nom = pNom;
            EstActif = pEstActif;
            EstComplementaire = pEstComplementaire;
        }
        #endregion

        #region Redéfinition de fonctions de bases
        /// <summary>
        /// Permet de comparer deux catégories.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Categorie p = obj as Categorie;

            if (p != null && p.IdCategorie == -1)
                return true;

            if (p == null)
            {
                return false;
            }

            return this.IdCategorie == p.IdCategorie;
        }
        #endregion
    }
}