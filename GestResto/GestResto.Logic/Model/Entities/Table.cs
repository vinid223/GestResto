//
//
//  
//
//  Projet : GestResto
//  Nom du fichier : Table.cs
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
    /// Classe contenant toutes les informations d'une table
    /// </summary>
    public class Table 
    {
        #region Attributs

        public virtual int? IdTable { get; set; }
        public virtual int NoTable { get; set; }
        public virtual bool EstActif { get; set; }
        public virtual bool EstAssigne { get; set; }
        //public virtual List<Client> ListeClients { get; set; }

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur par defaut
        /// </summary>
        public Table()
        {
            IdTable = null;
            NoTable = 0;
            EstActif = true;
            EstAssigne = true;
            //ListeClients = new List<Client>();
        }

        /// <summary>
        /// Constructeur paramétré
        /// </summary>
        /// <param name="pIdTable">id de la table</param>
        /// <param name="pNoTable">no de la table</param>
        /// <param name="pEstActif">Indique si la table est active</param>
        /// <param name="pEstAssigne">Indique si la table est assigne</param>
        /// <param name="pListeClient">Liste des clients à la table</param>
        public Table(int pIdTable, int pNoTable, bool pEstActif, bool pEstAssigne, List<Client> pListeClient)
        {
            IdTable = pIdTable;
            NoTable = pNoTable;
            EstActif = pEstActif;
            EstAssigne = pEstAssigne;
            //ListeClients = pListeClient;
        }

        #endregion
    }

}