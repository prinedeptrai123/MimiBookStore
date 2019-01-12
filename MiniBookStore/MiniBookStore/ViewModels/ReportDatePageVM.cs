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
    public class ReportDatePageVM:BaseViewModel
    {
        #region data binding

        private ObservableCollection<int> _listMonth;
        public ObservableCollection<int> ListMonth { get => _listMonth; set { if (value == _listMonth) return; _listMonth = value; OnPropertyChanged(); } }

        private ObservableCollection<int> _listYear;
        public ObservableCollection<int> ListYear { get => _listYear; set { if (value == _listYear) return; _listYear = value; OnPropertyChanged(); } }

        private int _selectedItemMonth;
        public int SelectedItemMonth { get => _selectedItemMonth; set { if (value == _selectedItemMonth) return;_selectedItemMonth = value;OnPropertyChanged(); } }

        private int _selectedItemYear;
        public int SelectedItemYear { get => _selectedItemYear; set { if (value == _selectedItemYear) return; _selectedItemYear = value; OnPropertyChanged(); } }

        private DateTime _dateEndSelectedDate;
        public DateTime DateEndSelectedDate { get => _dateEndSelectedDate; set { if (value == _dateEndSelectedDate) return; _dateEndSelectedDate = value; OnPropertyChanged(); } }

        private DateTime _dateBeginSelectedDate;
        public DateTime DateBeginSelectedDate { get => _dateBeginSelectedDate; set { if (value == _dateBeginSelectedDate) return; _dateBeginSelectedDate = value; OnPropertyChanged(); } }

        private ObservableCollection<CDateReport> _listReport;
        public ObservableCollection<CDateReport> ListReport { get => _listReport; set { if (value == _listReport) return; _listReport = value; OnPropertyChanged(); } }
        #endregion

        #region command binding

        public ICommand LoadCommand { get; set; }
        public ICommand DateBeginSelectedDateChanged { get; set; }
        public ICommand DateEndSelectedDateChanged { get; set; }
        public ICommand SelectionChangedMonth { get; set; }
        public ICommand SelectionChangedYear { get; set; }

        #endregion

        public ReportDatePageVM()
        {
            DateBeginSelectedDateChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (DateBeginSelectedDate != null && DateEndSelectedDate != null)
                {
                    ListReport = new ObservableCollection<CDateReport>(CDateReport.Ins.MonthlyReport(SelectedItemMonth, SelectedItemYear, DateBeginSelectedDate, DateEndSelectedDate));
                   
                }
            }
               );

            DateEndSelectedDateChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (DateBeginSelectedDate != null && DateEndSelectedDate != null)
                {
                    ListReport = new ObservableCollection<CDateReport>(CDateReport.Ins.MonthlyReport(SelectedItemMonth, SelectedItemYear, DateBeginSelectedDate, DateEndSelectedDate));

                }
            }
              );

            SelectionChangedMonth = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if(SelectedItemMonth!=0 && SelectedItemYear != 0)
                {
                    ListReport = new ObservableCollection<CDateReport>(CDateReport.Ins.MonthlyReport(SelectedItemMonth, SelectedItemYear));

                    DateTime date = new DateTime(SelectedItemYear, SelectedItemMonth, 1);
                    DateBeginSelectedDate = new DateTime(date.Year, date.Month, 1);
                    DateEndSelectedDate = DateBeginSelectedDate.AddMonths(1).AddDays(-1);
                }
            }
               );

            SelectionChangedYear = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItemMonth != 0 && SelectedItemYear != 0)
                {
                    ListReport = new ObservableCollection<CDateReport>(CDateReport.Ins.MonthlyReport(SelectedItemMonth, SelectedItemYear));

                    DateTime date = new DateTime(SelectedItemYear, SelectedItemMonth, 1);
                    DateBeginSelectedDate = new DateTime(date.Year, date.Month, 1);
                    DateEndSelectedDate = DateBeginSelectedDate.AddMonths(1).AddDays(-1);
                }
            }
              );

            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListMonth = new ObservableCollection<int>();
                ListYear = new ObservableCollection<int>();

                CreateMonth();
                CreateYear();

                SelectedItemMonth = DateTime.Now.Month;
                SelectedItemYear = DateTime.Now.Year;

                //https://stackoverflow.com/questions/24245523/getting-the-first-and-last-day-of-a-month-using-a-given-datetime-object

                DateTime date = new DateTime(SelectedItemYear, SelectedItemMonth, 1);
                DateBeginSelectedDate = new DateTime(date.Year, date.Month, 1);
                DateEndSelectedDate = DateBeginSelectedDate.AddMonths(1).AddDays(-1);

                ListReport = new ObservableCollection<CDateReport>(CDateReport.Ins.MonthlyReport(SelectedItemMonth, SelectedItemYear));
            }
               );

        }

        void CreateMonth()
        {
            for (int i = 1; i <= 12; i++)
            {
                ListMonth.Add(i);
            }                    
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
