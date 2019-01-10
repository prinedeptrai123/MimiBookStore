using MiniBookStore.Models;
using MiniBookStore.Models.MyClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.IO;



namespace MiniBookStore.ViewModels
{
    public class AddNewBookWindowVM:BaseViewModel
    {

        public string FileName;

        #region data binding

        private string _employeeName;
        public string EmployeeName { get => _employeeName; set { if (value == _employeeName) return;_employeeName = value;OnPropertyChanged(); } }

        private string _dateNow;
        public string DateNow { get => _dateNow; set { if (value == _dateNow) return;_dateNow = value;OnPropertyChanged(); } }

        private ObservableCollection<CBookInventory> _listBook;
        /// <summary>
        /// Danh sách sách hiển thị trên màn hình
        /// </summary>
        public ObservableCollection<CBookInventory> ListBook { get => _listBook; set { if (value == _listBook) return;_listBook = value;OnPropertyChanged(); } }

        private int _listSelectedIndex;
        public int ListSelectedIndex { get => _listSelectedIndex; set { if (value == _listSelectedIndex) return; _listSelectedIndex = value; OnPropertyChanged(); } }

        private CBookInventory _listSelectedItem;
        public CBookInventory ListSelectedItem { get => _listSelectedItem; set { if (value == _listSelectedItem) return; _listSelectedItem = value; OnPropertyChanged(); } }

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
        public string Name { get => _name; set { if (value == _name) return;_name = value;OnPropertyChanged(); } }

        private string _author;
        public string Author { get => _author; set { if (value == _author) return; _author = value; OnPropertyChanged(); } }

        private string _type;
        public string Type { get => _type; set { if (value == _type) return; _type = value; OnPropertyChanged(); } }

        private string _theme;
        public string Theme { get => _theme; set { if (value == _theme) return; _theme = value; OnPropertyChanged(); } }

        private string _company;
        public string Company { get => _company; set { if (value == _company) return; _company = value; OnPropertyChanged(); } }

        private string _warehouseInventory;
        public string WarehouseInventory { get => _warehouseInventory; set { if (value == _warehouseInventory) return; _warehouseInventory = value; OnPropertyChanged(); } }

        private string _inPrice;
        public string InPrice { get => _inPrice; set { if (value == _inPrice) return; _inPrice = value; OnPropertyChanged(); } }

        private float _totalPrice;
        public float TotalPrice {get => _totalPrice; set { if (value == _totalPrice) return; _totalPrice = value; OnPropertyChanged(); }}

        private BitmapImage _coverImage;
        public BitmapImage CoverImage { get => _coverImage;set { if (value == _coverImage) return;_coverImage = value;OnPropertyChanged(); } }
        
            

        #endregion

        #region properties binding


        #endregion


        #region command binding
        public ICommand LoadCommand { get; set; }

        public ICommand addToListCommand { get; set; }
        public ICommand editListCommand { get; set; }
        public ICommand deleteListCommand { get; set; }

        public ICommand addCommand { get; set; }
        public ICommand imageCommand { get; set; }

        public ICommand TypeSelectionChanged { get; set; }

        public ICommand ListSelectionChanged { get; set; }


        #endregion

        public AddNewBookWindowVM()
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                EmployeeName = DataTransfer.EmployeeInfo.Name;
                DateNow = DateTime.Now.ToShortDateString();

                ListBook = new ObservableCollection<CBookInventory>();
                listType = new ObservableCollection<string>(CBook.Ins.ListType());
                ListTheme = new ObservableCollection<string>();
                ListCompany = new ObservableCollection<string>(CBook.Ins.ListCompany());
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

            deleteListCommand = new RelayCommand<object>((p) => {
                if (ListBook.Count() == 0)
                    return false;
                if (ListSelectedItem == null)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                if (ListSelectedItem != null)
                {
                    //Xóa theo index
                    ListBook.RemoveAt(ListSelectedIndex);
                }

            }
               );

            editListCommand = new RelayCommand<object>((p) => {
                if (ListBook.Count() == 0)
                    return false;
                if (ListSelectedItem == null)
                {
                    return false;
                }

                if (CheckTrue2() == false)
                {
                    return false;
                }


                return true;
            }, (p) =>
            {
                if (ListSelectedItem != null)
                {
                    //Cập nhật theo index
                    //Tạo mới một sách
                    CBookInventory Book = new CBookInventory
                    {
                        Name = Name,
                        Author = Author,
                        Type = Type,
                        Theme = Theme,
                        Company = Company,
                        Image = CoverImage,
                        InPrice = float.Parse(InPrice),
                        WarehouseInventory = int.Parse(WarehouseInventory),
                        TotalPrice = float.Parse(InPrice) * int.Parse(WarehouseInventory)
                    };

                    ListBook[ListSelectedIndex] = Book;
                }

            }
               );

