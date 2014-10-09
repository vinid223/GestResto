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
    public class ItemsViewModel : BaseViewModel
    {
        public IItemService _itemService;

        public Item _item;

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

        public IList<Item> ObtenirTousLesItems()
        {
            RetrieveItemArgs args = new RetrieveItemArgs();
            IList<Item> listeItem = _itemService.RetrieveAll();
            return listeItem;
        }

        public void EnregistrerTousLesItems(IList<Item> listeItems)
        {
            foreach (var categorie in listeItems)
	        {
                _itemService.Update(Item);
	        }
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

    }
}
