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

        public virtual int? IdClient { get; set; }
        public virtual IList<FormatItemClientFacture> ListeFormatItemClientFacture { get; set; }
        public virtual Commande CommandeClient { get; set; }
        public virtual Facture FactureClient { get; set; }
        public virtual Table TableClient { get; set; }

        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur par défaut du client, qui ajoute une nouvelle liste d'items vide au client.
        /// </summary>
        public Client()
        {
            IdClient = null;
            ListeFormatItemClientFacture = new List<FormatItemClientFacture>();
            FactureClient = null;
            CommandeClient = new Commande();
            TableClient = new Table();
        }

        /// <summary>
        /// Constructeur paramétré.
        /// </summary>
        /// <param name="pIdClient">Ajoute le id au id du client.</param>
        /// <param name="pListeItems">La liste en paramètre est mise dans la liste du client.</param>
        public Client(int pIdClient, List<FormatItemClientFacture> pListeFormatItemClientFacture)
        {
            IdClient = pIdClient;
            ListeFormatItemClientFacture = pListeFormatItemClientFacture;
        }

        #endregion
    }

}