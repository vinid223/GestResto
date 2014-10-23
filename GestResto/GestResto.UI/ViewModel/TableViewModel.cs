using GestResto.Logic.Model.Args;
using GestResto.Logic.Model.Entities;
using GestResto.Logic.Services.Definitions;
using GestResto.Logic.Services.Helpers;
using GestResto.MvvmToolkit;
using GestResto.MvvmToolkit.Services;
using GestResto.MvvmToolkit.Services.Definitions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GestResto.UI.ViewModel
{
    public class TableViewModel : BaseViewModel
    {
        public ITableService _tableService;

        public TableViewModel()
        {
            Tables = new ObservableCollection<Table>(ServiceFactory.Instance.GetService<ITableService>().RetrieveAll());

            _tableService = ServiceFactory.Instance.GetService<ITableService>();
        }

        #region Bindables

        private ObservableCollection<Table> _tables = new ObservableCollection<Table>();
        private Table _table;

        public Table table
        {
            get { return _table; }
            set
            {
                _table = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Table> Tables
        {
            get { return _tables; }
            set
            {
                _tables = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        public int AjouterUneTable(Table table)
        {
            _tableService.Create(table);

            // On va chercher l'id de l'enregistrement.
            int i = table.IdTable ?? default(int);

            return i;
        }

        public void EnregistrerTable(Table table)
        {
            _tableService.Update(table);
        }

    }
}
