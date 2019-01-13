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

        private ObservableCollection<string> _listMonth;
        public ObservableCollection<string> ListMonth { get => _listMonth; set { if (value == _listMonth) return; _listMonth = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _listYear;
        public ObservableCollection<string> ListYear { get => _listYear; set { if (value == _listYear) return; _listYear = value; OnPropertyChanged(); } }

        private string _selectedItemMonth;
        public string SelectedItemMonth { get => _selectedItemMonth; set { if (value == _selectedItemMonth) return;_selectedItemMonth = value;OnPropertyChanged(); } }

        private string _selectedItemYear;
        public string SelectedItemYear { get => _selectedItemYear; set { if (value == _selectedItemYear) return; _selectedItemYear = value; OnPropertyChanged(); } }

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
                    ListReport = new ObservableCollection<CDateReport>(CDateReport.Ins.DailyReport(int.Parse(SelectedItemMonth), int.Parse(SelectedItemYear), DateBeginSelectedDate, DateEndSelectedDate));
                   
                }
            }
               );

            DateEndSelectedDateChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (DateBeginSelectedDate != null && DateEndSelectedDate != null)
                {
                    ListReport = new ObservableCollection<CDateReport>(CDateReport.Ins.DailyReport(int.Parse(SelectedItemMonth), int.Parse(SelectedItemYear), DateBeginSelectedDate, DateEndSelectedDate));

                }
            }
              );

            SelectionChangedMonth = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if(SelectedItemMonth!=null && SelectedItemYear != null)
                {
                    ListReport = new ObservableCollection<CDateReport>(CDateReport.Ins.DailyReport(int.Parse(SelectedItemMonth), int.Parse(SelectedItemYear)));

                    DateTime date = new DateTime(int.Parse(SelectedItemYear), int.Parse(SelectedItemMonth), 1);
                    DateBeginSelectedDate = new DateTime(date.Year, date.Month, 1);
                    DateEndSelectedDate = DateBeginSelectedDate.AddMonths(1).AddDays(-1);
                }
            }
               );

            SelectionChangedYear = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItemMonth != null && SelectedItemYear != null)
                {
                    ListReport = new ObservableCollection<CDateReport>(CDateReport.Ins.DailyReport(int.Parse(SelectedItemMonth), int.Parse(SelectedItemYear)));

                    DateTime date = new DateTime(int.Parse(SelectedItemYear), int.Parse(SelectedItemMonth), 1);
                    DateBeginSelectedDate = new DateTime(date.Year, date.Month, 1);
                    DateEndSelectedDate = DateBeginSelectedDate.AddMonths(1).AddDays(-1);
                }
            }
              );

            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {            
                CreateMonth();
                CreateYear();

                SelectedItemMonth = DateTime.Now.Month.ToString();
                SelectedItemYear = DateTime.Now.Year.ToString();

                //https://stackoverflow.com/questions/24245523/getting-the-first-and-last-day-of-a-month-using-a-given-datetime-object

                DateTime date = new DateTime(int.Parse(SelectedItemYear), int.Parse(SelectedItemMonth), 1);
                DateBeginSelectedDate = new DateTime(date.Year, date.Month, 1);
                DateEndSelectedDate = DateBeginSelectedDate.AddMonths(1).AddDays(-1);

                ListReport = new ObservableCollection<CDateReport>(CDateReport.Ins.DailyReport(int.Parse(SelectedItemMonth), int.Parse(SelectedItemYear)));
            }
               );

        }



        void CreateMonth()
        {
            ListMonth = new ObservableCollection<string>();
            for (int i = 1; i <= 12; i++)
            {
                ListMonth.Add(i.ToString());
            }                    
        }

        void CreateYear()
        {
            ListYear = new ObservableCollection<string>();
            for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year + 5; i++)
            {
                ListYear.Add(i.ToString());
            }          
        }
    }
}
