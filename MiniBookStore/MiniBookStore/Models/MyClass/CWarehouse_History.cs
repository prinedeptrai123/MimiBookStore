using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models.MyClass
{
    public class CWarehouse_History
    {
        #region design pattern singleton

        private static CWarehouse_History _ins;
        public static CWarehouse_History Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new CWarehouse_History();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }

        #endregion

        #region private propeties

        private int _id;
        private DateTime _date;
        private int _totalCount;
        private float _toltalMoney;
        private float _inPrice;
        private string _type;
        private CEmployee _employeeInfo;

        #endregion

        #region public propeties

        /// <summary>
        /// Mã nhập
        /// </summary>
        public int ID { get => _id; set { if (value == _id) return; _id = value; } }
        /// <summary>
        /// Ngày nhập
        /// </summary>
        public DateTime Date { get => _date; set { if (value == _date) return;_date = value; } }
        /// <summary>
        /// Tổng sách trong đợt nhập
        /// </summary>
        public int TotalCount { get => _totalCount; set { if (value == _totalCount) return; _totalCount = value; } }
        /// <summary>
        /// Tổng tiền trong đợt nhập
        /// </summary>
        public float ToltalMoney { get => _toltalMoney; set { if (value == _toltalMoney) return; _toltalMoney = value; } }

        /// <summary>
        /// Giá sách nhập trong đợt đó
        /// </summary>
        public float InPrice { get => _inPrice; set { if (value == _inPrice) return; _inPrice = value; } }

        /// <summary>
        /// Loại nhập nhập mới, nhập thêm
        /// </summary>
        public string Type { get => _type; set { if (value == _type) return; _type = value; } }

        public CEmployee EmployeeInfo { get => _employeeInfo; set { if (value == _employeeInfo) return;_employeeInfo = value; } }


        #endregion

        #region method

        /// <summary>
        /// Hàm trả về lịch sử nhập kho
        /// </summary>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public List<CWarehouse_History> Warehouse_History(int Month,int Year, int currentPage, int NumberPage)
        {
            List<CWarehouse_History> List = new List<CWarehouse_History>();
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var data = DB.Warehouses.Where(x => SqlFunctions.DatePart("year",
                         x.Warehouse_Date) == Year && SqlFunctions.DatePart("month", x.Warehouse_Date) == Month).OrderByDescending(x => x.Warehouse_Date).ToList().
                        Skip((currentPage - 1) * NumberPage).Take(NumberPage);

                    if (data.Count() > 0)
                    {
                        foreach(var item in data)
                        {
                            //Tạo mới
                            CWarehouse_History History = new CWarehouse_History
                            {
                                ID = item.Warehouse_ID,
                                ToltalMoney = (float)item.Warehouse_Toltal_Money,
                                Date = item.Warehouse_Date,
                                Type = item.Warehouse_Type == false ? "Nhập mới" : "Nhập thêm",
                                TotalCount = item.Warehouse_Detail.Sum(x => x.Book_Count),
                                EmployeeInfo = new CEmployee { ID = item.Employee_ID, Name = item.Employee.Employee_Name }
                            };

                            //Thêm vào
                            List.Add(History);
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
        /// Hàm trả về danh sách nhập kho trong từ ngày đến ngày
        /// </summary>
        /// <param name="DateBegin"></param>
        /// <param name="DateEnd"></param>
        /// <param name="currentPage"></param>
        /// <param name="NumberPage"></param>
        /// <returns></returns>
        public List<CWarehouse_History> Warehouse_History(DateTime DateBegin, DateTime DateEnd, int currentPage, int NumberPage)
        {
            List<CWarehouse_History> List = new List<CWarehouse_History>();
            try
            {
                using (var DB = new BookStoreDataEntities())
                {
                    var data = DB.Warehouses.Where(x => EntityFunctions.TruncateTime(x.Warehouse_Date) >= EntityFunctions.TruncateTime(DateBegin)
                    && EntityFunctions.TruncateTime(x.Warehouse_Date) <= EntityFunctions.TruncateTime(DateEnd)).OrderByDescending(x => x.Warehouse_Date).ToList().
                        Skip((currentPage - 1) * NumberPage).Take(NumberPage);

                    if (data.Count() > 0)
                    {
                        foreach (var item in data)
                        {
                            //Tạo mới
                            CWarehouse_History History = new CWarehouse_History
                            {
                                ID = item.Warehouse_ID,
                                ToltalMoney = (float)item.Warehouse_Toltal_Money,
                                Date = item.Warehouse_Date,
                                Type = item.Warehouse_Type == false ? "Nhập mới" : "Nhập thêm",
                                TotalCount = item.Warehouse_Detail.Sum(x => x.Book_Count),
                                EmployeeInfo = new CEmployee { ID = item.Employee_ID, Name = item.Employee.Employee_Name }
                            };

                            //Thêm vào
                            List.Add(History);
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
