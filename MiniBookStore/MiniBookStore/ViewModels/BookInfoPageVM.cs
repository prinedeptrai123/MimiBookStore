using MiniBookStore.Models;
using MiniBookStore.Models.MyClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MiniBookStore.ViewModels
{
    public class BookInfoPageVM:BaseViewModel
    {
        #region Global

        int currentPage = 1;
        int NumberPage = 10;

        #endregion

        #region data binding

        public Employee EmployeeInfo;

        private ObservableCollection<CBook> _listBook;
        public ObservableCollection<CBook> ListBook { get => _listBook; set { if (value == _listBook) return;_listBook = value;OnPropertyChanged(); } }

        private CBook _listSelectedItem;
        public CBook ListSelectedItem { get => _listSelectedItem; set { if (value == _listSelectedItem) return; _listSelectedItem = value; OnPropertyChanged(); } }

        private BitmapImage _coverImage;
        public BitmapImage CoverImage { get => _coverImage; set { if (value == _coverImage) return; _coverImage = value; OnPropertyChanged(); } }

        private int _sumNumber;
        public int SumNumber { get => _sumNumber; set { if (value == _sumNumber) return; _sumNumber = value; OnPropertyChanged(); } }

        private DateTime _lastDate;
        public DateTime LastDate { get => _lastDate; set { if (value == _lastDate) return; _lastDate = value; OnPropertyChanged(); } }

        private int _lastNumberBook;
        public int LastNumberBook { get => _lastNumberBook; set { if (value == _lastNumberBook) return; _lastNumberBook = value; OnPropertyChanged(); } }

        private float _lastTotalMoney;
        public float LastTotalMoney { get => _lastTotalMoney; set { if (value == _lastTotalMoney) return; _lastTotalMoney = value; OnPropertyChanged(); } }


        #endregion

        #region properties binding

        #endregion

        #region command binding

        public ICommand addNewBookCommand { get; set; }

        public ICommand LoadCommand { get; set; }

        public ICommand ListSelectionChanged { get; set; }

        public ICommand deleteBookCommand { get; set; }

        public ICommand editBookCommand { get; set; }

        #endregion

        public BookInfoPageVM()
        {
            addNewBookCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                //Khởi tạo
                AddNewBookWindow wd = new AddNewBookWindow();
                wd.ShowDialog();               
            }
               );

            deleteBookCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (ListSelectedItem != null)
                {
                    if (ListSelectedItem.Inventory > 0)
                    {
                        MessageBox.Show("Không thể xóa sách còn tồn trong kho", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        if(CBook.Ins.deleteBook(ListSelectedItem.ID) == true)
                        {
                            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            //load lại
                            ListBook = new ObservableCollection<CBook>(CBook.Ins.ListBook("tất cả", "tất cả", "tất cả", "tất cả", "tất cả", currentPage, NumberPage));
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
               );

            editBookCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (ListSelectedItem != null)
                {
                    DetailsBookWindow wd = new DetailsBookWindow(ListSelectedItem);
                    wd.ShowDialog();
                    ListBook = new ObservableCollection<CBook>(CBook.Ins.ListBook("tất cả", "tất cả", "tất cả", "tất cả", "tất cả", currentPage, NumberPage));
                }
            }
               );

            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                
                ListBook = new ObservableCollection<CBook>(CBook.Ins.ListBook("tất cả", "tất cả", "tất cả", "tất cả", "tất cả", currentPage, NumberPage));
                SumNumber = CBook.Ins.sumBook();

                CWarehouse_History LastWarehouse = CBookInventory.InsInventory.LastWarehouse();
                LastDate = LastWarehouse.Date;
                LastNumberBook = LastWarehouse.TotalCount;
                LastTotalMoney = LastWarehouse.ToltalMoney;
            }
               );

            ListSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (ListSelectedItem != null)
                {                   
                    CoverImage = ListSelectedItem.Image;
                }
            }
               );
        }
    }
}
