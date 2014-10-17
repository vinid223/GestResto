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
    /// Interface permettant de définir les fonctions du service Item
    /// </summary>
    public interface IItemService
    {
        void Create(Item item);
        IList<Item> RetrieveAll();
        Item Retrieve(RetrieveItemArgs args);
        void Update(Item item);
    }
}
