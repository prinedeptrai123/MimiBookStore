using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models.MyClass
{
    public class CBill
    {
        #region design pattern singleton

        private static CBill _ins;
        public static CBill Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new CBill();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }

        #endregion
        #region private properties

        private int _iD;
        private CCustomer _customerInfo;
        private CEmployee _employeeInfo;
        private float _sumMoney;
        private float _totalMoney;
        private DateTime _date;
        private float _promotion;
        private string _discountCode;
        private string _type;
        private float _customerCash;
        private float _excessCash;
        private List<CBookBill> _listBook;
        private int _bookCount;

        #endregion

        #region public properties
        /// <summary>
        /// mã hóa đơn
        /// </summary>
        public int ID { get => _iD; set { if (value == _iD) return;_iD = value; } }
        /// <summary>
        /// Thông tin khách hàng 
        /// </summary>
        public CCustomer CustomerInfo { get => _customerInfo; set { if (value == _customerInfo) return; _customerInfo = value; } }
        /// <summary>
        /// Thông tin nhân viên thanh toán
        /// </summary>
        public CEmployee EmployeeInfo { get => _employeeInfo; set { if (value == _employeeInfo) return; _employeeInfo = value; } }
        /// <summary>
        /// Tổng tiền ban đầu
        /// </summary>
        public float SumMoney { get => _sumMoney; set { if (value == _sumMoney) return; _sumMoney = value; } }
        /// <summary>
        /// Tổng tiền sau khi trừ đi khuyến mãi
        /// </summary>
        public float TotalMoney { get => _totalMoney; set { if (value == _totalMoney) return; _totalMoney = value; } }
        /// <summary>
        /// Ngày thanh toán
        /// </summary>
        public DateTime Date { get => _date; set { if (value == _date) return; _date = value; } }
        /// <summary>
        /// Thông tin khuyến mãi trong hóa đơn (%khuyến mãi)
        /// </summary>
        public float Promotion { get => _promotion; set { if (value == _promotion) return; _promotion = value; } }
        /// <summary>
        /// Mã khuyến mãi nếu có
        /// </summary>
        public string DiscountCode { get => _discountCode; set { if (value == _discountCode) return; _discountCode = value; } }
        /// <summary>
        /// Loại hóa đơn: thanh toán trực tiếp, đặt cọc...
        /// </summary>
        public string Type { get => _type; set { if (value == _type) return; _type = value; } }
        /// <summary>
        /// Danh sách sách trong hóa đơn
        /// </summary>
        public List<CBookBill> ListBook { get => _listBook; set { if (value == _listBook) return; _listBook = value; } }

        /// <summary>
        /// Tiền nhận từ khách hàng
        /// </summary>
        public float CustomerCash { get => _customerCash; set { if (value == _customerCash) return; _customerCash = value; } }

        /// <summary>
        /// Tiền thừa trả lại khách hàng
        /// </summary>
        public float ExcessCash { get => _excessCash; set { if (value == _excessCash) return; _excessCash = value; } }

        /// <summary>
        /// Tổng sách trong hóa đơn
        /// </summary>
        public int BookCount { get => _bookCount; set { if (value == _bookCount) return; _bookCount = value; } }

        #endregion

        #region method

        /// <summary>
        /// Hàm trả về danh sách loại khuyến mãi
        /// </summary>
        /// <param name="isAll"></param>
        /// <param name="currentPage"></param>
        /// <param name="NumberPage"></param>
        /// <returns></returns>
        public List<CPromotion_Type> ListTypeOfPromotion(bool isAll, int currentPage, int NumberPage)
        {
            List<CPromotion_Type> List = new List<CPromotion_Type>();
            try
            {
                using(var DB = new BookStoreDataEntities())
                {                  
                    var data = DB.Promotion_Type.ToList().Skip((currentPage - 1) * NumberPage).Take(NumberPage);
                    if (data.Count() > 0)
                    {
                        foreach(var item in data)
                        {                         
                            //Tạo mới 
                            CPromotion_Type Type = new CPromotion_Type
                            {
                                ID = item.Type_IDs,
                                Name = item.Type_Names,
                                BookCount = (int)item.Book_Count,
                                Promotion = (float)item.Type_Promotion,
                                IsExist = item.Exist,
                                Applied = item.Discount_Code.Count,
                                IsTrueValue=true
                            };

                            if(isAll == true && item.Exist == false)
                            {
                                List.Add(Type);
                            }else if( isAll == false && item.Exist == false)
                            {
                                continue;
                            }
                            else
                            {
                                List.Add(Type);
                            }
                        }
                    }
                }
            }
            catch
            {

            }

            return List;
        }

        /// <summary>
        /// Hàm trả về List theo tên
        /// </summary>
        /// <param name="TypeName"></param>
        /// <param name="isAll"></param>
        /// <param name="currentPage"></param>
        /// <param name="NumberPage"></param>
        /// <returns></returns>
        public List<CPromotion_Type> ListTypeOfPromotion(string TypeName,bool isAll, int currentPage, int NumberPage)
        {
            List<CPromotion_Type> List = new List<CPromotion_Type>();
            try
            {
                using (var DB = new BookStoreDataEntities())
                {
                    var data = DB.Promotion_Type.Where(x => x.Type_Names.ToLower().Contains(TypeName.ToLower())).ToList().Skip((currentPage - 1) * NumberPage).Take(NumberPage);

                    if (data.Count() > 0)
                    {
                        foreach (var item in data)
                        {
                            //Tạo mới 
                            CPromotion_Type Type = new CPromotion_Type
                            {
                                ID = item.Type_IDs,
                                Name = item.Type_Names,
                                BookCount = (int)item.Book_Count,
                                Promotion = (float)item.Type_Promotion,
                                IsExist = item.Exist,
                                Applied = item.Discount_Code.Count,
                                IsTrueValue=true
                            };

                            if (isAll == true && item.Exist == false)
                            {
                                List.Add(Type);
                            }
                            else if (isAll == false && item.Exist == false)
                            {
                                continue;
                            }
                            else
                            {
                                List.Add(Type);
                            }
                        }
                    }
                }
            }
            catch
            {

            }

            return List;
        }

        /// <summary>
        /// hàm trả về List string các loại khuyến mãi
        /// </summary>
        /// <returns></returns>
        public List<string> ListStringTypeOfPromotion()
        {
            List<string> List = new List<string>();
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var data = DB.Promotion_Type.Where(x => x.Exist == true);
                    if (data.Count() > 0)
                    {
                        foreach(var item in data)
                        {
                            List.Add(item.Type_Names);
                        }
                    }
                }
            }
            catch
            {

            }
            return List;
        }

        /// <summary>
        /// Hàm trả về Danh sách mã khuyến mãi
        /// </summary>
        /// <param name="isAll"></param>
        /// <param name="currentPage"></param>
        /// <param name="NumberPage"></param>
        /// <returns></returns>
        public List<CPromotion_Code> ListCode ( bool isAll,int currentPage, int NumberPage)
        {
            List<CPromotion_Code> List = new List<CPromotion_Code>();
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var data = DB.Discount_Code.ToList().Skip((currentPage - 1) * NumberPage).Take(NumberPage);
                    if (data.Count() > 0)
                    {
                        foreach(var item in data)
                        {
                            //tạo mới
                            CPromotion_Code Code = new CPromotion_Code
                            {
                                ID = item.Code_ID,
                                Name = item.Code_Name,
                                Type = item.Promotion_Type.Type_Names,
                                DateBegin = item.Date_Begin,
                                DateEnd = item.Date_End,
                                IsExist = item.Exist,
                                IstrueValue=true
                            };

                            if(isAll==true &&item.Exist == false)
                            {
                                List.Add(Code);
                            }
                            else if(isAll==false && item.Exist == false)
                            {
                                continue;
                            }
                            else
                            {
                                List.Add(Code);
                            }
                        }                        
                    }
                }
            }
            catch
            {

            }
            return List;
        }

        /// <summary>
        /// hàm trả về list khuyến mãi theo tên
        /// </summary>
        /// <param name="CodeName"></param>
        /// <param name="isAll"></param>
        /// <param name="currentPage"></param>
        /// <param name="NumberPage"></param>
        /// <returns></returns>
        public List<CPromotion_Code> ListCode(string CodeName,bool isAll, int currentPage, int NumberPage)
        {
            List<CPromotion_Code> List = new List<CPromotion_Code>();
            try
            {
                using (var DB = new BookStoreDataEntities())
                {
                    var data = DB.Discount_Code.Where(x => x.Code_Name.ToLower().Contains(CodeName.ToLower())).ToList().Skip((currentPage - 1) * NumberPage).Take(NumberPage);
                    if (data.Count() > 0)
                    {
                        foreach (var item in data)
                        {
                            //tạo mới
                            CPromotion_Code Code = new CPromotion_Code
                            {
                                ID = item.Code_ID,
                                Name = item.Code_Name,
                                Type = item.Promotion_Type.Type_Names,
                                DateBegin = item.Date_Begin,
                                DateEnd = item.Date_End,
                                IsExist = item.Exist,
                                IstrueValue = true
                            };

                            if (isAll == true && item.Exist == false)
                            {
                                List.Add(Code);
                            }
                            else if (isAll == false && item.Exist == false)
                            {
                                continue;
                            }
                            else
                            {
                                List.Add(Code);
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return List;
        }

        /// <summary>
        /// Hàm trả về Id của loại khuyến mãi, nếu không có trả về 0
        /// </summary>
        /// <param name="TypeName"></param>
        /// <returns></returns>
        public int isPromotionType(string TypeName)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var find = DB.Promotion_Type.Where(x => x.Type_Names == TypeName).FirstOrDefault();
                    if (find != null)
                    {
                        return find.Type_IDs;
                    }
                }
            }
            catch
            {

            }
            return 0;
        }

        /// <summary>
        /// Hàm thêm vào loại khuyến mãi mới
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public bool addNewPromotionType(CPromotion_Type Type)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    //Tạo mới
                    Promotion_Type newType = new Promotion_Type
                    {
                        Type_Names = Type.Name,
                        Book_Count = Type.BookCount,
                        Type_Promotion = Type.Promotion,
                        Exist=true
                    };

                    //Thêm
                    DB.Promotion_Type.Add(newType);

                    //Lưu thay đổi
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
        /// hàm đánh dấu loại đã bị xóa
        /// </summary>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public bool deletePromotionType(int TypeID)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    //Tìm loại theo ID
                    var find = DB.Promotion_Type.Find(TypeID);
                    if (find != null)
                    {
                        //Đánh dấu đã bị xóa
                        find.Exist = false;

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
        /// Hàm khôi phục lại loại khuyến mãi
        /// </summary>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public bool restorePromotionType(int TypeID)
        {
            try
            {
                using (var DB = new BookStoreDataEntities())
                {
                    //Tìm loại theo ID
                    var find = DB.Promotion_Type.Find(TypeID);
                    if (find != null)
                    {
                        //Khôi phục lại
                        find.Exist = true;

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
        /// hàm Cập nhật lại thông tin của loại khuyến mãi
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public bool updatePromotionType(CPromotion_Type Type)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    //Tìm loại theo id
                    var find = DB.Promotion_Type.Find(Type.ID);
                    if (find != null)
                    {
                        //Cập nhật lại thông tin mới
                        find.Type_Names = Type.Name;
                        find.Type_Promotion = Type.Promotion;
                        find.Book_Count = Type.BookCount;
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
        /// Hàm kiểm tra xem code đã tồn tại hay chưa
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public string isCode(string Code)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var find = DB.Discount_Code.Find(Code);
                    if (find != null)
                    {
                        return find.Code_ID;
                    }
                }
            }
            catch
            {

            }
            return "";
        }

        /// <summary>
        /// Hàm kiểm tra xem đã tồn tại tên này hay chưa
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public string isCodeName(string Name)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var find = DB.Discount_Code.Where(x => x.Code_Name.ToLower() == Name.ToLower()).FirstOrDefault();
                    if (find != null)
                    {
                        return find.Code_ID;
                    }
                }
            }
            catch
            {

            }
            return "";
        }

        /// <summary>
        /// Hàm thêm mã code mới
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public bool addNewCode(CPromotion_Code Code)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    //Tìm loại của Type
                    int TypeID = isPromotionType(Code.Type);
                    if (TypeID == 0)
                        return false;
                    //tạo mới
                    Discount_Code newCode = new Discount_Code
                    {
                        Code_ID = Code.ID,
                        Code_Name = Code.Name,
                        Code_Type = TypeID,
                        Date_Begin = Code.DateBegin,
                        Date_End = Code.DateEnd,
                        Exist = true
                    };

                    //Thêm vào
                    DB.Discount_Code.Add(newCode);

                    //Lưu thay đổi
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
        /// hàm đánh dấu mã đã bị xóa
        /// </summary>
        /// <param name="CodeID"></param>
        /// <returns></returns>
        public bool deleteCode(string CodeID)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var find = DB.Discount_Code.Find(CodeID);
                    if (find != null)
                    {
                        //Đánh dấu đã bị xóa
                        find.Exist = false;

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
        /// Hàm khôi phục CodeID
        /// </summary>
        /// <param name="CodeID"></param>
        /// <returns></returns>
        public bool restoreCode(string CodeID)
        {
            try
            {
                using (var DB = new BookStoreDataEntities())
                {
                    var find = DB.Discount_Code.Find(CodeID);
                    if (find != null)
                    {
                        //Đánh dấu đã bị xóa
                        find.Exist = true;

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

        public bool updateCode(CPromotion_Code Code)
        {

            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    //Tìm mã theo ID
                    var find = DB.Discount_Code.Find(Code.ID);
                    if (find != null)
                    {
                        //Lấy ra loại
                        int TypeID = isPromotionType(Code.Type);
                        //Cập nhật thông tin mới
                        find.Code_Name = Code.Name;
                        find.Code_Type = TypeID;
                        find.Date_Begin = Code.DateBegin;
                        find.Date_End = Code.DateEnd;

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
        /// hàm trả về tất cả các hình thức thanh toán của cửa hàng
        /// </summary>
        /// <returns></returns>
        public List<string> ListBillType()
        {
            List<string> List = new List<string>();
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var data = DB.Bill_Type.Where(x => x.Exist == true);
                    if (data.Count() > 0)
                    {
                        foreach(var item in data)
                        {
                            List.Add(item.Type_Names);
                        }
                    }
                }
            }
            catch
            {

            }
            return List;
        }

        /// <summary>
        /// Hàm trả về ID hình thức thanh toán
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public int IsBillType(string Type)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var find = DB.Bill_Type.Where(x => x.Type_Names.ToLower() == Type.ToLower()).FirstOrDefault();
                    if (find != null)
                    {
                        return find.Type_IDs;

                    }
                }
            }
            catch
            {

            }
            return 0;
        }

        /// <summary>
        /// hàm trả về thông tin code theo ID
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public CPromotion_Code PromotionOfCode(string Code)
        {           
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var find = DB.Discount_Code.Find(Code);
                    if (find != null)
                    {
                        if (find.Exist == false)
                        {
                            return null;
                        }

                        CPromotion_Code myCode = new CPromotion_Code
                        {
                            ID = find.Code_ID,
                            Name = find.Code_Name,
                            Promotion = (float)find.Promotion_Type.Type_Promotion,
                            BookCount = (int)find.Promotion_Type.Book_Count,
                            DateBegin = find.Date_Begin,
                            DateEnd = find.Date_End
                        };

                        return myCode;
                    }
                }
            }
            catch
            {

            }
            return null;
        }

        /// <summary>
        /// Hàm thêm vào thông tin của 1 bill mới, trả về id bill
        /// </summary>
        /// <param name="Bill"></param>
        /// <returns></returns>
        public int addnewBill(CBill mBill)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    //Thêm vào thông tin Bill
                    //tạo mới một Bill
                    int TypeID = IsBillType(mBill.Type);
                    if (TypeID == 0)
                    {
                        return 0;
                    }

                    Bill myBill = new Bill
                    {
                        Bill_Type = TypeID,
                        Customer_ID = mBill.CustomerInfo.ID,
                        Employee_ID = mBill.EmployeeInfo.ID,
                        Discount_Code = mBill.Promotion == 0 ? null : mBill.DiscountCode,
                        Sum_Money = mBill.SumMoney,
                        Total_Money = mBill.TotalMoney,
                        Excess_Cash = mBill.ExcessCash,
                        Customer_Cash = mBill.CustomerCash,
                        Bill_Date = mBill.Date
                    };

                    //Thêm vào
                    DB.Bills.Add(myBill);

                    //Lưu thay đổi
                    DB.SaveChanges();

                    //Tìm id của bill mới tạo
                    int BillID;
                    var find = DB.Bills.OrderByDescending(x => x.Bill_Date).FirstOrDefault();
                    if (find != null)
                    {
                        BillID = find.Bill_ID;
                    }
                    else
                    {
                        return 0;
                    }
                   
                    //Trả về
                    return BillID;
                
                }
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            return 0;
        }


        /// <summary>
        /// Hàm thêm vào chi tiết hóa đơn
        /// </summary>
        /// <param name="BillID"></param>
        /// <param name="Book"></param>
        /// <returns></returns>
        public bool addBillDetail(int BillID, CBookBill Book)
        {
            try
            {
                using (var DB = new BookStoreDataEntities())
                {
                    //Lấy ra số lượng sách bán đi 
                    int NumberBook = Book.Count;
                    //Với mỗi sách lấy ra một List nhập kho của sách đó lấy lần lượt những sách nhập trước
                    var BookInventory = DB.Book_Inventory.Where(x => x.Book_Count > 0 && x.Book_ID == Book.ID).OrderBy(x => x.Warehouse.Warehouse_Date);

                    foreach (var item in BookInventory)
                    {
                        //Kiểm tra xem số lượng của lần nhập đó có đủ để bán không
                        if (item.Book_Count - NumberBook >= 0)
                        {
                            //Trường hợp đủ để bán
                            //Thêm vào trong chi tiết
                            Bill_Detail Detail = new Bill_Detail
                            {
                                Bill_ID = BillID,
                                Book_ID = item.Book_ID,
                                Warehouse_ID = item.Warehouse_ID,
                                Book_Count = NumberBook,
                                Book_Price = Book.OutPrice,
                                Book_Promotion = Book.Promotion
                            };
                            //Thêm vào bảng detail
                            DB.Bill_Detail.Add(Detail);
                            //Lưu thay đổi
                            CBookInventory.InsInventory.decreaseInventory(item.Book_ID, item.Warehouse_ID, NumberBook);
                            //ngắt vòng lặp
                            break;
                        }
                        else
                        {
                            //Giảm số lượng của sách cần nhập xuống
                            NumberBook = NumberBook - item.Book_Count;

                            //Trường hợp không đủ để bán thì bán hết số lượng của lần đó
                            //Thêm vào trong chi tiết
                            Bill_Detail Detail = new Bill_Detail
                            {
                                Bill_ID = BillID,
                                Book_ID = item.Book_ID,
                                Warehouse_ID = item.Warehouse_ID,
                                Book_Count = item.Book_Count,
                                Book_Price = Book.OutPrice,
                                Book_Promotion = Book.Promotion
                            };
                            //Thêm vào bảng detail
                            DB.Bill_Detail.Add(Detail);

                            CBookInventory.InsInventory.decreaseInventory(item.Book_ID, item.Warehouse_ID, item.Book_Count);
                        }
                    }

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
        /// Hàm trả về danh sách hóa đơn thanh toán trong tháng
        /// </summary>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public List<CBill> ListBill(int Month,int Year, int currentPage, int NumberPage)
        {
            List<CBill> List = new List<CBill>();
            try
            {
                using (var DB = new BookStoreDataEntities())
                {
                    var data = DB.Bills.Where(x => SqlFunctions.DatePart("year",
                        x.Bill_Date) == Year && SqlFunctions.DatePart("month", x.Bill_Date) == Month).OrderByDescending(x => x.Bill_Date).ToList().
                        Skip((currentPage - 1) * NumberPage).Take(NumberPage);

                    if (data.Count() > 0)
                    {
                        foreach(var item in data)
                        {
                            //Tạo mới hóa đơn
                            CBill myBill = new CBill
                            {
                                ID=item.Bill_ID,
                                Date = item.Bill_Date,
                                TotalMoney = (float)item.Total_Money,
                                CustomerInfo = new CCustomer { Name = item.Customer.Customer_Name },
                                EmployeeInfo = new CEmployee { Name = item.Employee.Employee_Name },
                                Promotion = item.Discount_Code == null ? 0 : (float)item.Discount_Code1.Promotion_Type.Type_Promotion,
                                BookCount = item.Bill_Detail.Sum(x => x.Book_Count),
                                Type = item.Bill_Type1.Type_Names
                            };

                            //Thêm
                            List.Add(myBill);
                        }
                    }
                }

            }
            catch
            {

            }
            return List;
        }

        /// <summary>
        /// hàm trả về danh sách hóa đơn thanh toán từ ngày đến ngày
        /// </summary>
        /// <param name="DateBegin"></param>
        /// <param name="DateEnd"></param>
        /// <param name="currentPage"></param>
        /// <param name="NumberPage"></param>
        /// <returns></returns>
        public List<CBill> ListBill(DateTime DateBegin,DateTime DateEnd, int currentPage, int NumberPage)
        {
            List<CBill> List = new List<CBill>();
            try
            {
                using (var DB = new BookStoreDataEntities())
                {
                    var data = DB.Bills.Where(x => EntityFunctions.TruncateTime(x.Bill_Date) >= EntityFunctions.TruncateTime(DateBegin)
                    && EntityFunctions.TruncateTime(x.Bill_Date) <= EntityFunctions.TruncateTime(DateEnd)).OrderByDescending(x => x.Bill_Date).ToList().
                        Skip((currentPage - 1) * NumberPage).Take(NumberPage);

                    if (data.Count() > 0)
                    {
                        foreach (var item in data)
                        {
                            //Tạo mới hóa đơn
                            CBill myBill = new CBill
                            {
                                ID = item.Bill_ID,
                                Date = item.Bill_Date,
                                TotalMoney = (float)item.Total_Money,
                                CustomerInfo = new CCustomer { Name = item.Customer.Customer_Name },
                                EmployeeInfo = new CEmployee { Name = item.Employee.Employee_Name },
                                Promotion = item.Discount_Code == null ? 0 : (float)item.Discount_Code1.Promotion_Type.Type_Promotion,
                                BookCount = item.Bill_Detail.Sum(x => x.Book_Count),
                                Type = item.Bill_Type1.Type_Names
                            };

                            //Thêm
                            List.Add(myBill);
                        }
                    }
                }

            }
            catch
            {

            }
            return List;
        }

        /// <summary>
        /// hàm trả về chi tiết của hóa đơn theo ID
        /// </summary>
        /// <param name="BillID"></param>
        /// <returns></returns>
        public List<CBookBill> BillDetail(int BillID)
        {
            List<CBookBill> List = new List<CBookBill>();
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var data = DB.Bill_Detail.Where(x => x.Bill_ID == BillID);
                    if (data.Count() > 0)
                    {
                        foreach(var item in data)
                        {
                            //Tạo mới
                            CBookBill Detailt = new CBookBill
                            {
                                ID = item.Bill_ID,
                                Name = item.Book.Book_Name,
                                OutPrice = (float)item.Book_Price,
                                Promotion = (float)item.Book_Promotion,
                                OutPricePromotion = (float)(item.Book_Price - item.Book_Price * item.Book_Promotion),
                                Count = item.Book_Count,
                                TotalMoney = item.Book_Count * (float)(item.Book_Price - item.Book_Price * item.Book_Promotion)
                            };

                            //Thêm vào
                            List.Add(Detailt);
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
