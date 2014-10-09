﻿using GestResto.Logic.Model.Args;
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
    public class RestaurantViewModel
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
            _restaurant = ServiceFactory.Instance.GetService<IRestaurantService>();
        }

        public Restaurant ObtenirRestaurant()
        {
            RetrieveRestaurantArgs args = new RetrieveRestaurantArgs();
            args.IIdRestaurant = 1;
            Restaurant restaurant = _restoService.Retrieve(args.IIdRestaurant);
            return listeCateg;
        }
    }
}
