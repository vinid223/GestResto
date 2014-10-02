using GestResto.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Services.Definitions
{
    public interface IItemService
    {
        void Create(Item item);
        IList<Item> RetrieveAll();
        Item Retrieve(int pIdItem);
        void Update(Item item);
    }
}
