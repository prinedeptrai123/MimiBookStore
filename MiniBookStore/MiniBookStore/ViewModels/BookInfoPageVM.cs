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

        private int _currentPage;
        public int CurrentPage { get => _currentPage; set { if (value == _currentPage) return; _currentPage = value; OnPropertyChanged(); } }

        int NumberPage = 8;
       

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

        private ObservableCollection<string> _listAuthor;
        public ObservableCollection<string> ListAuthor { get => _listAuthor; set { if (value == _listAuthor) return; _listAuthor = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _listType;
        public ObservableCollection<string> ListType { get => _listType; set { if (value == _listType) return; _listType = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _listTheme;
        public ObservableCollection<string> ListTheme { get => _listTheme; set { if (value == _listTheme) return; _listTheme = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _listCompany;
        public ObservableCollection<string> ListCompany { get => _listCompany; set { if (value == _listCompany) return; _listCompany = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _listSortBy;
        public ObservableCollection<string> ListSortBy { get => _listSortBy; set { if (value == _listSortBy) return; _listSortBy = value; OnPropertyChanged(); } }

        /// <summary>
        /// Binding selected item combobox
        /// </summary>

        private string _selectedItemAuthor;
        public string SelectedItemAuthor { get => _selectedItemAuthor; set { if (value == _selectedItemAuthor) return; _selectedItemAuthor = value; OnPropertyChanged(); } }
       
        private string _selectedItemTheme;
        public string SelectedItemTheme { get => _selectedItemTheme; set { if (value == _selectedItemTheme) return; _selectedItemTheme = value; OnPropertyChanged(); } }

        private string _selectedItemType;
        public string SelectedItemType { get => _selectedItemType; set { if (value == _selectedItemType) return; _selectedItemType = value; OnPropertyChanged(); } }

        private string _filterString;
        public string FilterString { get => _filterString; set { if (value == _filterString) return; _filterString = value; OnPropertyChanged(); } }

        private string _selectedItemCompany;
        public string SelectedItemCompany { get => _selectedItemCompany; set { if (value == _selectedItemCompany) return; _selectedItemCompany = value; OnPropertyChanged(); } }

        private string _selectedItemSortBy;
        public string SelectedItemSortBy { get => _selectedItemSortBy; set { if (value == _selectedItemSortBy) return; _selectedItemSortBy = value; OnPropertyChanged(); } }


        #endregion

        #region properties binding

        #endregion

        #region command binding

        public ICommand addNewBookCommand { get; set; }

        public ICommand LoadCommand { get; set; }

        public ICommand ListSelectionChanged { get; set; }

        public ICommand deleteBookCommand { get; set; }

        public ICommand editBookCommand { get; set; }

        public ICommand increaseBookCommand { get; set; }

        public ICommand PreviousPageCommand { get; set; }
        public ICommand NextPageCommand { get; set; }

        public ICommand TypeSelectionChanged { get; set; }
        public ICommand ThemeSelectionChanged { get; set; }
        public ICommand AuthorSelectionChanged { get; set; }
        public ICommand CompanySelectionChanged { get; set; }
        public ICommand SortBySelectionChanged { get; set; }

        public ICommand searchCommand { get; set; }

        #endregion

        public BookInfoPageVM()
        {
            searchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadBook();
            }
               );

            PreviousPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (CurrentPage > 1)
                {
                    CurrentPage = CurrentPage - 1;
                    LoadBook();
                }
            }
              );

            NextPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentPage = CurrentPage + 1;
                LoadBook();
            }
              );

            TypeSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItemType != null)
                {
                    ListTheme = new ObservableCollection<string>(CBook.Ins.ListThemeOfType(SelectedItemType));
                    ListTheme.Add("Tất cả");
                    SelectedItemTheme = "Tất cả";

                    LoadBook();
                }
            }
               );

            ThemeSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadBook();
            }
               );

            AuthorSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadBook();
            }
               );

            CompanySelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadBook();
            }
               );

            addNewBookCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                //Khởi tạo
                AddNewBookWindow wd = new AddNewBookWindow();
                wd.ShowDialog();
                //Load lại List
                ListBook = new ObservableCollection<CBook>(CBook.Ins.ListBook("tất cả", "tất cả", "tất cả", "tất cả", "tất cả", CurrentPage, NumberPage));
                SumNumber = CBook.Ins.sumBook();

                CWarehouse_History LastWarehouse = CBookInventory.InsInventory.LastWarehouse();
            }
               );

            increaseBookCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                //Khởi tạo
                IncreaseBookWindow wd = new IncreaseBookWindow();
                wd.ShowDialog();
                //Load lại List
                ListBook = new ObservableCollection<CBook>(CBook.Ins.ListBook("tất cả", "tất cả", "tất cả", "tất cả", "tất cả", CurrentPage, NumberPage));
                SumNumber = CBook.Ins.sumBook();

                CWarehouse_History LastWarehouse = CBookInventory.InsInventory.LastWarehouse();
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
                            ListBook = new ObservableCollection<CBook>(CBook.Ins.ListBook("tất cả", "tất cả", "tất cả", "tất cả", "tất cả", CurrentPage, NumberPage));
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
                    ListBook = new ObservableCollection<CBook>(CBook.Ins.ListBook("tất cả", "tất cả", "tất cả", "tất cả", "tất cả", CurrentPage, NumberPage));
                }
            }
               );

            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentPage = 1;
                FilterString = "";

                ListBook = new ObservableCollection<CBook>(CBook.Ins.ListBook("tất cả", "tất cả", "tất cả", "tất cả", "tất cả", CurrentPage, NumberPage));
                SumNumber = CBook.Ins.sumBook();

                CWarehouse_History LastWarehouse = CBookInventory.InsInventory.LastWarehouse();
                LastDate = LastWarehouse.Date;
                LastNumberBook = LastWarehouse.TotalCount;
                LastTotalMoney = LastWarehouse.ToltalMoney;

                ListTheme = new ObservableCollection<string>();
                ListTheme.Add("Tất cả");

                ListType = new ObservableCollection<string>(CBook.Ins.ListType());
                ListType.Add("Tất cả");

                ListAuthor = new ObservableCollection<string>(CBook.Ins.ListAuthor());
                ListAuthor.Add("Tất cả");

                ListCompany = new ObservableCollection<string>(CBook.Ins.ListCompany());
                ListCompany.Add("Tất cả");
               
                SelectedItemAuthor = "Tất cả";
                SelectedItemCompany = "Tất cả";              
                SelectedItemType = "Tất cả";
                SelectedItemTheme = "Tất cả";

                CreateSortBy();
                SelectedItemSortBy = "Tên";
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

            SortBySelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadBookWithSort();
            }
               );
        }

        private void LoadBook()
        {
            if (SelectedItemCompany != null && SelectedItemTheme != null && SelectedItemType != null && SelectedItemAuthor != null)
            {
                ListBook = new ObservableCollection<CBook>(CBook.Ins.ListBook(FilterString, SelectedItemAuthor, SelectedItemType, SelectedItemTheme, SelectedItemCompany, CurrentPage, NumberPage));
            }
        }

        private void LoadBookWithSort()
        {
            if (SelectedItemCompany != null && SelectedItemTheme != null && SelectedItemType != null && SelectedItemAuthor != null&&SelectedItemSortBy!=null)
            {
                ListBook = new ObservableCollection<CBook>(CBook.Ins.ListBook(FilterString, SelectedItemAuthor, SelectedItemType, SelectedItemTheme, SelectedItemCompany, SelectedItemSortBy, CurrentPage, NumberPage));
            }
        }

        private void CreateSortBy()
        {         
            ListSortBy = new ObservableCollection<string>();
            ListSortBy.Add("Tên");
            ListSortBy.Add("Mã");
            ListSortBy.Add("Tồn kho giảm dần");
            ListSortBy.Add("Tồn kho tăng dần");
            ListSortBy.Add("Lượt mua giảm dần");
            ListSortBy.Add("Lượt mua tăng dần");
        }
    }
}
