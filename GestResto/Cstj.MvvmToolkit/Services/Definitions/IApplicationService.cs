using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cstj.MvvmToolkit.Services.Definitions
{
    public interface IApplicationService
    {
        void ChangeView<T>(T view);
        
    }
}
