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
    public class ItemsViewModel : BaseViewModel
    {
        public IItemService _itemService;

        public ItemsViewModel()
        {
            Formats = new ObservableCollection<Format>(ServiceFactory.Instance.GetService<IFormatService>().RetrieveAll());
            Items = new ObservableCollection<Item>(ServiceFactory.Instance.GetService<IItemService>().RetrieveAll());
            Categories = new ObservableCollection<Categorie>(ServiceFactory.Instance.GetService<ICategorieService>().RetrieveAll());
            categsTest = new ObservableCollection<Categorie>(ServiceFactory.Instance.GetService<ICategorieService>().RetrieveAll());
            
            // Insère une catégorie seulement dans la liste en mémoire.
            categTest = new Categorie(-1, "Tous les items", true, false);
            categsTest.Insert(0, categTest);

            _itemService = ServiceFactory.Instance.GetService<IItemService>();
        }


        #region Bindables

        private ObservableCollection<Format> _formats = new ObservableCollection<Format>();
        private ObservableCollection<Categorie> _categories = new ObservableCollection<Categorie>();
        private ObservableCollection<Categorie> _categoriesTest = new ObservableCollection<Categorie>();
        private ObservableCollection<Item> _items = new ObservableCollection<Item>();

        public Categorie categTest;

        public ObservableCollection<Item> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Categorie> categsTest
        {
            get { return _categoriesTest; }
            set
            {
                _categoriesTest = value;
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

        public ObservableCollection<Format> Formats
        {
            get{ return _formats; }
            set
            {
                _formats = value;
                RaisePropertyChanged();
            }
        }

        public Item _item;

        public Item Item
        {
            get{ return _item; }
            set
            {
                _item = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        public IList<Item> ObtenirTousLesItems()
        {
            RetrieveItemArgs args = new RetrieveItemArgs();
            IList<Item> listeItem = _itemService.RetrieveAll();
            return listeItem;
        }

        public IList<Item> ObtenirTousLesItemsDeLaCategorie(Categorie categorie)
        {
            RetrieveItemArgs args = new RetrieveItemArgs();
            IList<Item> listeItem = _itemService.RetrieveAll();
            IList<Item> listeItemVerifiee = new List<Item>();

            if (categorie.Nom == "Tous les items")
            {
                listeItemVerifiee = listeItem;
            }
            else
            {
                // Parcours tous les items et vérifie si la catégorie de l'item correspond à la catégorie choisie.
                foreach (var item in listeItem)
                {
                    if (item.Categories != null && item.Categories.IdCategorie == categorie.IdCategorie)
                    {
                        listeItemVerifiee.Add(item);
                    }
                }
            }

            return listeItemVerifiee;
        }

        public void EnregistrerTousLesItems(IList<Item> listeItems)
        {
            foreach (var categorie in listeItems)
	        {
                _itemService.Update(Item);
	        }
        }


        public void EnregistrerUnItem(Item item)
        {

            item.Formats.Where(v => v.ItemAssocie == null).ToList().ForEach(v => v.ItemAssocie = item);

            _itemService.Update(item);

        }

        public int AjouterUnItem(Item pItem)
        {
            // On insert l'enregistrement dans la base de donnée
            _itemService.Create(pItem);

            // On va chercher l'id lors de l'enregistrement
            int i;

            // Puisque l'id de l'objet est nullable on doit la transformer pour s'en servir
            i = pItem.IdItem ?? default(int);

            return i;
        }

        public void DeleteFormatItem(FormatItem formatitem)
        {

            Item.Formats.Remove(formatitem);
            _itemService.Delete(formatitem);
        }

        public void ClearSession()
        {
            _itemService.ClearSession();
        }

    }
}
