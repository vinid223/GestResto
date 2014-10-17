using GestResto.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestResto.Logic.Model.Args;

namespace GestResto.Logic.Services.Definitions
{
    /// <summary>
    /// Interface permettant de définir les fonctions du service Restaurant
    /// </summary>
    public interface IRestaurantService
    {
        Restaurant Retrieve(RetrieveRestaurantArgs args);
        void Update(Restaurant item);
    }
}
