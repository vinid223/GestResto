using GestResto.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestResto.Logic.Model.Args;

namespace GestResto.Logic.Services.Definitions
{
    public interface ITableService
    {
        void Create(Table table);
        IList<Table> RetrieveAll();
        Table Retrieve(RetrieveTableArgs args);
        void Update(Table table);
    }
}
