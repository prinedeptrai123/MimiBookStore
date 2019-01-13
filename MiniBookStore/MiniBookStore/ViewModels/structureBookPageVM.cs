using LiveCharts;
using LiveCharts.Defaults;
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
    public class structureBookPageVM:BaseViewModel
    {

        #region data binding

        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection { get => _seriesCollection; set { if (value == _seriesCollection) return; _seriesCollection = value; OnPropertyChanged(); } }

        private ObservableCollection<CBookType> _listType;
        public ObservableCollection<CBookType> ListType { get => _listType; set { if (value == _listType) return; _listType = value; OnPropertyChanged(); } }

        #endregion

        #region command binding

        public ICommand LoadCommand { get; set; }

        #endregion

        public structureBookPageVM()
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                loaddata();
                LoadChart();
            }
               );
        }

        public void loaddata()
        {
            ListType = new ObservableCollection<CBookType>(CBook.Ins.ListFullType(false));
        }

        public void LoadChart()
        {
            SeriesCollection = new SeriesCollection();

            foreach(var item in ListType)
            {
                var data = new PieSeries
                {
                    Title = item.Name,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(item.Count) },
                    DataLabels = true
                };
                SeriesCollection.Add(data);
            }                                                                 
        }
    }
}
