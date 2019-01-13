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
    public class HistoryWareHousePageVM:BaseViewModel
    {
        #region Global

        private int _currentPage;
        public int CurrentPage { get => _currentPage; set { if (value == _currentPage) return; _currentPage = value; OnPropertyChanged(); } }

        int NumberPage = 6;

        #endregion

        #region data binding

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

        private ObservableCollection<CWarehouse_History> _listWareHouse;
        public ObservableCollection<CWarehouse_History> listWareHouse { get => _listWareHouse; set { if (value == _listWareHouse) return; _listWareHouse = value; OnPropertyChanged(); } }

        private CWarehouse_History _wareHouseSelectedItem;
        public CWarehouse_History WareHouseSelectedItem { get => _wareHouseSelectedItem; set { if (value == _wareHouseSelectedItem) return; _wareHouseSelectedItem = value; OnPropertyChanged(); } }

        private int _wareHouseID;
        public int WareHouseID { get => _wareHouseID; set { if (value == _wareHouseID) return; _wareHouseID = value; OnPropertyChanged(); } }

        private ObservableCollection<CBookInventory> _listDetail;
        public ObservableCollection<CBookInventory> ListDetail { get => _listDetail; set { if (value == _listDetail) return; _listDetail = value; OnPropertyChanged(); } }

        #endregion

        public ICommand LoadCommand { get; set; }
        public ICommand PreviousPageCommand { get; set; }
        public ICommand NextPageCommand { get; set; }
        public ICommand WareHouseSelectionChanged { get; set; }

        public ICommand SelectionChangedMonth { get; set; }
        public ICommand SelectionChangedYear { get; set; }

        public ICommand DateEndSelectedDateChanged { get; set; }
        public ICommand DateBeginSelectedDateChanged { get; set; }

        public HistoryWareHousePageVM()
        {
            WareHouseSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (WareHouseSelectedItem != null)
                {
                    WareHouseID = WareHouseSelectedItem.ID;
                    ListDetail = new ObservableCollection<CBookInventory>(CBookInventory.InsInventory.DetailOfWareHouse(WareHouseSelectedItem.ID));
                }
            }
             );

            DateEndSelectedDateChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (DateBeginSelectedDate != null && DateEndSelectedDate != null)
                {
                    ListDetail = new ObservableCollection<CBookInventory>();

                    listWareHouse = new ObservableCollection<CWarehouse_History>(CWarehouse_History.Ins.Warehouse_History(DateBeginSelectedDate, DateEndSelectedDate, CurrentPage, NumberPage));
                }
            }
             );

            DateBeginSelectedDateChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (DateBeginSelectedDate != null && DateEndSelectedDate != null)
                {
                    ListDetail = new ObservableCollection<CBookInventory>();

                    listWareHouse = new ObservableCollection<CWarehouse_History>(CWarehouse_History.Ins.Warehouse_History(DateBeginSelectedDate, DateEndSelectedDate, CurrentPage, NumberPage));
                }
            }
             );

            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentPage = 1;
                CreateMonth();
                CreateYear();

                SelectedItemMonth = DateTime.Now.Month.ToString();
                SelectedItemYear = DateTime.Now.Year.ToString();

                //https://stackoverflow.com/questions/24245523/getting-the-first-and-last-day-of-a-month-using-a-given-datetime-object

                DateTime date = new DateTime(int.Parse(SelectedItemYear), int.Parse(SelectedItemMonth), 1);
                DateBeginSelectedDate = new DateTime(date.Year, date.Month, 1);
                DateEndSelectedDate = DateBeginSelectedDate.AddMonths(1).AddDays(-1);

                listWareHouse = new ObservableCollection<CWarehouse_History>(CWarehouse_History.Ins.Warehouse_History(int.Parse(SelectedItemMonth), int.Parse(SelectedItemYear), CurrentPage, NumberPage));
            }
             );

            PreviousPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (CurrentPage > 1)
                {
                    CurrentPage = CurrentPage - 1;
                    listWareHouse = new ObservableCollection<CWarehouse_History>(CWarehouse_History.Ins.Warehouse_History(int.Parse(SelectedItemMonth), int.Parse(SelectedItemYear), CurrentPage, NumberPage));
                }
            }
              );

            NextPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentPage = CurrentPage + 1;
                listWareHouse = new ObservableCollection<CWarehouse_History>(CWarehouse_History.Ins.Warehouse_History(int.Parse(SelectedItemMonth), int.Parse(SelectedItemYear), CurrentPage, NumberPage));
            }
              );

            SelectionChangedYear = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItemMonth != null && SelectedItemYear != null)
                {
                    ListDetail = new ObservableCollection<CBookInventory>();

                    DateTime date = new DateTime(int.Parse(SelectedItemYear), int.Parse(SelectedItemMonth), 1);
                    DateBeginSelectedDate = new DateTime(date.Year, date.Month, 1);
                    DateEndSelectedDate = DateBeginSelectedDate.AddMonths(1).AddDays(-1);

                    listWareHouse = new ObservableCollection<CWarehouse_History>(CWarehouse_History.Ins.Warehouse_History(int.Parse(SelectedItemMonth), int.Parse(SelectedItemYear), CurrentPage, NumberPage));
                }
            }
             );

            SelectionChangedMonth = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItemMonth != null && SelectedItemYear != null)
                {
                    ListDetail = new ObservableCollection<CBookInventory>();

                    DateTime date = new DateTime(int.Parse(SelectedItemYear), int.Parse(SelectedItemMonth), 1);
                    DateBeginSelectedDate = new DateTime(date.Year, date.Month, 1);
                    DateEndSelectedDate = DateBeginSelectedDate.AddMonths(1).AddDays(-1);

                    listWareHouse = new ObservableCollection<CWarehouse_History>(CWarehouse_History.Ins.Warehouse_History(int.Parse(SelectedItemMonth), int.Parse(SelectedItemYear), CurrentPage, NumberPage));
                }
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
