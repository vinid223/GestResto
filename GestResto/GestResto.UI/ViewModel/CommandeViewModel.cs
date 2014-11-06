using GestResto.Logic.Model.Args;
using GestResto.Logic.Model.Entities;
using GestResto.Logic.Services.Definitions;
using GestResto.Logic.Services.Helpers;
using GestResto.MvvmToolkit;
using GestResto.MvvmToolkit.Services;
using GestResto.MvvmToolkit.Services.Definitions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GestResto.UI.ViewModel
{
    public class CommandeViewModel : BaseViewModel
    {
        public ICommandeService _commandeService;

        public CommandeViewModel()
        {

            Categories = new ObservableCollection<Categorie>(ServiceFactory.Instance.GetService<ICategorieService>().RetrieveAll());
            Items = new ObservableCollection<Item>(ServiceFactory.Instance.GetService<IItemService>().RetrieveAll());
            Clients = new ObservableCollection<Client>(ServiceFactory.Instance.GetService<IClientService>().RetrieveAll());
        }


        #region Bindables
        
        private ObservableCollection<Categorie> _categories = new ObservableCollection<Categorie>();
        private ObservableCollection<Item> _items = new ObservableCollection<Item>();
        private ObservableCollection<Client> _clients = new ObservableCollection<Client>();

        public Categorie _categorie;

        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Item> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Categorie> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                RaisePropertyChanged();
            }
        }

        public Categorie Categorie
        {
            get { return _categorie; }
            set
            {
                _categorie = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        

    }
}
