using MiniBookStore.Models;
using MiniBookStore.Models.MyClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows;

namespace MiniBookStore.ViewModels
{
    public class MakeBillWindowVM:BaseViewModel
    {
        #region data binding

        private ObservableCollection<CBookBill> _listBook;
        public ObservableCollection<CBookBill> ListBook { get => _listBook; set { if (value == _listBook) return; _listBook = value; OnPropertyChanged(); } }

        private CBookBill _listBookSelectedItem;
        public CBookBill ListBookSelectedItem { get => _listBookSelectedItem; set { if (value == _listBookSelectedItem) return;_listBookSelectedItem = value;OnPropertyChanged(); } }

        private int _listBookSelectedIndex;
        public int ListBookSelectedIndex { get => _listBookSelectedIndex; set { if (value == _listBookSelectedIndex) return;_listBookSelectedIndex = value;OnPropertyChanged(); } }

        private BitmapImage _coverImage;
        public BitmapImage CoverImage { get => _coverImage; set { if (value == _coverImage) return; _coverImage = value; OnPropertyChanged(); } }

        private string _employeeName;
        public string EmployeeName { get => _employeeName; set { if (value == _employeeName) return; _employeeName = value; OnPropertyChanged(); } }

        private string _dateNow;
        public string DateNow { get => _dateNow; set { if (value == _dateNow) return; _dateNow = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _listTypePayment;
        public ObservableCollection<string> ListTypePayment { get => _listTypePayment; set { if (value == _listTypePayment) return; _listTypePayment = value; OnPropertyChanged(); } }

        private string _listTypePaymentSelectedItem;
        public string ListTypePaymentSelectedItem { get => _listTypePaymentSelectedItem; set { if (value == _listTypePaymentSelectedItem) return; _listTypePaymentSelectedItem = value; OnPropertyChanged(); } }

        private float _totalPrice;
        public float TotalPrice { get => _totalPrice; set { if (value == _totalPrice) return; _totalPrice = value; OnPropertyChanged(); } }

        private string _code;
        public string Code { get => _code; set { if (value == _code) return; _code = value; OnPropertyChanged(); } }

        private float _promotion;
        public float Promotion { get => _promotion; set { if (value == _promotion) return; _promotion = value; OnPropertyChanged(); } }

        private string _errorMess;
        public string ErrorMess { get => _errorMess; set { if (value == _errorMess) return; _errorMess = value; OnPropertyChanged(); } }

        private float lastTotalPrice;
        public float LastTotalPrice { get => lastTotalPrice; set { if (value == lastTotalPrice) return; lastTotalPrice = value; OnPropertyChanged(); } }

        private string _customerMoney;
        public string CustomerMoney { get => _customerMoney; set { if (value == _customerMoney) return; _customerMoney = value; OnPropertyChanged(); } }

        private float _excessCash;
        public float ExcessCash { get => _excessCash; set { if (value == _excessCash) return; _excessCash = value; OnPropertyChanged(); } }

        private bool _isMaleChecked;
        public bool IsMaleChecked { get => _isMaleChecked; set { if (value == _isMaleChecked) return; _isMaleChecked = value; OnPropertyChanged(); } }

        private bool _isFeMaleChecked;
        public bool IsFeMaleChecked { get => _isFeMaleChecked; set { if (value == _isFeMaleChecked) return; _isFeMaleChecked = value; OnPropertyChanged(); } }

        private bool _isCustomerChecked;
        public bool IsCustomerChecked { get => _isCustomerChecked; set { if (value == _isCustomerChecked) return; _isCustomerChecked = value; OnPropertyChanged(); } }

        /// <summary>
        /// Thông tin khách hàng
        /// </summary>
        
        private int _iD;
        public int ID { get => _iD; set { if (value == _iD) return; _iD = value; OnPropertyChanged(); } }

        private string _name;
        public string Name { get => _name; set { if (value == _name) return; _name = value; OnPropertyChanged(); } }

        private string _phone;
        public string Phone { get => _phone; set { if (value == _phone) return; _phone = value; OnPropertyChanged(); } }

        private string _email;
        public string Email { get => _email; set { if (value == _email) return; _email = value; OnPropertyChanged(); } }

        private string _address;
        public string Address { get => _address; set { if (value == _address) return; _address = value; OnPropertyChanged(); } }

        private ObservableCollection<CCustomer> _listCustomer;
        public ObservableCollection<CCustomer> ListCustomer { get => _listCustomer; set { if (value == _listCustomer) return; _listCustomer = value; OnPropertyChanged(); } }

        private CCustomer _listCustomerSelectedItem;
        public CCustomer ListCustomerSelectedItem { get => _listCustomerSelectedItem; set { if (value == _listCustomerSelectedItem) return; _listCustomerSelectedItem = value; OnPropertyChanged(); } }

        private string _filterString;
        public string FilterString { get => _filterString; set { if (value == _filterString) return; _filterString = value; OnPropertyChanged(); } }

        #endregion

        #region properties binding

        private Visibility _messVisibility;
        public Visibility MessVisibility { get => _messVisibility; set { if (value == _messVisibility) return; _messVisibility = value; OnPropertyChanged(); } }

        #endregion

        #region command binding

        public ICommand LoadCommand { get; set; }
        public ICommand ListBookSelectionChanged { get; set; }
        public ICommand deleteCommand { get; set; }
        public ICommand editCommand { get; set; }
        public ICommand CodeTextChangeCommand { get; set; }
        public ICommand CustomerTextChangeCommand { get; set; }

        public ICommand MaleCheckedCommand { get; set; }
        public ICommand FeMaleCheckedCommand { get; set; }

        public ICommand PayCommand { get; set; }
        public ICommand ListCustomerSelectionChanged { get; set; }
        public ICommand PhoneTextChange { get; set; }

        public ICommand searchCommand { get; set; }

        #endregion

        public MakeBillWindowVM()
        {
            PhoneTextChange = new RelayCommand<object>((p) => {
                return true;
            }, (p) =>
            {
                if (!string.IsNullOrEmpty(Phone))
                {
                    if (CCustomer.Ins.isCustomer(Phone) != 0)
                    {
                        IsCustomerChecked = true;
                    }
                    else
                    {
                        IsCustomerChecked = false;
                    }
                }
                else
                {
                    IsCustomerChecked = false;
                }
            }
               );

            searchCommand = new RelayCommand<object>((p) => {
                return true;
            }, (p) =>
            {
                if (Help.isInt(FilterString)==true)
                {
                    ListCustomer = new ObservableCollection<CCustomer>(CCustomer.Ins.ListCustomerFilterPhone(FilterString));
                }
                else
                {
                    ListCustomer = new ObservableCollection<CCustomer>(CCustomer.Ins.ListCustomerFilterName(FilterString));
                }
                
            }
              );

            ListCustomerSelectionChanged = new RelayCommand<object>((p) => {
                return true;
            }, (p) =>
            {
                if (ListCustomerSelectedItem != null)
                {
                    ID = ListCustomerSelectedItem.ID;
                    Name = ListCustomerSelectedItem.Name;
                    Phone = ListCustomerSelectedItem.Phone;
                    Address = ListCustomerSelectedItem.Address;
                    Email = ListCustomerSelectedItem.Email;

                    if (ListCustomerSelectedItem.Gender == "Nam")
                    {
                        IsMaleChecked = true;
                        IsFeMaleChecked = false;
                    }
                    else
                    {
                        IsMaleChecked = false;
                        IsFeMaleChecked = true;
                    }

                    IsCustomerChecked = true;
                }
            }
               );

            PayCommand = new RelayCommand<object>((p) => {
                if (ListBook.Count == 0)
                {
                    return false;
                }
                if(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(CustomerMoney))
                {
                    return false;
                }
                if (Help.isFloat(CustomerMoney) == false)
                {
                    return false;
                }
                if (ExcessCash < 0)
                {
                    return false;
                }

                if (Help.isInt(Phone) == false)
                {
                    return false;
                }

                if (!string.IsNullOrEmpty(Email))
                {
                    if (Help.isEmail(Email) == false)
                    {
                        return false;
                    }
                }
                return true;
            }, (p) =>
            {
                //Lấy ID của khách hàng
                int CustomerID = CCustomer.Ins.isCustomer(Phone);
                if (CustomerID == 0)
                {
                    //Tạo mới một khách hàng
                    CCustomer myCustomer = new CCustomer
                    {
                        Name = Name,
                        Phone = Phone,
                        Email = Email,
                        Address = Address,
                        Gender = IsFeMaleChecked == true ? "Nữ" : "Nam",
                    };

                    CustomerID = CCustomer.Ins.addCustomer(myCustomer);
                }

                //Tạo mới một Bill
                CBill Bill = new CBill
                {
                    EmployeeInfo = new CEmployee { ID = DataTransfer.EmployeeInfo.ID },
                    CustomerInfo = new CCustomer { ID = CustomerID },
                    Type = ListTypePaymentSelectedItem,
                    DiscountCode = Code,
                    SumMoney = TotalPrice,
                    TotalMoney = LastTotalPrice,
                    CustomerCash = float.Parse(CustomerMoney),
                    ExcessCash = ExcessCash,
                    Promotion = Promotion / 100,
                    Date = DateTime.Now,
                    ListBook = ListBook.ToList()
                };

                int BillID = CBill.Ins.addnewBill(Bill);
                //Thanh toán
                if (BillID != 0)
                {
                    //Duyệt trong List và thêm vào lịch sử thanh toán
                    foreach(var item in ListBook)
                    {
                        CBill.Ins.addBillDetail(BillID, item);

                        //Trừ số lượng trong kho
                        CBook.Ins.decreaseBook(item.ID, item.Count);
                    }

                    MessageBox.Show("Thanh toán thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                    //Trả list về rỗng
                    ListBook.Clear();

                    ID = 0;
                    Name = "";
                    Phone = "";
                    Email = "";
                    Address = "";
                    Code = "";
                    Promotion = 0;

                    TotalPrice = 0;
                    lastTotalPrice = 0;
                    CustomerMoney = "";
                    ExcessCash = 0;

                }
                else
                {
                    MessageBox.Show("Thanh toán thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
               );

            MaleCheckedCommand = new RelayCommand<object>((p) => {               
                return true;
            }, (p) =>
            {
                if (IsFeMaleChecked == false)
                {
                    IsMaleChecked = true;
                }
                else
                {
                    if (IsMaleChecked == true)
                    {
                        IsFeMaleChecked = false;
                    }
                }
                
            }
               );

            FeMaleCheckedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (IsMaleChecked == false)
                {
                    IsFeMaleChecked = true;
                }
                else
                {
                    if (IsFeMaleChecked == true)
                    {
                        IsMaleChecked = false;
                    }
                }               
            }
               );

            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListBook = DataTransfer.ListBookBill;
                EmployeeName = DataTransfer.EmployeeInfo.Name;
                DateNow = DateTime.Now.ToShortDateString();
                if (ListBook.Count == 1)
                {
                    CoverImage = ListBook[0].Image;
                }

                ListTypePayment = new ObservableCollection<string>(CBill.Ins.ListBillType());
                ListTypePaymentSelectedItem = "Thanh toán trực tiếp";

                TotalPrice = ListBook.Sum(x => x.TotalMoney);
                LastTotalPrice = ListBook.Sum(x => x.TotalMoney) - ListBook.Sum(x => x.TotalMoney) * Promotion / 100;

                MessVisibility = Visibility.Collapsed;

                ErrorMess = "Mã code không hợp lệ";

                Promotion = 0;
                ExcessCash = 0;

                IsMaleChecked = true;
                IsFeMaleChecked = false;

                ListCustomer = new ObservableCollection<CCustomer>(CCustomer.Ins.ListCustomerFilterPhone(""));
            }
               );

            CustomerTextChangeCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (string.IsNullOrEmpty(CustomerMoney))
                {
                    ExcessCash = 0;
                }
                else
                {
                    if (Help.isFloat(CustomerMoney) == true)
                    {
                        ExcessCash = float.Parse(CustomerMoney) - LastTotalPrice;
                    }
                }
                
            }
               );

            CodeTextChangeCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                
                if (Code == "")
                {
                    MessVisibility = Visibility.Collapsed;
                    Promotion = 0;
                    LastTotalPrice = ListBook.Sum(x => x.TotalMoney) - ListBook.Sum(x => x.TotalMoney) * Promotion / 100;
                }
                else
                {
                    CPromotion_Code myCode = CBill.Ins.PromotionOfCode(Code);
                    if (myCode != null)
                    {
                        if(myCode.DateEnd < DateTime.Now)
                        {
                            ErrorMess = "Mã này đã hết hạn";
                            MessVisibility = Visibility.Visible;
                            Promotion = 0;
                            LastTotalPrice = ListBook.Sum(x => x.TotalMoney) - ListBook.Sum(x => x.TotalMoney) * Promotion / 100;
                        }
                        else
                        {
                            if (myCode.BookCount == 0)
                            {
                                MessVisibility = Visibility.Collapsed;
                                Promotion = myCode.Promotion * 100;
                                LastTotalPrice = ListBook.Sum(x => x.TotalMoney) - ListBook.Sum(x => x.TotalMoney) * Promotion / 100;
                            }
                            else
                            {
                                if (myCode.BookCount > ListBook.Sum(x => x.Count))
                                {
                                    ErrorMess = "Số lượng sách mua không đủ";
                                    MessVisibility = Visibility.Visible;
                                    Promotion = 0;
                                    LastTotalPrice = ListBook.Sum(x => x.TotalMoney) - ListBook.Sum(x => x.TotalMoney) * Promotion / 100;
                                }
                                else
                                {
                                    MessVisibility = Visibility.Collapsed;
                                    Promotion = myCode.Promotion * 100;
                                    LastTotalPrice = ListBook.Sum(x => x.TotalMoney) - ListBook.Sum(x => x.TotalMoney) * Promotion / 100;
                                }
                            }
                        }                          
                    }
                    else
                    {
                        ErrorMess = "Mã code không hợp lệ";
                        MessVisibility = Visibility.Visible;
                        Promotion = 0;
                        LastTotalPrice = ListBook.Sum(x => x.TotalMoney) - ListBook.Sum(x => x.TotalMoney) * Promotion / 100;
                    }
                }
                
            }
               );

