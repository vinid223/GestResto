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

	    private int? idCommande;
	    private string Statut;
	    private DateTime Debut;
	    private DateTime? Fin;
	    private List<Table> ListeTables;
	    private List<Facture> ListeFactures;

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur par défaut de commande. Nouvelle commande
        /// </summary>
        public Commande()
        {
            idCommande = null;
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
            idCommande = pIdCommande;
            Statut = pStatut;
            Debut = pDebut;
            Fin = pFin;
            ListeTables = pListeTable;
            ListeFactures = pListeFactures;
        }

        #endregion
    }
}