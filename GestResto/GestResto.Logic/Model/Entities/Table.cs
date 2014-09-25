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

	    private int? idTable;
	    private int? noTable;
	    private bool bActif;
	    private bool bAssigne;
	    private List<Client> ListeClients;

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur par defaut
        /// </summary>
        public Table()
        {
            idTable = null;
            noTable = null;
            bActif = true;
            bAssigne = true;
            ListeClients = new List<Client>();
        }

        /// <summary>
        /// Constructeur param�tr�
        /// </summary>
        /// <param name="pIdTable">id de la table</param>
        /// <param name="pNoTable">no de la table</param>
        /// <param name="pBActif">Indique si la table est active</param>
        /// <param name="pBAssigne">Indique si la table est assigne</param>
        /// <param name="pListeClient">Liste des clients � la table</param>
        public Table(int pIdTable, int pNoTable, bool pBActif, bool pBAssigne, List<Client> pListeClient)
        {
            idTable = pIdTable;
            noTable = pNoTable;
            bActif = pBActif;
            bAssigne = pBAssigne;
            ListeClients = pListeClient;
        }

        #endregion
    }

}