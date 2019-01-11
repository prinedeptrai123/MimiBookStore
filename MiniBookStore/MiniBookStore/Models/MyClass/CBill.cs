using System;
using System.Collections.Generic;
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
        private List<CBookBill> _listBook;

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
        #endregion
    }
}
