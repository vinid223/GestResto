﻿using GestResto.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Services.Definitions
{
    interface IRestaurantService
    {
        void Create(Restaurant item);
        Restaurant Retrieve(int pIdRestaurant);
        void Update(Restaurant item);
    }
}