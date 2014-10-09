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

namespace GestResto.Logic.Model.Entities
{
    /// <summary>
    /// Classe Item
    /// </summary>
    public class Item
    {
        #region Variables de la classe
        public virtual int? IdItem { get; set; }
        public virtual string Nom { get; set; }
        public virtual Categorie Categories { get; set; }
        public virtual IList<FormatItem> Formats { get; set; }

       // public virtual List<Item> ListeComplements { get; set; }
        #endregion

        #region Constructeur de la classe Item
        /// <summary>
        /// Constructeur par d�faut d'item
        /// </summary>
        public Item()
        {
            IdItem = null;
            Nom = "";
            Formats = new List<FormatItem>();
            Categories = new Categorie();
          //  ListeComplements = new List<Item>();
        }

        /// <summary>
        /// Constructeur param�tr� de la classe Item
        /// </summary>
        /// <param name="pIdItem">Id de l'item</param>
        /// <param name="pNom">Nom de l'item</param>
        /// <param name="pFormatItem">Objet format de l'item</param>
        /// <param name="pCategories">Objet cat�grie de l'item</param>
        public Item(int pIdItem, string pNom, List<FormatItem> pFormatItem, Categorie pCategories)
            : this()
        {
            IdItem = pIdItem;
            Nom = pNom;
            Formats = pFormatItem;
            Categories = pCategories;
        }

        /// <summary>
        /// Permet de ins�r� des items dans la BD sans le ID.
        /// </summary>
        /// <param name="pNom">Nom de l'item</param>
        /// <param name="pFormatItem">Objet format de l'item</param>
        /// <param name="pCategories">Objet cat�grie de l'item</param>
        public Item(string pNom, List<FormatItem> pFormatItem, Categorie pCategories)
        {
            Nom = pNom;
            Formats = pFormatItem;
            Categories = pCategories;
        }

        #endregion
    }
}