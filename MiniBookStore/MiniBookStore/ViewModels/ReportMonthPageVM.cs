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

        private int _bookInCount;
        public int BookInCount { get => _bookInCount; set { if (value == _bookInCount) return; _bookInCount = value; OnPropertyChanged(); } }

        private float _bookInPrice;
        public float BookInPrice { get => _bookInPrice; set { if (value == _bookInPrice) return; _bookInPrice = value; OnPropertyChanged(); } }

        private int _bookOutCount;
        public int BookOutCount { get => _bookOutCount; set { if (value == _bookOutCount) return; _bookOutCount = value; OnPropertyChanged(); } }

        private float _bookOutPrice;
        public float BookOutPrice { get => _bookOutPrice; set { if (value == _bookOutPrice) return; _bookOutPrice = value; OnPropertyChanged(); } }

        private float _profit;
        public float Profit { get => _profit; set { if (value == _profit) return; _profit = value; OnPropertyChanged(); } }

        private float _salary;
        public float Salary { get => _salary; set { if (value == _salary) return; _salary = value; OnPropertyChanged(); } }

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

                BookInCount = ListReport.Sum(x => x.BookInCount);
                BookInPrice = ListReport.Sum(x => x.BookInPrice);
                BookOutCount = ListReport.Sum(x => x.BookOutCount);
                BookOutPrice = ListReport.Sum(x => x.BookOutPrice);
                Profit = ListReport.Sum(x => x.Profit);
                Salary = ListReport.Sum(x => x.Salary);
            }
               );

            SelectionChangedYear = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListReport = new ObservableCollection<CMonthReport>(CMonthReport.Ins.MonthlyReport(SelectedItemYear));

                BookInCount = ListReport.Sum(x => x.BookInCount);
                BookInPrice = ListReport.Sum(x => x.BookInPrice);
                BookOutCount = ListReport.Sum(x => x.BookOutCount);
                BookOutPrice = ListReport.Sum(x => x.BookOutPrice);
                Profit = ListReport.Sum(x => x.Profit);
                Salary = ListReport.Sum(x => x.Salary);
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
