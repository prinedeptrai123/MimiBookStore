using LiveCharts;
using LiveCharts.Wpf;
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
    public class ChartReportDatePageVM:BaseViewModel
    {

        #region data binding
        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection { get => _seriesCollection; set { if (value == _seriesCollection) return; _seriesCollection = value; OnPropertyChanged(); } }

        private string[] _labels;
        public string[] Labels { get => _labels; set { if (value == _labels) return; _labels = value; OnPropertyChanged(); } }

        public Func<double, string> YFormatter { get; set; }

        private SeriesCollection _bookSeriesCollection;
        public SeriesCollection BookSeriesCollection { get => _bookSeriesCollection; set { if (value == _bookSeriesCollection) return; _bookSeriesCollection = value; OnPropertyChanged(); } }

        private string[] _bookLabels;
        public string[] BookLabels { get => _bookLabels; set { if (value == _bookLabels) return; _bookLabels = value; OnPropertyChanged(); } }

        public Func<int, string> BookYFormatter { get; set; }

        private ObservableCollection<string> _listMonth;
        public ObservableCollection<string> ListMonth { get => _listMonth; set { if (value == _listMonth) return; _listMonth = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _listYear;
        public ObservableCollection<string> ListYear { get => _listYear; set { if (value == _listYear) return; _listYear = value; OnPropertyChanged(); } }

        private string _selectedItemMonth;
        public string SelectedItemMonth { get => _selectedItemMonth; set { if (value == _selectedItemMonth) return; _selectedItemMonth = value; OnPropertyChanged(); } }

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

        public ChartReportDatePageVM()
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

                CreateYear();
                SelectedItemYear = DateTime.Now.Year.ToString(); CreateMonth();
                CreateYear();

                SelectedItemMonth = DateTime.Now.Month.ToString();
                SelectedItemYear = DateTime.Now.Year.ToString();

                //https://stackoverflow.com/questions/24245523/getting-the-first-and-last-day-of-a-month-using-a-given-datetime-object

                DateTime date = new DateTime(int.Parse(SelectedItemYear), int.Parse(SelectedItemMonth), 1);
                DateBeginSelectedDate = new DateTime(date.Year, date.Month, 1);
                DateEndSelectedDate = DateBeginSelectedDate.AddMonths(1).AddDays(-1);

                LoadData();

                LoadChart();
            }
               );

            DateBeginSelectedDateChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (DateBeginSelectedDate != null && DateEndSelectedDate != null)
                {
                    ListReport = new ObservableCollection<CDateReport>(CDateReport.Ins.DailyReport(int.Parse(SelectedItemMonth), int.Parse(SelectedItemYear), DateBeginSelectedDate, DateEndSelectedDate));
                    LoadChart();
                }
            }
               );

            DateEndSelectedDateChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (DateBeginSelectedDate != null && DateEndSelectedDate != null)
                {
                    ListReport = new ObservableCollection<CDateReport>(CDateReport.Ins.DailyReport(int.Parse(SelectedItemMonth), int.Parse(SelectedItemYear), DateBeginSelectedDate, DateEndSelectedDate));
                    LoadChart();
                }
            }
              );

            SelectionChangedMonth = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItemMonth != null && SelectedItemYear != null)
                {
                    ListReport = new ObservableCollection<CDateReport>(CDateReport.Ins.DailyReport(int.Parse(SelectedItemMonth), int.Parse(SelectedItemYear)));

                    DateTime date = new DateTime(int.Parse(SelectedItemYear), int.Parse(SelectedItemMonth), 1);
                    DateBeginSelectedDate = new DateTime(date.Year, date.Month, 1);
                    DateEndSelectedDate = DateBeginSelectedDate.AddMonths(1).AddDays(-1);

                    LoadChart();
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

                    LoadChart();
                }
            }
              );
        }

        void LoadData()
        {
            ListReport = new ObservableCollection<CDateReport>(CDateReport.Ins.DailyReport(int.Parse(SelectedItemMonth), int.Parse(SelectedItemYear)));
        }

        void LoadChart()
        {
            //Lấy ra giá trị của lợi nhuận các tháng trong năm          
            SeriesCollection = new SeriesCollection();
            BookSeriesCollection = new SeriesCollection();

            Labels = new string[31];
            BookLabels = new string[31];

            YFormatter = value => value.ToString("F");
            BookYFormatter = value => value.ToString("D");

            //Đường chứa lợi nhuân
            SeriesCollection.Add(new LineSeries
            {
                Title = "Lợi nhuận",
                Values = new ChartValues<double>(),
                LineSmoothness = 0, //0: straight lines, 1: really smooth lines               

            });

            //Đường biểu diễn tiền bán sách
            SeriesCollection.Add(new LineSeries
            {
                Title = "Doanh thu",
                Values = new ChartValues<double>(),
                PointGeometry = DefaultGeometries.Square,
                PointGeometrySize = 15

            });

            BookSeriesCollection.Add(new LineSeries
            {
                Title = "Sách bán ra",
                Values = new ChartValues<int>(),
                LineSmoothness = 0, //0: straight lines, 1: really smooth lines 
            });

            int i = 0;
            foreach(var item in ListReport)
            {
                SeriesCollection[0].Values.Add((double)item.Profit);
                SeriesCollection[1].Values.Add((double)item.SumMoney);
                Labels[i] = item.Date.ToShortDateString();

                BookSeriesCollection[0].Values.Add(item.BookCount);
                BookLabels[i] = item.Date.ToShortDateString();
                i++;
            }
            
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
