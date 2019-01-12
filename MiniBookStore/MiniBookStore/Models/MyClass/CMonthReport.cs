using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models.MyClass
{
    
    public class CMonthReport
    {
        #region design pattern singleton

        private static CMonthReport _ins;
        public static CMonthReport Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new CMonthReport();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }

        #endregion

        #region private properties

        private int _month;
        private int _bookInCount;
        private float _bookInPrice;
        private int _bookOutCount;
        private float _bookOutPrice;
        private float _profit;
        private float _salary;

        #endregion

        #region public properties

        /// <summary>
        /// Tháng cần thống kê
        /// </summary>
        public int Month { get => _month; set { if (value == Month) return;_month = value; } }
        /// <summary>
        /// Tổng sách nhập trong tháng
        /// </summary>
        public int BookInCount { get => _bookInCount; set { if (value == _bookInCount) return; _bookInCount = value; } }
        /// <summary>
        /// Tổng tiền nhập sách trong tháng
        /// </summary>
        public float BookInPrice { get => _bookInPrice; set { if (value == _bookInPrice) return; _bookInPrice = value; } }
        /// <summary>
        /// Tổng sách bán ra trong tháng
        /// </summary>
        public int BookOutCount { get => _bookOutCount; set { if (value == _bookOutCount) return; _bookOutCount = value; } }
        /// <summary>
        /// Tổng tiền bán sách trong tháng
        /// </summary>
        public float BookOutPrice { get => _bookOutPrice; set { if (value == _bookOutPrice) return; _bookOutPrice = value; } }
        /// <summary>
        /// Lợi nhuận trong tháng
        /// </summary>
        public float Profit { get => _profit; set { if (value == _profit) return; _profit = value; } }
        /// <summary>
        /// Tiền lương thanh toán cho nhân viên trong tháng
        /// </summary>
        public float Salary { get => _salary; set { if (value == _salary) return; _salary = value; } }

        #endregion

        #region method

        /// <summary>
        /// Hàm trả về lợi nhuận của các tháng trong năm
        /// </summary>
        /// <param name="Year"></param>
        /// <returns></returns>
        public List<CMonthReport> MonthlyReport(int Year)
        {
            List<CMonthReport> List = new List<CMonthReport>();
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    for (int Month = 1; Month <= 12; Month++)
                    {
                        //Lấy ra tổng số sách nhập trong tháng
                        int CountBookInput = DB.Warehouse_Detail.Where(x => SqlFunctions.DatePart("year", x.Warehouse.Warehouse_Date) == Year &&
                        SqlFunctions.DatePart("month", x.Warehouse.Warehouse_Date) == Month).Select(x => x.Book_Count).DefaultIfEmpty(0).Sum();

                        //Lấy ra tổng số tiền nhập sách trong tháng
                        float TotalMoneyInput = (float)DB.Warehouse_Detail.Where(x => SqlFunctions.DatePart("year", x.Warehouse.Warehouse_Date) == Year &&
                        SqlFunctions.DatePart("month", x.Warehouse.Warehouse_Date) == Month).Select(x => x.Book_Count * x.Book_Price).DefaultIfEmpty(0).Sum();

                        //Lấy ra tổng tiền bán sách trong tháng
                        float TotalMoney = (float)DB.Bills.Where(x => SqlFunctions.DatePart("year", x.Bill_Date) == Year &&
                        SqlFunctions.DatePart("month", x.Bill_Date) == Month).Select(x => x.Total_Money).DefaultIfEmpty(0).Sum();

                        //Lấy ra tổng số sách bán được trong tháng
                        int CountBook = DB.Bill_Detail.Where(x => SqlFunctions.DatePart("year", x.Bill.Bill_Date) == Year &&
                        SqlFunctions.DatePart("month", x.Bill.Bill_Date) == Month).Select(x => x.Book_Count).DefaultIfEmpty(0).Sum();

                        //Lấy ra tổng tiền lương trả cho nhân viên trong tháng
                        float SumSalary = (float)DB.Pay_Wage.Where(x => SqlFunctions.DatePart("year", x.PayWage_Date) == Year &&
                          SqlFunctions.DatePart("month", x.PayWage_Date) == Month).Select(x => x.Salary).DefaultIfEmpty(0).Sum();

                        //Tính toán lợi nhuận bằng tiền bán sách trừ cho tiền nhập sách với tiền lương trả cho nhân viên trong tháng đó
                        float Profit = TotalMoney - TotalMoneyInput - SumSalary;

                        //Tạo mới CReport
                        CMonthReport Report = new CMonthReport
                        {
                            Month = Month,
                            BookInCount = CountBookInput,
                            BookInPrice = TotalMoneyInput,
                            BookOutCount = CountBook,
                            BookOutPrice = TotalMoney,
                            Salary = SumSalary,
                            Profit = Profit
                        };

                        //Thêm vào List
                        List.Add(Report);
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
