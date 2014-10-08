using GestResto.Logic.Model.Args;
using GestResto.Logic.Model.Entities;
using GestResto.Logic.Services.Definitions;
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
    class ItemsViewModel
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
            _categService = ServiceFactory.Instance.GetService<ICategorieService>();
        }

        public IList<Categorie> ObtenirToutesLesCategories()
        {
            RetrieveCategorieArgs args = new RetrieveCategorieArgs();
            IList<Categorie> listeCateg = _categService.RetrieveAll();
            return listeCateg;
        }

        public void EnregistrerToutesLesCategories(IList<Categorie> listeCategorie)
        {
            foreach (var categorie in listeCategorie)
	        {
                _categService.Update(categorie);
	        }
        }

    }
}
