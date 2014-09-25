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
    /// Classe facture qui permet d'enregistrer la liste d'items que le client a commandé.
    /// On enregistre le pourcentage de la taxe au cas où elle a changé.
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

        public Facture()
        {
            idFacture = null;
            DateCreation = new DateTime();
        }

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