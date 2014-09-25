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

namespace GestResto.Logic.Classes
{
    /// <summary>
    /// La classe format est généralement associé à un item,
    /// un format permet à l'utilisateur de savoir le prix de l'item,
    /// il peut avoir 1 format minimum et plusieurs qui sera choisi lors de la facture.
    /// </summary>
    public class Format
    {
        #region Variables de la classe

        private int? idFormat ;
	    private string sNom ;
	    private string sLibele ;
	    private float fPrix ;

        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur par défaut qui met le id null et les autres champs vides.
        /// </summary>
        public Format()
        {
            idFormat = null;
            sNom = "";
            sLibele = "";
            fPrix = 0;
        }

        /// <summary>
        /// Constructeur paramétré qui met tous les paramètres dans les attributs de l'objet.
        /// </summary>
        /// <param name="pidFormat">Le id du format en paramètre</param>
        /// <param name="pNom">Le nom du format en paramètre.</param>
        /// <param name="pLibele">Le libelé en paramètre.</param>
        /// <param name="pPrix">Le prix en paramètre.</param>
        public Format(int pidFormat, string pNom, string pLibele, float pPrix)
        {
            idFormat = pidFormat;
            sNom = pNom;
            sLibele = pLibele;
            fPrix = pPrix;
        }

        #endregion
    }
}