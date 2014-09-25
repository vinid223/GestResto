//
//
//  
//
//  Projet : GestResto
//  Nom du fichier : Facture.cs
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
    /// Classe facture qui permet d'enregistrer la liste d'items que le client a command�.
    /// On enregistre le pourcentage de la taxe au cas o� elle a chang�.
    /// </summary>
    public class Facture
    {
        #region Variables de la classe

        private uint? idFacture;
        private DateTime DateCreation;
        private float fPourcentageTaxe;
        private List<Item> ListeItems;

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur par d�faut de la classe facture.
        /// </summary>
        public Facture()
        {
            idFacture = null;
            DateCreation = new DateTime();
            fPourcentageTaxe = 0;
            ListeItems = new List<Item>();
        }

        /// <summary>
        /// Constructeur param�tr� de la classe facture.
        /// </summary>
        /// <param name="pidFacture">id de la facture en param�tre.</param>
        /// <param name="pDateCreation">La date de la cr�ation de la facture.</param>
        /// <param name="pPourcentageTaxe">Le pourcentage de la facture lors de la date de cr�ation.</param>
        /// <param name="pListeItems">Liste d'items de la facture.</param>
        public Facture(uint pidFacture, DateTime pDateCreation, float pPourcentageTaxe, List<Item> pListeItems)
        {
            idFacture = pidFacture;
            DateCreation = pDateCreation;
            fPourcentageTaxe = pPourcentageTaxe;
            ListeItems = pListeItems;
        }

        #endregion

    }
}