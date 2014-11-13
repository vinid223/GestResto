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

namespace GestResto.Logic.Model.Entities
{
    /// <summary>
    /// Classe facture qui permet d'enregistrer la liste d'items que le client a command�.
    /// On enregistre le pourcentage de la taxe au cas o� elle a chang�.
    /// </summary>
    public class Facture
    {
        #region Variables de la classe

        public virtual uint? IdFacture { get; set; }
        //public virtual DateTime DateCreation { get; set; }
        public virtual float PourcentageTaxe { get; set; }
        public virtual Commande CommandeClient { get; set; }
        public virtual IList<FormatItemClientFacture> ListeFormatItemClientFacture { get; set; }

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur par d�faut de la classe facture.
        /// </summary>
        public Facture()
        {
            IdFacture = null;
            //DateCreation = new DateTime();
            PourcentageTaxe = 0;
            ListeFormatItemClientFacture = new List<FormatItemClientFacture>();
        }

        /// <summary>
        /// Constructeur param�tr� de la classe facture.
        /// </summary>
        /// <param name="pIdFacture">id de la facture en param�tre.</param>
        /// <param name="pDateCreation">La date de la cr�ation de la facture.</param>
        /// <param name="pPourcentageTaxe">Le pourcentage de la facture lors de la date de cr�ation.</param>
        /// <param name="pListeItems">Liste d'items de la facture.</param>
        public Facture(uint pIdFacture, DateTime pDateCreation, float pPourcentageTaxe, List<FormatItemClientFacture> pListeItems)
        {
            IdFacture = pIdFacture;
            //DateCreation = pDateCreation;
            PourcentageTaxe = pPourcentageTaxe;
            ListeFormatItemClientFacture = pListeItems;
        }

        #endregion

    }
}