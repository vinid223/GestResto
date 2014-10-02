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
    /// Classe TypeEmploye
    /// </summary>
    public class TypeEmploye
    {

        #region Liste des variables de la classe

        public virtual int? IdTypeEmploye { get; set; }
        public virtual string NomType { get; set; }
        #endregion

        #region Tous les constructeurs de la classe
        /// <summary>
        /// Constructeur par défaut de TypeEmploye
        /// </summary>
        public TypeEmploye()
        {
            IdTypeEmploye = null;
            NomType = "";
        }

        /// <summary>
        /// Constructeur paramétré de TypeEmploye
        /// </summary>
        /// <param name="pIdEmploye">Id du type d'employé</param>
        /// <param name="pNomType">Le nom du type d'employé</param>
        public TypeEmploye(int pIdEmploye, string pNomType)
        {
            IdTypeEmploye = pIdEmploye;
            NomType = pNomType;
        }

        #endregion
    }
}
