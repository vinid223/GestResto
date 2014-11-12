using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GestResto.Logic.Model.Entities
{
    public class BaseEntity : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;

        public virtual PropertyChangedEventHandler PropertyChangedHandler
        {
            get { return PropertyChanged; }
        }

        public virtual event PropertyChangingEventHandler PropertyChanging;

        public virtual PropertyChangingEventHandler PropertyChangingHandler
        {
            get { return PropertyChanging; }
        }


        public virtual void RaisePropertyChanging([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanging;
            if (handler != null)
            {
                handler(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        public virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
