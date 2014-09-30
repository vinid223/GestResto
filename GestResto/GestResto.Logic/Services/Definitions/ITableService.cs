using GestResto.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Services.Definitions
{
    public interface ITableService
    {
        void Create(Table table);
        IList<Table> RetrieveAll();
        Table Retrieve(int pIdTable);
        void Update(Table table);
        void Delete(Table table);
    }
}
