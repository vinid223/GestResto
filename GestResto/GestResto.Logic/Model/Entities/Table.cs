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
    public class Table : BaseEntity
    {
        #region Attributs

        public virtual int? IdTable { get; set; }
        private int _noTable;
        private bool _estActif;
        private bool _estAssigne;
        private IList<Client> _listeClient;
        private bool _estModifie;

        #endregion

        #region Propri�t�

        public virtual int NoTable
        {
            get
            {
                return _noTable;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _noTable = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual bool EstActif
        {
            get
            {
                return _estActif;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _estActif = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual bool EstAssigne
        {
            get
            {
                return _estAssigne;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _estAssigne = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual IList<Client> ListeClients
        {
            get
            {
                return _listeClient;
            }
            set
            {
                RaisePropertyChanging();
                RaisePropertyChanging("EstModifie");
                _listeClient = value;
                _estModifie = true;
                RaisePropertyChanged();
                RaisePropertyChanged("EstModifie");
            }
        }
        public virtual bool EstModifie
        {
            get
            {
                return _estModifie;
            }
            set
            {
                RaisePropertyChanging();
                _estModifie = value;
                RaisePropertyChanged();
            }
        }

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
            ListeClients = new List<Client>();
        }

        /// <summary>
        /// Constructeur param�tr�
        /// </summary>
        /// <param name="pIdTable">id de la table</param>
        /// <param name="pNoTable">no de la table</param>
        /// <param name="pEstActif">Indique si la table est active</param>
        /// <param name="pEstAssigne">Indique si la table est assigne</param>
        /// <param name="pListeClient">Liste des clients � la table</param>
        public Table(int pIdTable, int pNoTable, bool pEstActif, bool pEstAssigne, List<Client> pListeClient)
        {
            IdTable = pIdTable;
            NoTable = pNoTable;
            EstActif = pEstActif;
            EstAssigne = pEstAssigne;
            ListeClients = pListeClient;
        }

        #endregion
    }

}