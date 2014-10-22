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
    public class FormatViewModel : BaseViewModel
    {
        private IFormatService _formatService;
        private ObservableCollection<Format> _formats = new ObservableCollection<Format>();

        public ObservableCollection<Format> Formats
        {
            get { return _formats; }
            set
            {
                _formats = value;
                RaisePropertyChanged();
            }
        }

        private Format _format;

        public Format Format
        {
            get
            {
                return _format;
            }
        
            set
            {
                _format = value;
                RaisePropertyChanged();
            }
        }

        public FormatViewModel()
        {
            Formats = new ObservableCollection<Format>(ServiceFactory.Instance.GetService<IFormatService>().RetrieveAll());
            _formatService = ServiceFactory.Instance.GetService<IFormatService>();
        }

        public IList<Format> ObtenirTousLesFormats()
        { 
            RetrieveFormatArgs args = new RetrieveFormatArgs();
            IList<Format> listeFormat = _formatService.RetrieveAll();
            return listeFormat;
        }

        public void EnregistrerUnFormat(Format format)
        {
            try
            {
                _formatService.Update(format);
            }
            catch (NHibernate.Exceptions.GenericADOException adoE)
            {
               MySql.Data.MySqlClient.MySqlException mysqlExeception = (adoE.InnerException as MySql.Data.MySqlClient.MySqlException);
               throw mysqlExeception;
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        public int AjouterUnFormat(Format format)
        {
            // On insert l'enregistrement dans la base de donnée
            _formatService.Create(format);

            // On va chercher l'id lors de l'enregistrement
            int i;

            // Puisque l'id de l'objet est nullable on doit la transformer pour s'en servir
            i = format.IdFormat ?? default(int);

            return i;
        }

    }
}
