using MiniBookStore.Models.MyClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MiniBookStore.ViewModels
{
    public class ProductPageVM:BaseViewModel
    {
        #region Global

        private int _currentPage;
        public int CurrentPage { get => _currentPage; set { if (value == _currentPage) return; _currentPage = value; OnPropertyChanged(); } }

        int NumberPage = 8;
        public bool isSale = false;

        #endregion

        #region data binding

        private ObservableCollection<CBook> _listBook;
        public ObservableCollection<CBook> ListBook { get => _listBook; set { if (value == _listBook) return; _listBook = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _listAuthor;
        public ObservableCollection<string> ListAuthor { get => _listAuthor; set { if (value == _listAuthor) return; _listAuthor = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _listType;
        public ObservableCollection<string> ListType { get => _listType; set { if (value == _listType) return; _listType = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _listTheme;
        public ObservableCollection<string> ListTheme { get => _listTheme; set { if (value == _listTheme) return; _listTheme = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _listCompany;
        public ObservableCollection<string> ListCompany { get => _listCompany; set { if (value == _listCompany) return; _listCompany = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _listPrice;
        public ObservableCollection<string> ListPrice { get => _listPrice; set { if (value == _listPrice) return; _listPrice = value; OnPropertyChanged(); } }

        /// <summary>
        /// Binding text combobox
        /// </summary>

        private string _textType;
        public string TextType { get => _textType; set { if (value == _textType) return; _textType = value; OnPropertyChanged(); } }

        private string _textTheme;
        public string TextTheme { get => _textTheme; set { if (value == _textTheme) return; _textTheme = value; OnPropertyChanged(); } }

        private string _textAuthor;
        public string TextAuthor { get => _textAuthor; set { if (value == _textAuthor) return; _textAuthor = value; OnPropertyChanged(); } }

        private string _textCompany;
        public string TextCompany { get => _textCompany; set { if (value == _textCompany) return; _textCompany = value; OnPropertyChanged(); } }

        private string _textPrice;
        public string TextPrice { get => _textPrice; set { if (value == _textPrice) return; _textPrice = value; OnPropertyChanged(); } }

        /// <summary>
        /// Binding selected item combobox
        /// </summary>

        private string _selectedItemAuthor;
        public string SelectedItemAuthor { get => _selectedItemAuthor; set { if (value == _selectedItemAuthor) return; _selectedItemAuthor = value; OnPropertyChanged(); } }

        private string _selectedItemPrice;
        public string SelectedItemPrice { get => _selectedItemPrice; set { if (value == _selectedItemPrice) return; _selectedItemPrice = value; OnPropertyChanged(); } }

        private string _selectedItemTheme;
        public string SelectedItemTheme { get => _selectedItemTheme; set { if (value == _selectedItemTheme) return; _selectedItemTheme = value; OnPropertyChanged(); } }

        private string _selectedItemType;
        public string SelectedItemType { get => _selectedItemType; set { if (value == _selectedItemType) return; _selectedItemType = value; OnPropertyChanged(); } }

        private string _filterString;
        public string FilterString { get => _filterString; set { if (value == _filterString) return; _filterString = value; OnPropertyChanged(); } }

        private string _selectedItemCompany;
        public string SelectedItemCompany { get => _selectedItemCompany; set { if (value == _selectedItemCompany) return; _selectedItemCompany = value; OnPropertyChanged(); } }

        #endregion

        #region properties binding

        private Visibility _messTextVisibility;
        public Visibility MessTextVisibility { get => _messTextVisibility; set { if (value == _messTextVisibility) return; _messTextVisibility = value; OnPropertyChanged(); } }

        

        #endregion

        #region command binding

        public ICommand LoadCommand { get; set; }

        public ICommand PreviousPageCommand { get; set; }
        public ICommand NextPageCommand { get; set; }

        public ICommand TypeSelectionChanged { get; set; }
        public ICommand ThemeSelectionChanged { get; set; }
        public ICommand AuthorSelectionChanged { get; set; }
        public ICommand CompanySelectionChanged { get; set; }
        public ICommand PriceSelectionChanged { get; set; }

        public ICommand searchCommand { get; set; }

        #endregion

        public ProductPageVM()
        {
            searchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
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

            PriceSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
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

            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentPage = 1;
                FilterString = "";
                ListBook = new ObservableCollection<CBook>(CBook.Ins.ListBook(FilterString, "tất cả", "tất cả", "tất cả", "tất cả", CurrentPage, NumberPage));

                ListPrice = new ObservableCollection<string> { "Tất cả", "Giảm giá" };

                ListTheme = new ObservableCollection<string>();
                ListTheme.Add("Tất cả");

                ListType = new ObservableCollection<string>(CBook.Ins.ListType());
                ListType.Add("Tất cả");

                ListAuthor = new ObservableCollection<string>(CBook.Ins.ListAuthor());
                ListAuthor.Add("Tất cả");

                ListCompany = new ObservableCollection<string>(CBook.Ins.ListCompany());
                ListCompany.Add("Tất cả");

                MessTextVisibility = Visibility.Hidden;

                SelectedItemAuthor = "Tất cả";
                SelectedItemCompany = "Tất cả";
                SelectedItemPrice = "Tất cả";
                SelectedItemType = "Tất cả";
                SelectedItemTheme = "Tất cả";
                
            }
               );
        }

        private void LoadBook()
        {
            if (SelectedItemPrice != null)
            {
                if(SelectedItemPrice=="Giảm giá")
                {
                    if (SelectedItemCompany != null && SelectedItemTheme != null && SelectedItemType != null && SelectedItemAuthor != null)
                    {
                        ListBook = new ObservableCollection<CBook>(CBook.Ins.ListBook(FilterString, SelectedItemAuthor, SelectedItemType, SelectedItemTheme, SelectedItemCompany, isSale, CurrentPage, NumberPage));
                    }
                }
                else
                {
                    if (SelectedItemCompany != null && SelectedItemTheme != null && SelectedItemType != null && SelectedItemAuthor != null)
                    {
                        ListBook = new ObservableCollection<CBook>(CBook.Ins.ListBook(FilterString, SelectedItemAuthor, SelectedItemType, SelectedItemTheme, SelectedItemCompany, CurrentPage, NumberPage));
                    }
                }
            }
            
            if (ListBook.Count() == 0)
            {
                MessTextVisibility = Visibility.Visible;
            }
            else
            {
                MessTextVisibility = Visibility.Hidden;
            }
        }
    }
}
