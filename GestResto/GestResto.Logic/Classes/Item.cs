//
//
//  
//
//  Projet : GestResto
//  Nom du fichier : Item.cs
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
    /// Classe Item
    /// </summary>
    public class Item
    {
        #region Variables de la classe
        private int? idItem;
	    private string sNom;
	    private Format FormatItem;
	    private Categorie Categories;
	    private List<Item> ListeComplements;
        #endregion

        #region Constructeur de la classe Item
        /// <summary>
        /// Constructeur par défaut d'item
        /// </summary>
        public Item()
        {
            idItem = null;
            sNom = "";
            FormatItem = new Format();
            Categories = new Categorie();
            ListeComplements = new List<Item>();
        }

        /// <summary>
        /// Constructeur paramétré de la classe Item
        /// </summary>
        /// <param name="pIdItem">Id de l'item</param>
        /// <param name="pNom">Nom de l'item</param>
        /// <param name="pFormatItem">Objet format de l'item</param>
        /// <param name="pCategories">Objet catégrie de l'item</param>
        public Item(int pIdItem, string pNom, Format pFormatItem, Categorie pCategories)
        {
            idItem = pIdItem;
            sNom = pNom;
            FormatItem = pFormatItem;
            Categories = pCategories;
            ListeComplements = new List<Item>();
        }
        #endregion
    }
}