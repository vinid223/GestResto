﻿using GestResto.Logic.Model.Args;
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
            Commandes = new ObservableCollection<Commande>(ServiceFactory.Instance.GetService<ICommandeService>().RetrieveAll(1));
        }


        #region Bindables
        
        private ObservableCollection<Categorie> _categories = new ObservableCollection<Categorie>();
        private ObservableCollection<Item> _items = new ObservableCollection<Item>();
        private ObservableCollection<Commande> _commandes = new ObservableCollection<Commande>();

        public Categorie _categorie;

        public ObservableCollection<Commande> Commandes
        {
            get { return _commandes; }
            set
            {
                _commandes = value;
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
