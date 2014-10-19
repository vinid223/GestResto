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
    public class FormatItemViewModel : BaseViewModel
    {
        public IFormatItemService _FormatItemService;

        public FormatItem _formatItem;

        public FormatItem FormatItem
        { 
            get 
            { 
                return _formatItem; 
            } 
            
            set 
            { 
                _formatItem = value; 
                RaisePropertyChanged(); 
            } 
        }

        public FormatItemViewModel()
        {
            _FormatItemService = ServiceFactory.Instance.GetService<IFormatItemService>();
        }

        public IList<FormatItem> ObtenirTousLesFormatItems()
        {
            RetrieveFormatItemArgs args = new RetrieveFormatItemArgs();
            IList<FormatItem> listeFormatItem = _FormatItemService.RetrieveAll();
            return listeFormatItem;
        }

        public void EnregistrerTousLesFormatItems(IList<FormatItem> listeFormatItems)
        {
            foreach (var FormatItem in listeFormatItems)
	        {
                _FormatItemService.Update(FormatItem);
	        }
        }

        public int AjouterUnFormatItem(FormatItem pFormatItem)
        {
            // On insert l'enregistrement dans la base de donnée
            _FormatItemService.Create(pFormatItem);

            // On va chercher l'id lors de l'enregistrement
            int i;

            // Puisque l'id de l'objet est nullable on doit la transformer pour s'en servir
            i = pFormatItem.IdFormatItem ?? default(int);

            return i;
        }
    }
}
