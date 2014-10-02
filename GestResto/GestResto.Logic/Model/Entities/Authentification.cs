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
    /// Classe Authentification
    /// </summary>
    public class Authentification
    {
        #region Variables de la classe

        public virtual Employe Employe { get; set; }

        #endregion

        #region Tous les constructeurs de la classe
        /// <summary>
        /// Constructeur par défaut de Authentification
        /// </summary>
        public Authentification()
        {
            Employe = new Employe();
        }

        /// <summary>
        /// Constructeur paramétré de Authentification
        /// </summary>
        /// <param name="pEmploye">L'Employé à authentifier</param>
        public Authentification(Employe pEmploye)
        {
            Employe = pEmploye;
        }
        #endregion
    }
}
