using MiniBookStore.Models;
using MiniBookStore.Models.MyClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MiniBookStore.ViewModels
{
    public class IncreaseBookWindowVM:BaseViewModel
    {
        #region Global

        int currentPage = 1;
        int NumberPage = 10;

        

        #endregion

        #region data binding

        private ObservableCollection<CBook> _listBookSearch;
        public ObservableCollection<CBook> ListBookSearch { get => _listBookSearch; set { if (value == _listBookSearch) return; _listBookSearch = value; OnPropertyChanged(); } }

        private CBook _listSearchSelectedItem;
        public CBook ListSearchSelectedItem { get => _listSearchSelectedItem; set { if (value == _listSearchSelectedItem) return; _listSearchSelectedItem = value; OnPropertyChanged(); } }

        private string _employeeName;
        public string EmployeeName { get => _employeeName; set { if (value == _employeeName) return; _employeeName = value; OnPropertyChanged(); } }

        private string _dateNow;
        public string DateNow { get => _dateNow; set { if (value == _dateNow) return; _dateNow = value; OnPropertyChanged(); } }

        private BitmapImage _coverImage;
        public BitmapImage CoverImage { get => _coverImage; set { if (value == _coverImage) return; _coverImage = value; OnPropertyChanged(); } }

        private string _filterString;
        public string FilterString { get => _filterString; set { if (value == _filterString) return; _filterString = value; OnPropertyChanged(); } }

        private int _iD;
        public int ID { get => _iD; set { if (value == _iD) return; _iD = value; OnPropertyChanged(); } }

        private string _count;
        public string Count { get => _count; set { if (value == _count) return; _count = value; OnPropertyChanged(); } }

        private string _inPrice;
        public string InPrice { get => _inPrice; set { if (value == _inPrice) return; _inPrice = value; OnPropertyChanged(); } }

        private float _totalBookPrice;
        public float TotalBookPrice { get => _totalBookPrice; set { if (value == _totalBookPrice) return; _totalBookPrice = value; OnPropertyChanged(); } }

        private ObservableCollection<CBookInventory> _listBook;
        public ObservableCollection<CBookInventory> ListBook { get => _listBook; set { if (value == _listBook) return; _listBook = value; OnPropertyChanged(); } }

        private CBookInventory _listSelectedItem;
        public CBookInventory ListSelectedItem { get => _listSelectedItem; set { if (value == _listSelectedItem) return; _listSelectedItem = value; OnPropertyChanged(); } }

        private int _listSelectedIndex;
        public int ListSelectedIndex { get => _listSelectedIndex; set { if (value == _listSelectedIndex) return; _listSelectedIndex = value; OnPropertyChanged(); } }

        private float _totalPrice;
        public float TotalPrice { get => _totalPrice; set { if (value == _totalPrice) return; _totalPrice = value; OnPropertyChanged(); } }
    
        #endregion

        #region command binding

        public ICommand addToListCommand { get; set; }

        public ICommand editListCommand { get; set; }

        public ICommand deleteListCommand { get; set; }

        public ICommand ListSearchSelectionChanged { get; set; }

        public ICommand LoadCommand { get; set; }

        public ICommand SearchCommand { get; set; }

        public ICommand ListSelectionChanged { get; set; }

        public ICommand addCommand { get; set; }

        public ICommand CountTextChange { get; set; }

        public ICommand InPriceTextChange { get; set; }

        #endregion

        public IncreaseBookWindowVM()
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                EmployeeName = DataTransfer.EmployeeInfo.Name;
                DateNow = DateTime.Now.ToShortDateString();

                //Cập nhật List search
                ListBookSearch = new ObservableCollection<CBook>(CBook.Ins.ListBook("tất cả", "tất cả", "tất cả", "tất cả", "tất cả", currentPage, NumberPage));

                ListBook = new ObservableCollection<CBookInventory>();
            }
               );

            SearchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if(FilterString == "")
                {
                    ListBookSearch = new ObservableCollection<CBook>(CBook.Ins.ListBook("tất cả", "tất cả", "tất cả", "tất cả", "tất cả", currentPage, NumberPage));
                }
                else
                {
                    ListBookSearch = new ObservableCollection<CBook>(CBook.Ins.ListBook(FilterString, "tất cả", "tất cả", "tất cả", "tất cả", currentPage, NumberPage));
                }
            }
               );

            addCommand = new RelayCommand<object>((p) => {
                if (ListBook.Count == 0)
                    return false;
                return true;
            }, (p) =>
            {
                //Tạo mới một lịch sử nhập kho
                int WarehouseID = CBookInventory.InsInventory.addWarehouse(DataTransfer.EmployeeInfo.ID, true, TotalPrice, DateTime.Now);

                //Duyệt trong danh sách sách trong List và thêm vào trong kho
                foreach (var item in ListBook)
                {
                    
                    //Thêm vào chi tiết lịch sử nhập kho
                    CBookInventory.InsInventory.addDetailWarehouse(WarehouseID, item.ID, item.WarehouseInventory, item.InPrice);

                    //Thêm vào bảng lưu số lượng sách trong đợt nhập này
                    CBookInventory.InsInventory.addBookInventory(item.ID, WarehouseID, item.WarehouseInventory);

                    //Thêm số lượng sách cho sách trong kho
                    CBook.Ins.increaseBook(item.ID, item.WarehouseInventory);

                }

                //Thông báo nhập thành công
                MessageBox.Show("Nhập thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                //Làm mới ListBook
                ListBook.Clear();

                //Làm mới bảng nhập
                ID = 0;
                InPrice = "";
                TotalPrice = 0;
                Count = "";

                //Load lại list tìm kiếm
                ListBookSearch = new ObservableCollection<CBook>(CBook.Ins.ListBook("tất cả", "tất cả", "tất cả", "tất cả", "tất cả", currentPage, NumberPage));
            }
               );

            ListSearchSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (ListSearchSelectedItem != null)
                {
                    CoverImage = ListSearchSelectedItem.Image;

                    ID = ListSearchSelectedItem.ID;
                }
            }
               );

            ListSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (ListSelectedItem != null)
                {
                    CoverImage = ListSelectedItem.Image;
                    ID = ListSelectedItem.ID;
                    Count = ListSelectedItem.WarehouseInventory.ToString();
                    InPrice = ListSelectedItem.InPrice.ToString();
                    TotalBookPrice = ListSelectedItem.TotalPrice;                   
                }                
            }
               );

            CountTextChange = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if(Help.isFloat(InPrice)==true && Help.isInt(Count) == true)
                {
                    TotalBookPrice = int.Parse(Count) * float.Parse(InPrice);
                }
            }
               );

            InPriceTextChange = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (Help.isFloat(InPrice) == true && Help.isInt(Count) == true)
                {
                    TotalBookPrice = int.Parse(Count) * float.Parse(InPrice);
                }
            }
               );

            deleteListCommand = new RelayCommand<object>((p) => 
            {
                if (ListBook.Count == 0)
                {
                    return false;
                }
                if(ListSelectedItem == null)
                {
                    return false;
                }
                
                return true;
            }, (p) =>
            {
                if(ListSelectedItem!= null)
                {
                    ListBook.RemoveAt(ListSelectedIndex);
                    if(ListBook.Count == 0)
                    {
                        TotalPrice = 0;
                    }
                    else
                    {
                        TotalPrice = ListBook.Sum(x => x.TotalPrice);
                    }
                }
            }
               );

            editListCommand = new RelayCommand<object>((p) =>
            {
                if (ListBook.Count == 0)
                {
                    return false;
                }
                if (ListSelectedItem == null)
                {
                    return false;
                }
                if(string.IsNullOrEmpty(Count) || string.IsNullOrEmpty(InPrice))
                {
                    return false;
                }

                if (Help.isInt(Count) == false || Help.isFloat(InPrice) == false)
                {
                    return false;
                }

                if (float.Parse(InPrice) <= 0 || int.Parse(Count) <= 0)
                {
                    return false;
                }

                return true;
            }, (p) =>
            {
                if (ListSelectedItem != null)
                {
                    ListSelectedItem.WarehouseInventory = int.Parse(Count);
                    ListSelectedItem.InPrice = float.Parse(InPrice);
                    ListSelectedItem.TotalPrice = int.Parse(Count) * float.Parse(InPrice);

                    if (ListBook.Count == 0)
                    {
                        TotalPrice = 0;
                    }
                    else
                    {
                        TotalPrice = ListBook.Sum(x => x.TotalPrice);
                    }
                }
            }
               );

            addToListCommand = new RelayCommand<object>((p) => 
            {
                if (ListBook.Count > 0)
                {
                    if (ListBook.Any(x => x.ID == ID) == true)
                    {
                        return false;
                    }
                }

                if(string.IsNullOrEmpty(Count) || string.IsNullOrEmpty(InPrice))
                {
                    return false;
                }

                if(Help.isInt(Count) ==false || Help.isFloat(InPrice) == false)
                {
                    return false;
                }

                if(float.Parse(InPrice) <=0 || int.Parse(Count) <= 0)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                if (ListSearchSelectedItem != null)
                {                   
                    //Tạo mới một sách
                    CBookInventory Book = new CBookInventory
                    {
                        ID = ListSearchSelectedItem.ID,
                        Name = ListSearchSelectedItem.Name,
                        Author = ListSearchSelectedItem.Author,
                        WarehouseInventory = int.Parse(Count),
                        InPrice = float.Parse(InPrice),
                        TotalPrice = int.Parse(Count) * float.Parse(InPrice),
                        Image = ListSearchSelectedItem.Image,
                        Company = ListSearchSelectedItem.Company
                    };

                    //Thêm vào List
                    ListBook.Add(Book);

                    TotalPrice = ListBook.Sum(x => x.TotalPrice);
                }
            }
               );
        }
    }
}
