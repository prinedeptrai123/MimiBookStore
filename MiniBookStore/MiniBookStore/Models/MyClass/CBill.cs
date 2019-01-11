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
                                Applied = item.Discount_Code.Count
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



        #endregion
    }
}
