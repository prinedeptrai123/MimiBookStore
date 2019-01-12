using MiniBookStore.Models.MyClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniBookStore.ViewModels
{
    public class ReportMonthPageVM:BaseViewModel
    {
        #region data binding

        private ObservableCollection<int> _listYear;
        public ObservableCollection<int> ListYear { get => _listYear; set { if (value == _listYear) return; _listYear = value; OnPropertyChanged(); } }

        private int _selectedItemYear;
        public int SelectedItemYear { get => _selectedItemYear; set { if (value == _selectedItemYear) return; _selectedItemYear = value; OnPropertyChanged(); } }

        private ObservableCollection<CMonthReport> _listReport;
        public ObservableCollection<CMonthReport> ListReport { get => _listReport; set { if (value == _listReport) return; _listReport = value; OnPropertyChanged(); } }

        #endregion

        #region command binding

        public ICommand LoadCommand { get; set; }
        public ICommand SelectionChangedYear { get; set; }

        #endregion

        public ReportMonthPageVM()
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListYear = new ObservableCollection<int>();
                CreateYear();
                SelectedItemYear = DateTime.Now.Year;

                ListReport = new ObservableCollection<CMonthReport>(CMonthReport.Ins.MonthlyReport(SelectedItemYear));
            }
               );

            SelectionChangedYear = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListReport = new ObservableCollection<CMonthReport>(CMonthReport.Ins.MonthlyReport(SelectedItemYear));
            }
               );
        }

        void CreateYear()
        {
            for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year + 5; i++)
            {
                ListYear.Add(i);
            }
        }
    }
}