            addCommand = new RelayCommand<object>((p) => {
                if (ListBook.Count() == 0)
                    return false;
                
                return true;
            }, (p) =>
            {
                //Tính tổng tiền của của tất cả các sách sẽ nhập
                float Totalmoney = ListBook.Sum(x => x.TotalPrice);

                //Tạo mới một lịch sử nhập kho
                int WarehouseID = CBookInventory.InsInventory.addWarehouse(DataTransfer.EmployeeInfo.ID, false, Totalmoney, DateTime.Now);

                //Duyệt trong danh sách sách trong List và thêm vào trong kho
                foreach(var item in ListBook)
                {
                    //Thêm sách vào bảng sách
                    int BookID = CBookInventory.InsInventory.addNewBook(item);

                    //Thêm vào chi tiết lịch sử nhập kho
                    CBookInventory.InsInventory.addDetailWarehouse(WarehouseID, BookID, item.WarehouseInventory, item.InPrice);

                    //Thêm vào bảng lưu số lượng sách trong đợt nhập này
                    CBookInventory.InsInventory.addBookInventory(BookID, WarehouseID, item.WarehouseInventory);
                }

                //Thông báo nhập thành công
                MessageBox.Show("Nhập thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                //Làm mới ListBook
                ListBook.Clear();

            }
               );

            ListSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (ListSelectedItem != null)
                {
                    //Đưa lại dữ liệu lên trên
                    Name = ListSelectedItem.Name;
                    Author = ListSelectedItem.Author;
                    Type = ListSelectedItem.Type;
                    Theme = ListSelectedItem.Theme;
                    InPrice = ListSelectedItem.InPrice.ToString();
                    WarehouseInventory = ListSelectedItem.WarehouseInventory.ToString();
                    TotalPrice = ListSelectedItem.TotalPrice;
                    CoverImage = ListSelectedItem.Image;
                    Company = ListSelectedItem.Company;
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

            addToListCommand = new RelayCommand<object>((p) =>
            {
                if (CheckTrue() == false)
                    return false;
                return true;
               
            }
            , (p) =>
            {
                //Tạo mới một sách
                CBookInventory Book = new CBookInventory
                {
                    Name = Name,
                    Author = Author,
                    Type = Type,
                    Theme = Theme,
                    Company = Company,
                    Image = CoverImage,
                    InPrice = float.Parse(InPrice),
                    WarehouseInventory = int.Parse(WarehouseInventory),
                    TotalPrice = float.Parse(InPrice) * int.Parse(WarehouseInventory)
                };

                ListBook.Add(Book);
            }
               );
        }

        /// <summary>
        /// Kiểm tra sách đã tồn tại trong danh sách bên dưới hay chưa
        /// </summary>
        /// <param name="Book"></param>
        /// <returns></returns>
        public bool isExistInList(CBookInventory Book)
        {
            if (ListBook.Count() > 0)
            {
                if (ListBook.Where(x => x.Name.ToLower() == Book.Name.ToLower() && x.Author.ToLower() == Book.Author.ToLower() &&
             x.Type.ToLower() == Book.Type.ToLower() && Book.Theme.ToLower() == x.Theme.ToLower() && x.Company.ToLower() == Book.Company.ToLower()).Count() > 0)
                {
                    return true;
                }
            }
            
            return false;
        }

        public bool isisExistInList2(CBookInventory Book)
        {
            if (ListBook.Count() > 1)
            {
                if (ListBook.Where(x => x.Name.ToLower() == Book.Name.ToLower() && x.Author.ToLower() == Book.Author.ToLower() &&
             x.Type.ToLower() == Book.Type.ToLower() && Book.Theme.ToLower() == x.Theme.ToLower() && x.Company.ToLower() == Book.Company.ToLower()).Count() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckTrue()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Author) || string.IsNullOrEmpty(Type) || string.IsNullOrEmpty(Theme) ||
                string.IsNullOrEmpty(Company) || CoverImage == null || string.IsNullOrEmpty(WarehouseInventory) || string.IsNullOrEmpty(InPrice))
            {
                return false;
            }


            float FInPrice;
            int IWarehouseInventory;

            if (int.TryParse(WarehouseInventory, out IWarehouseInventory) == false || float.TryParse(InPrice, out FInPrice) == false)
            {
                return false;
            }

            //Kiểm tra sách có tồn tại trong kho hay chưa
            CBookInventory Book = new CBookInventory
            {
                Name = Name,
                Author = Author,
                Type = Type,
                Theme = Theme,
                Company = Company,
                Image = CoverImage,
                InPrice = float.Parse(InPrice),
                WarehouseInventory = int.Parse(WarehouseInventory),
                TotalPrice = float.Parse(InPrice) * int.Parse(WarehouseInventory)
            };
            if (CBook.Ins.isExistBook(Book) != 0)
            {
                return false;
            }

            //Kiểm tra sách có tồn tại ở trong List bên dưới hay chưa
            if (isExistInList(Book) == true)
            {
                return false;
            }

            return true;
        }

        public bool CheckTrue2()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Author) || string.IsNullOrEmpty(Type) || string.IsNullOrEmpty(Theme) ||
                string.IsNullOrEmpty(Company) || CoverImage == null || string.IsNullOrEmpty(WarehouseInventory) || string.IsNullOrEmpty(InPrice))
            {
                return false;
            }


            float FInPrice;
            int IWarehouseInventory;

            if (int.TryParse(WarehouseInventory, out IWarehouseInventory) == false || float.TryParse(InPrice, out FInPrice) == false)
            {
                return false;
            }

            //Kiểm tra sách có tồn tại trong kho hay chưa
            CBookInventory Book = new CBookInventory
            {
                Name = Name,
                Author = Author,
                Type = Type,
                Theme = Theme,
                Company = Company,
                Image = CoverImage,
                InPrice = float.Parse(InPrice),
                WarehouseInventory = int.Parse(WarehouseInventory),
                TotalPrice = float.Parse(InPrice) * int.Parse(WarehouseInventory)
            };
            if (CBook.Ins.isExistBook(Book) != 0)
            {
                return false;
            }

            //Kiểm tra sách có tồn tại 2 cuốn ở trong List bên dưới hay chưa
            if (isisExistInList2(Book) == true)
            {
                return false;
            }

            return true;
        }
    }
}
