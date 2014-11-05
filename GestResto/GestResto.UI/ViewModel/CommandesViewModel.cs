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

        public ObservableCollection<Commande> Commandes
        {
            get { return _commandes; }
            set
            {
                _commandes = value;
                RaisePropertyChanged();
            }
        }

        public CommandesViewModel()
        {
            Commandes = new ObservableCollection<Commande>(ServiceFactory.Instance.GetService<ICommandeService>().RetrieveAll(Constante.employe.IdEmploye.GetValueOrDefault()));
            _commandeServices = ServiceFactory.Instance.GetService<ICommandeService>();
        }
    }
}
