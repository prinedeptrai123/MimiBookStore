using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using MiniBookStore.Models.MyClass;

namespace MiniBookStore.ViewModels
{
    public class CharReportMonthPageVM : BaseViewModel
    {
        #region data binding
        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection { get => _seriesCollection; set { if (value == _seriesCollection) return; _seriesCollection = value; OnPropertyChanged(); } }

        private string[] _labels;
        public string[] Labels { get => _labels; set { if (value == _labels) return; _labels = value; OnPropertyChanged(); } }

        public Func<double, string> YFormatter { get; set; }

        private ObservableCollection<CMonthReport> _listReport;
        public ObservableCollection<CMonthReport> ListReport { get => _listReport; set { if (value == _listReport) return; _listReport = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _listYear;
        public ObservableCollection<string> ListYear { get => _listYear; set { if (value == _listYear) return; _listYear = value; OnPropertyChanged(); } }

        private string _selectedItemYear;
        public string SelectedItemYear { get => _selectedItemYear; set { if (value == _selectedItemYear) return; _selectedItemYear = value; OnPropertyChanged(); } }

        #endregion

        #region command binding

        public ICommand LoadCommand { get; set; }
        public ICommand SelectionChangedYear { get; set; }

        #endregion

        public CharReportMonthPageVM()
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

                CreateYear();
                SelectedItemYear = DateTime.Now.Year.ToString();

                LoadChart();
            }
               );

            SelectionChangedYear = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItemYear != null)
                {
                    LoadChart();
                }
            }
               );

        }
        void LoadData()
        {
            ListReport = new ObservableCollection<CMonthReport>(CMonthReport.Ins.MonthlyReport(int.Parse(SelectedItemYear)));
        }

        void LoadChart()
        {
            //Lấy ra giá trị của lợi nhuận các tháng trong năm
            LoadData();

            SeriesCollection = new SeriesCollection();

            Labels = new[] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };
            YFormatter = value => value.ToString("F");

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

            //Đường biểu diễn tiền nhập sách
            SeriesCollection.Add(new LineSeries
            {
                Title = "Tiền nhập sách",
                Values = new ChartValues<double>(),
                PointGeometry = null

            });

            foreach (var item in ListReport)
            {
                SeriesCollection[0].Values.Add((double)item.Profit);
                SeriesCollection[1].Values.Add((double)item.BookOutPrice);
                SeriesCollection[2].Values.Add((double)item.BookInPrice);
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
