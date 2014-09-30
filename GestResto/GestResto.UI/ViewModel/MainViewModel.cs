using GestResto.MvvmToolkit;
using GestResto.MvvmToolkit.Services.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GestResto.UI.ViewModel
{
    public class MainViewModel : BaseViewModel, IApplicationService
    {
        public MainViewModel()
        {

        }

        private UserControl _currentView;

        public UserControl CurrentView
        {
            get
            {
                return _currentView;
            }

            set
            {
                if (_currentView == value)
                {
                    return;
                }

                RaisePropertyChanging();
                _currentView = value;
                RaisePropertyChanged();
            }
        }


        public void ChangeView<T>(T view)
        {
            CurrentView = view as UserControl;
        }
    }
}
