using GestResto.Logic.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Services.Definitions
{
    interface IFormatService
    {
        void Create(Format format);
        IList<Format> RetrieveAll();
        Format Retrieve(int pIdFormat);
        void Update(Format format);
        void Delete(Format format);
    }
}
