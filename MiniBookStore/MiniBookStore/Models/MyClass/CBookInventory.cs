using MiniBookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models.MyClass
{
    /// <summary>
    /// Lưu thông tin của sách nhập theo đợt
    /// </summary>
    public class CBookInventory : CBook
    {
        #region design pattern singleton

        private static CBookInventory _insInventory;
        public static CBookInventory InsInventory
        {
            get
            {
                if (_insInventory == null)
                    _insInventory = new CBookInventory();
                return _insInventory;
            }
            set
            {
                _insInventory = value;
            }
        }

        #endregion

        #region private properties

        private int _warehouseID;
        private int _warehouseInventory;
        private float _inPrice;
        private float _totalPrice;

        #endregion

        #region public properties
        /// <summary>
        /// Mã đợt nhập kho
        /// </summary>
        public int WarehouseID { get => _warehouseID; set { if (value == _warehouseID) return; _warehouseID = value; } }
        /// <summary>
        /// Số lượng sách tồn kho của đợt nhập này
        /// </summary>
        public int WarehouseInventory { get => _warehouseInventory; set { if (value == _warehouseInventory) return; _warehouseInventory = value; OnPropertyChanged(); } }
        /// <summary>
        /// Giá nhập sách của đợt nhập này
        /// </summary>
        public float InPrice { get => _inPrice; set { if (value == _inPrice) return; _inPrice = value; OnPropertyChanged(); } }

        /// <summary>
        /// Tổng giá nhập của đợt nhập này
        /// </summary>
        public float TotalPrice { get => _totalPrice; set { if (value == _totalPrice) return; _totalPrice = value; OnPropertyChanged(); } }

        #endregion

        #region method

        /// <summary>
        /// Hàm thêm sách mới vào kho
        /// </summary>
        /// <param name="Book"></param>
        /// <returns></returns>
        public int addNewBook(CBookInventory Book)
        {
            try
            {
                int CompanyID;
                int TypeID;
                int ThemeID;
                using(var DB = new BookStoreDataEntities())
                {
                    //Lấy id công ty xuất bản
                    CompanyID = isCompany(Book.Company);

                    //lấy id thể loại
                    TypeID = isType(Book.Type);

                    //Lấy id chủ đề

                    ThemeID = isTheme(Book.Theme);

                    if(CompanyID !=0 && TypeID!=0 && ThemeID != 0)
                    {
                        //Tạo mới sách
                        var bookdata = new Book
                        {
                            Book_Name = Book.Name,
                            Book_Author=Book.Author,
                            Book_Company=CompanyID,
                            Book_Theme = ThemeID,
                            Book_Type = TypeID,
                            Book_Price = Book.InPrice*1.3,
                            Book_Promotion=0,
                            Book_Count = Book.WarehouseInventory,
                            Book_Image = Help.ImageToByte(Book.Image)
                        };

                        //Thêm sách
                        DB.Books.Add(bookdata);

                        //Lưu thay đổi
                        DB.SaveChanges();
                    }                  
                }

                //Tìm id của sách mới nhập
                using(var DB = new BookStoreDataEntities())
                {
                    var find = DB.Books.Where(x => x.Book_Name == Book.Name && x.Book_Author == Book.Author &&
                    x.Book_Theme == ThemeID && x.Book_Type == TypeID && x.Book_Company == CompanyID).Select(x => x.Book_ID).FirstOrDefault();

                    if (find != 0)
                    {
                        return find;
                    }
                }
            }
            catch
            {

            }
            return 0;
        }

        /// <summary>
        /// Hàm nhập thêm vào lịch sử nhập kho
        /// </summary>
        /// <param name="EmployeeID">id nhân viên nhập</param>
        /// <param name="Type">false nếu nhập mới, true nếu nhập thêm</param>
        /// <param name="totalmoney">tổng tiền của đợt nhập</param>
        /// <param name="DateNow">Thời gian nhập</param>
        /// <returns>id nhập kho</returns>
        public int addWarehouse(int EmployeeID, bool Type, float totalmoney, DateTime DateNow)
        {
            try
            {
                using (var DB = new BookStoreDataEntities())
                {
                    //Tạo mới
                    Warehouse data = new Warehouse
                    {
                        Employee_ID = EmployeeID,
                        Warehouse_Type = Type,
                        Warehouse_Toltal_Money = totalmoney,
                        Warehouse_Date = DateNow
                    };

                    //Thêm vào
                    DB.Warehouses.Add(data);

                    //Lưu thay đổi
                    DB.SaveChanges();
                }

                //Tìm id của lần nhập kho hiện tại
                using (var DB = new BookStoreDataEntities())
                {
                    var ID = DB.Warehouses.Where(x => x.Employee_ID == EmployeeID).OrderByDescending(x => x.Warehouse_Date).Select(x => x.Warehouse_ID).FirstOrDefault();
                    return ID;
                }
            }
            catch
            {
                
            }
            return 0;
        }

        /// <summary>
        /// Hàm thêm vào chi tiết nhập kho
        /// </summary>
        /// <param name="WarehoseID"></param>
        /// <param name="BookID"></param>
        /// <param name="BookCout"></param>
        /// <param name="BookPrice"></param>
        /// <returns></returns>
        public bool addDetailWarehouse(int WarehouseID,int BookID,int BookCout,float BookPrice)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    if(WarehouseID!=0 && BookID != 0)
                    {
                        //tạo mới
                        Warehouse_Detail data = new Warehouse_Detail
                        {
                            Warehouse_ID = WarehouseID,
                            Book_ID = BookID,
                            Book_Count = BookCout,
                            Book_Price = BookPrice
                        };

                        //Thêm vào
                        DB.Warehouse_Detail.Add(data);

                        //Lưu thay đổi
                        DB.SaveChanges();

                        return true;
                    }
                }
            }
            catch
            {

            }
            return false;
        }

        /// <summary>
        /// hàm thêm vào bảng lưu thông tin số lượng sách của đợt nhập đó
        /// </summary>
        /// <param name="BookID"></param>
        /// <param name="WarehouseID"></param>
        /// <param name="BookCount"></param>
        /// <returns></returns>
        public bool addBookInventory(int BookID,int WarehouseID,int BookCount)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    //Tạo mới
                    var data = new Book_Inventory
                    {
                        Book_ID = BookID,
                        Book_Count = BookCount,
                        Warehouse_ID = WarehouseID
                    };

                    //Thêm vào
                    DB.Book_Inventory.Add(data);

                    //Lưu
                    DB.SaveChanges();

                    return true;
                }
            }
            catch
            {

            }
            return false;
        }

        /// <summary>
        /// Hàm trả về thông tin ngày cuối nhập kho
        /// </summary>
        /// <returns></returns>
        public CWarehouse_History LastWarehouse()
        {
            CWarehouse_History data = new CWarehouse_History();
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var find = DB.Warehouses.OrderByDescending(x => x.Warehouse_Date).FirstOrDefault();

                    if (find != null)
                    {
                        data.Date = find.Warehouse_Date;
                        data.ToltalMoney = (float)find.Warehouse_Toltal_Money;
                        data.TotalCount = find.Warehouse_Detail.Sum(x => x.Book_Count);
                    }                  
                }
            }
            catch
            {

            }

            return data;
        }

        /// <summary>
        /// Hàm trả về chi tiết số lượng tồn trong các đợt nhập sách của sách nếu isAll == true thì trả về tất cả nếu isAll== false thì trả về những sách tồn(giá trị lớn hơn 0)
        /// </summary>
        /// <param name="BookID"></param>
        /// <param name="isAll"></param>
        /// <returns></returns>
        public List<CWarehouse_History> DetailsInventoryOfBook(int BookID,bool isAll)
        {
            List<CWarehouse_History> List = new List<CWarehouse_History>();
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var data = DB.Book_Inventory.Where(x => x.Book_ID == BookID);

                    foreach (var item in data)
                    {
                        //Lấy ra giá nhập sách của đợt này
                        float Price = (float)DB.Warehouse_Detail.Where(x => x.Warehouse_ID == item.Warehouse_ID && x.Book_ID == item.Book_ID).Select(x => x.Book_Price).FirstOrDefault();
                        CWarehouse_History history = new CWarehouse_History
                        {
                            ID = item.Warehouse_ID,
                            TotalCount = item.Book_Count,
                            InPrice = Price,
                            Date = item.Warehouse.Warehouse_Date,
                            Type = item.Warehouse.Warehouse_Type == false ? "Nhập mới" : "Nhập thêm"
                        };

                        if(isAll==true && item.Book_Count == 0)
                        {
                            List.Add(history);
                        }
                        else if(isAll==false && item.Book_Count==0)
                        {
                            continue;
                        }
                        else
                        {
                            List.Add(history);
                        }
                    }
                }
            }
            catch
            {

            }

            return List;
        }

        #endregion
    }
}
