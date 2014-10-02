using GestResto.Logic.Model.Args;
using GestResto.Logic.Model.Entities;
using GestResto.Logic.Services.Defenitions;
using GestResto.MvvmToolkit.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.UI.ViewModel
{
    public class CategorieViewModel
    {
        private ICategorieService _categService;
        public CategorieViewModel()
        {
            _categService = ServiceFactory.Instance.GetService<ICategorieService>();
        }

        public void SauvegarderCategorie()
        {

            RetrieveCategorieArgs args = new RetrieveCategorieArgs();

            args.idCategorie = 2;

            Categorie test = _categService.Retrieve(args);

            int nsfsdf = 0;
        }
    }
}
