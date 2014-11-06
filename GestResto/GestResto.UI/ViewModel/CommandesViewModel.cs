using GestResto.Logic.Model.Args;
using GestResto.Logic.Model.Entities;
using GestResto.Logic.Services.Definitions;
using GestResto.MvvmToolkit;
using GestResto.MvvmToolkit.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GestResto.UI.ViewModel
{
    public class CommandesViewModel : BaseViewModel
    {
        private ICommandeService _commandeServices;
        private ObservableCollection<Commande> _commandes = new ObservableCollection<Commande>();
        private ObservableCollection<Table> _tables = new ObservableCollection<Table>();

        public ObservableCollection<Commande> Commandes
        {
            get { return _commandes; }
            set
            {
                _commandes = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Table> Tables
        {
            get { return _tables; }
            set
            {
                _tables = value;
                RaisePropertyChanged();
            }
        }

        public CommandesViewModel()
        {
            Tables = new ObservableCollection<Table>(ServiceFactory.Instance.GetService<ITableService>().RetrieveAll());
            Commandes = new ObservableCollection<Commande>(ServiceFactory.Instance.GetService<ICommandeService>().RetrieveAll(Constante.employe.IdEmploye.GetValueOrDefault()));
            _commandeServices = ServiceFactory.Instance.GetService<ICommandeService>();
        }

        public Commande CreerCommande(Commande commande)
        {
            try
            {
                // On insert l'enregistrement dans la base de donnée
                _commandeServices.Create(commande);
            }
            catch (NHibernate.Exceptions.GenericADOException adoE)
            {
                MySql.Data.MySqlClient.MySqlException mysqlExeception = (adoE.InnerException as MySql.Data.MySqlClient.MySqlException);
                throw mysqlExeception;
            }
            catch (Exception e)
            {
                throw e;
            }

            return commande;
        }
    }
}
