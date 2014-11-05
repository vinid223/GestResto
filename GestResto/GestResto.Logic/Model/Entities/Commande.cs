//
//
//  
//
//  Projet : GestResto
//  Nom du fichier : Commande.cs
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
    /// Classe contenant toutes les informations d'une commande
    /// </summary>
    public class Commande
    {
        #region Attributs

        public virtual int? IdCommande { get; set; }
        public virtual string Statut { get; set; }
        public virtual int IdEmploye { get; set; }
        public virtual DateTime Debut { get; set; }
        public virtual DateTime? Fin { get; set; }
        public virtual IList<Table> ListeTables { get; set; }
        public virtual IList<Facture> ListeFactures { get; set; }

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur par défaut de commande. Nouvelle commande
        /// </summary>
        public Commande()
        {
            IdCommande = null;
            Statut = "Active";
            Debut = DateTime.Now;
            Fin = null;
            ListeTables = new List<Table>();
            ListeFactures = new List<Facture>();   
        }

        /// <summary>
        /// Constructeur paramétré de commande.
        /// 
        /// </summary>
        /// <param name="pIdCommande">L'id de la commande</param>
        /// <param name="pStatut">Le statut de la commande</param>
        /// <param name="pListeTable">La liste des tables</param>
        public Commande(int pIdCommande, string pStatut, DateTime pDebut, DateTime pFin, List<Table> pListeTable, List<Facture> pListeFactures)
        {
            IdCommande = pIdCommande;
            Statut = pStatut;
            Debut = pDebut;
            Fin = pFin;
            ListeTables = pListeTable;
            ListeFactures = pListeFactures;
        }

        #endregion
    }
}