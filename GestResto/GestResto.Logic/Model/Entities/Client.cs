//
//
//  
//
//  Projet : GestResto
//  Nom du fichier : Client.cs
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
    /// Classe d'un client qui possède des items.
    /// </summary>
    public class Client
    {
        #region Variables de la classe

        public virtual uint? IdClient { get; set; }
        public virtual List<Item> ListeItems { get; set; }
        public virtual Facture facture { get; set; }

        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur par défaut du client, qui ajoute une nouvelle liste d'items vide au client.
        /// </summary>
        public Client()
        {
            IdClient = null;
            ListeItems = new List<Item>();
        }

        /// <summary>
        /// Constructeur paramétré.
        /// </summary>
        /// <param name="pIdClient">Ajoute le id au id du client.</param>
        /// <param name="pListeItems">La liste en paramètre est mise dans la liste du client.</param>
        public Client(uint pIdClient, List<Item> pListeItems)
        {
            IdClient = pIdClient;
            ListeItems = pListeItems;
        }

        #endregion
    }

}