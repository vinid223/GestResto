using GestResto.Logic.Model.Args;
using GestResto.Logic.Model.Entities;
using GestResto.Logic.Services.Definitions;
using GestResto.MvvmToolkit;
using GestResto.MvvmToolkit.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.UI.ViewModel
{
    class ItemsViewModel : BaseViewModel
    {
        private IItemService _itemService;

        private Item _item;

        public Item Item
        { 
            get 
            { 
                return _item; 
            } 
            
            set 
            { 
                _item = value; 
                RaisePropertyChanged(); 
            } 
        }

        public ItemsViewModel()
        {
            _itemService = ServiceFactory.Instance.GetService<IItemService>();
        }

        public IList<Item> ObtenirToutesLesItems()
        {
            RetrieveItemArgs args = new RetrieveItemArgs();
            IList<Item> listeItem = _itemService.RetrieveAll();
            return listeItem;
        }

        public void EnregistrerToutesLesItems(IList<Categorie> listeItems)
        {
            foreach (var categorie in listeItems)
	        {
                _itemService.Update(Item);
	        }
        }

    }
}
