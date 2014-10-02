//
//
//  
//
//  Projet : GestResto
//  Nom du fichier : Format.cs
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
    /// La classe format est g�n�ralement associ� � un item,
    /// un format permet � l'utilisateur de savoir le prix de l'item,
    /// il peut avoir 1 format minimum et plusieurs qui sera choisi lors de la facture.
    /// </summary>
    public class Format
    {
        #region Variables de la classe

        public virtual int? IdFormat { get; set; }
        public virtual string Nom { get; set; }
        public virtual string Libelle { get; set; }
        public virtual float Prix { get; set; }

        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur par d�faut qui met le id null et les autres champs vides.
        /// </summary>
        public Format()
        {
            IdFormat = null;
            Nom = "";
            Libelle = "";
            Prix = 0;
        }

        /// <summary>
        /// Constructeur param�tr� qui met tous les param�tres dans les attributs de l'objet.
        /// </summary>
        /// <param name="pIdFormat">Le id du format en param�tre</param>
        /// <param name="pNom">Le nom du format en param�tre.</param>
        /// <param name="pLibele">Le libel� en param�tre.</param>
        /// <param name="pPrix">Le prix en param�tre.</param>
        public Format(int pIdFormat, string pNom, string pLibelle, float pPrix)
        {
            IdFormat = pIdFormat;
            Nom = pNom;
            Libelle = pLibelle;
            Prix = pPrix;
        }

        #endregion
    }
}