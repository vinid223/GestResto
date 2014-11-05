using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestResto.Logic.Model.Entities;
using GestResto.Logic.Services.Definitions;
using GestResto.MvvmToolkit;
using GestResto.MvvmToolkit.Services;
using GestResto.Logic.Model.Args;

namespace GestResto.UI.ViewModel
{
    class FormatItemClientFactureViewModel : BaseViewModel
    {
        public IFormatItemClientFactureService _FormatItemClientFactureService;

        public FormatItemClientFacture _formatItemClientFacture;

        public FormatItemClientFacture FormatItemClientFacture
        {
            get
            {
                return _formatItemClientFacture;
            }
            set
            {
                _formatItemClientFacture = value;
                RaisePropertyChanged();
            }
        }

        public FormatItemClientFactureViewModel()
        {
            _FormatItemClientFactureService = ServiceFactory.Instance.GetService<IFormatItemClientFactureService>();
        }

        public IList<FormatItemClientFacture> ObtenirTousLesFormatItemClientFactures()
        {
            RetrieveFormatItemClientFactureArgs args = new RetrieveFormatItemClientFactureArgs();
            IList<FormatItemClientFacture> listeFormatItemClientFacture = _FormatItemClientFactureService.RetrieveAll();
            return listeFormatItemClientFacture;
        }

        public void EnregistrerUnFormatItemClientFacture(FormatItemClientFacture formatItemClientFacture)
        {
            try
            {
                _FormatItemClientFactureService.Update(formatItemClientFacture);
            }
            catch (NHibernate.Exceptions.GenericADOException adoE)
            {
                MySql.Data.MySqlClient.MySqlException mysqlExeception = (adoE.InnerException as MySql.Data.MySqlClient.MySqlException);
                throw mysqlExeception;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void EnregistrerTousLesFormatItemClientFacture(IList<FormatItemClientFacture> listeFormatItemClientFacture)
        {
            foreach (var FormatItemClientFacture in listeFormatItemClientFacture)
            {
                _FormatItemClientFactureService.Update(FormatItemClientFacture);
            }
        }

        public int AjouterUnFormatItemClientFacture(FormatItemClientFacture pFormatItemClientFacture)
        {
            // On insert l'enregistrement dans la base de donnée
            _FormatItemClientFactureService.Create(pFormatItemClientFacture);

            // On va chercher l'id lors de l'enregistrement
            int i;

            // Puisque l'id de l'objet est nullable on doit la transformer pour s'en servir
            i = pFormatItemClientFacture.IdFormatItemClientFacture ?? default(int);

            return i;
        }

        public void SupprimerUnFormatItemClientFacture(FormatItemClientFacture formatItemClientFacture)
        {
            _FormatItemClientFactureService.Delete(formatItemClientFacture);
        }

    }
}
