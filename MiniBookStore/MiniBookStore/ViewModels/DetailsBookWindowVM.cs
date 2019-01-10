using MiniBookStore.Models.MyClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using MiniBookStore.Models;

namespace MiniBookStore.ViewModels
{
    public class DetailsBookWindowVM:BaseViewModel
    {
        #region Global

        string FileName;

        #endregion

        #region data binding

        private ObservableCollection<string> _listType;
        /// <summary>
        /// Danh sách loại sách
        /// </summary>
        public ObservableCollection<string> listType { get => _listType; set { if (value == _listType) return; _listType = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _listTheme;
        /// <summary>
        /// Danh sách thể loại
        /// </summary>
        public ObservableCollection<string> ListTheme { get => _listTheme; set { if (value == _listTheme) return; _listTheme = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _listCompany;
        public ObservableCollection<string> ListCompany { get => _listCompany; set { if (value == _listCompany) return; _listCompany = value; OnPropertyChanged(); } }

        private string _typeSelectedItem;
        public string TypeSelectedItem { get => _typeSelectedItem; set { if (value == _typeSelectedItem) return; _typeSelectedItem = value; OnPropertyChanged(); } }

        private string _name;
        public string Name { get => _name; set { if (value == _name) return; _name = value; OnPropertyChanged(); } }

        private string _author;
        public string Author { get => _author; set { if (value == _author) return; _author = value; OnPropertyChanged(); } }

        private string _type;
        public string Type { get => _type; set { if (value == _type) return; _type = value; OnPropertyChanged(); } }

        private string _theme;
        public string Theme { get => _theme; set { if (value == _theme) return; _theme = value; OnPropertyChanged(); } }

        private string _company;
        public string Company { get => _company; set { if (value == _company) return; _company = value; OnPropertyChanged(); } }

        private int _iD;
        public int ID { get => _iD; set { if (value == _iD) return; _iD = value; OnPropertyChanged(); } }

        private int _inventory;
        public int Inventory { get => _inventory; set { if (value == _inventory) return; _inventory = value; OnPropertyChanged(); } }

        private int _sold;
        public int Sold { get => _sold; set { if (value == _sold) return; _sold = value; OnPropertyChanged(); } }

        private string _outPrice;
        public string OutPrice { get => _outPrice; set { if (value == _outPrice) return; _outPrice = value; OnPropertyChanged(); } }

        private string _promotion;
        public string Promotion { get => _promotion; set { if (value == _promotion) return; _promotion = value; OnPropertyChanged(); } }

        private float _outPricePromotion;
        public float OutPricePromotion { get => _outPricePromotion; set { if (value == _outPricePromotion) return; _outPricePromotion = value; OnPropertyChanged(); } }

        private BitmapImage _coverImage;
        public BitmapImage CoverImage { get => _coverImage; set { if (value == _coverImage) return; _coverImage = value; OnPropertyChanged(); } }

        private ObservableCollection<CWarehouse_History> _listWarehouse;
        public ObservableCollection<CWarehouse_History> ListWarehouse { get => _listWarehouse; set { if (value == _listWarehouse) return; _listWarehouse = value; OnPropertyChanged(); } }

        #endregion


        #region properties binding

        private bool _isChecked;
        public bool IsChecked { get => _isChecked; set { if (value == _isChecked) return; _isChecked = value; OnPropertyChanged(); } }

        #endregion

        #region command binding

        public ICommand LoadCommand { get; set; }
        public ICommand TypeSelectionChanged { get; set; }
        public ICommand imageCommand { get; set; }
        public ICommand updateCommand { get; set; }

        public ICommand OutPriceTextChanged { get; set; }
        public ICommand PromotionTextChanged { get; set; }

        public ICommand CheckedCommand { get; set; }

        #endregion


        public DetailsBookWindowVM(CBook BookDetails)
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsChecked = false;
                //Cập nhật thông tin cho giao diện
                ID = BookDetails.ID;
                Name = BookDetails.Name;
                Author = BookDetails.Author;
                Type = BookDetails.Type;
                Theme = BookDetails.Theme;
                Inventory = BookDetails.Inventory;
                OutPrice = BookDetails.OutPrice.ToString();
                Promotion = BookDetails.Promotion.ToString();
                OutPricePromotion = BookDetails.OutPricePromotion;
                Company = BookDetails.Company;
                Sold = BookDetails.Sold;

                CoverImage = BookDetails.Image;

                listType = new ObservableCollection<string>(CBook.Ins.ListType());
                ListTheme = new ObservableCollection<string>();
                ListCompany = new ObservableCollection<string>(CBook.Ins.ListCompany());

                ListWarehouse = new ObservableCollection<CWarehouse_History>(CBookInventory.InsInventory.DetailsInventoryOfBook(BookDetails.ID, IsChecked));

            }
               );

            CheckedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListWarehouse = new ObservableCollection<CWarehouse_History>(CBookInventory.InsInventory.DetailsInventoryOfBook(BookDetails.ID, IsChecked));
            }
               );

            TypeSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (TypeSelectedItem != null)
                {
                    ListTheme = new ObservableCollection<string>(CBook.Ins.ListThemeOfType(TypeSelectedItem));
                }

            }
               );

            OutPriceTextChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if(Help.isFloat(OutPrice) ==true && Help.isFloat(Promotion) == true)
                {
                    OutPricePromotion = float.Parse(OutPrice) - float.Parse(OutPrice) * float.Parse(Promotion);
                }

            }
               );

            PromotionTextChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (Help.isFloat(OutPrice) == true && Help.isFloat(Promotion) == true)
                {
                    OutPricePromotion = float.Parse(OutPrice) - float.Parse(OutPrice) * float.Parse(Promotion);
                }
            }
               );

            imageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg|PNG|*.png", ValidateNames = true, Multiselect = false };

                var dialogOk = ofd.ShowDialog();
                if (dialogOk == true)
                {
                    FileName = ofd.FileName;
                    CoverImage = new BitmapImage(new Uri(FileName));
                }

            }
               );

            updateCommand = new RelayCommand<object>((p) => {
                if (CheckTrue() == false)
                    return false;
                return true;
            }, (p) =>
            {
                //Tạo mới thông tin của sách cần update
                CBook Book = new CBook
                {
                    ID = ID,
                    Name = Name,
                    Author = Author,
                    Type = Type,
                    Theme = Theme,
                    Company = Company,
                    Image = CoverImage,
                    OutPrice = float.Parse(OutPrice),
                    Promotion = float.Parse(Promotion)
                };
                //Gọi hàm update
                if (CBook.Ins.updateBookInfo(Book) == true)
                {
                    MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
               );
        }

        public bool CheckTrue()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Author) || string.IsNullOrEmpty(Type) || string.IsNullOrEmpty(Theme) ||
                string.IsNullOrEmpty(Company) || CoverImage == null || string.IsNullOrEmpty(Promotion) || string.IsNullOrEmpty(OutPrice))
            {
                return false;
            }

            float FOutPrice;
            float FPromotion;

            if (float.TryParse(Promotion, out FPromotion) == false || float.TryParse(OutPrice, out FOutPrice) == false)
            {
                return false;
            }

            if(FOutPrice <= 0)
            {
                return false;
            }

            if(FPromotion <0 || FPromotion >1)
            {
                return false;
            }

            //Kiểm tra sách có tồn tại trong kho hay chưa
            CBook Book = new CBook
            {
                ID=ID,
                Name = Name,
                Author = Author,
                Type = Type,
                Theme = Theme,
                Company = Company,
                Image = CoverImage,
                OutPrice = float.Parse(OutPrice),
                Promotion = float.Parse(Promotion)
            };
            int check = CBook.Ins.isExistBook(Book);
            if (check != ID && check!=0)
            {
                return false;
            }

            

            return true;
        }
    }
}
