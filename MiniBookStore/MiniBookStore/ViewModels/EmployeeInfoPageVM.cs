using MiniBookStore.Models;
using MiniBookStore.Models.MyClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace MiniBookStore.ViewModels
{
    public class EmployeeInfoPageVM:BaseViewModel
    {
        #region data binding

        private CEmployee _Employee;
        public CEmployee Employee
        {
            get { return _Employee; }
            set
            {
                _Employee = value;
                OnPropertyChanged(nameof(Employee));
            }
        }

        #endregion

        #region command binding

        public ICommand LoadCommand { get; set; }
        public ICommand ChangePassCommand { get; set; }

        #endregion

        public EmployeeInfoPageVM()
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Employee = DataTransfer.EmployeeInfo;
                
            }
               );

            ChangePassCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ChangePassWordWindow wd = new ChangePassWordWindow();
                wd.ShowDialog();
            }
               );
        }
    }
}
