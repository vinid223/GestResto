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
    public class CategorieViewModel : BaseViewModel
    {
        private ICategorieService _categService;


        private Categorie _categorie;

        public Categorie Categorie
        { 
            get 
            { 
                return _categorie; 
            } 
            
            set
            {
                RaisePropertyChanging();
                _categorie = value; 
                RaisePropertyChanged(); 
            } 
        }

        public CategorieViewModel()
        {
            _categService = ServiceFactory.Instance.GetService<ICategorieService>();
        }

        public IList<Categorie> ObtenirToutesLesCategories()
        {
            RetrieveCategorieArgs args = new RetrieveCategorieArgs();
            IList<Categorie> listeCateg = _categService.RetrieveAll();
            return listeCateg;
        }

        public void EnregistrerUneCategorie(Categorie categorie)
        {
            _categService.Update(categorie);
        }
    }
}
