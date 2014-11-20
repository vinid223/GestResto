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
    public class RestaurantViewModel : BaseViewModel
    {
        private IRestaurantService _restoService;


        private Restaurant _restaurant;

        public Restaurant Restaurant
        {
            get
            {
                return _restaurant;
            }

            set
            {
                RaisePropertyChanging();
                _restaurant = value;
                RaisePropertyChanged();
            }
        }

        public RestaurantViewModel()
        {
            _restoService = ServiceFactory.Instance.GetService<IRestaurantService>();
            Restaurant = ObtenirRestaurant(1);
            Restaurant.EstModifie = false;
        }

        public Restaurant ObtenirRestaurant(int i)
        {
            RetrieveRestaurantArgs args = new RetrieveRestaurantArgs();
            args.IIdRestaurant = i;
            Restaurant restaurant = _restoService.Retrieve(args);
            return restaurant;
        }

        public void EnregistrerRestaurant(Restaurant restaurant)
        {          
                _restoService.Update(restaurant);
        }
    }
}