            editCommand = new RelayCommand<object>((p) => {
                if (ListBookSelectedItem != null)
                {
                    if (ListBookSelectedItem.Count <= 0)
                    {
                        ListBookSelectedItem.IsTrueValue = false;
                    }
                    else
                    {
                        if(ListBookSelectedItem.Inventory+1 - ListBookSelectedItem.Count < 0)
                        {
                            ListBookSelectedItem.IsTrueValue = false;

                        }
                        else
                        {
                            ListBookSelectedItem.IsTrueValue = true;
                        }                                          
                    }
                }
                return true;
            }, (p) =>
            {
                if (ListBookSelectedItem != null)
                {
                    ListBookSelectedItem.Inventory = ListBookSelectedItem.Inventory + 1 - ListBookSelectedItem.Count;
                    //Cập nhật lại tổng tiền
                    ListBookSelectedItem.TotalMoney = ListBookSelectedItem.OutPricePromotion * ListBookSelectedItem.Count;
                    TotalPrice = ListBook.Sum(x => x.TotalMoney);
                    LastTotalPrice = ListBook.Sum(x => x.TotalMoney) - ListBook.Sum(x => x.TotalMoney) * Promotion / 100;
                }
            }
               );

            deleteCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (ListBookSelectedItem != null)
                {
                    //Xóa theo index
                    ListBook.RemoveAt(ListBookSelectedIndex);
                    //Cập nhật lại tổng tiền
                    TotalPrice = ListBook.Sum(x => x.TotalMoney);
                    LastTotalPrice = ListBook.Sum(x => x.TotalMoney) - ListBook.Sum(x => x.TotalMoney) * Promotion / 100;

                }
            }
               );

            ListBookSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (ListBookSelectedItem != null)
                {
                    CoverImage = ListBookSelectedItem.Image;
                }
            }
               );
        }
    }
}
