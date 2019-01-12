using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models.MyClass
{
    public class CDateReport
    {
        #region design pattern singleton

        private static CDateReport _ins;
        public static CDateReport Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new CDateReport();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }

        #endregion
        #region private properties

        private DateTime _date;
        private int _bookCount;
        private float _bookInPrice;
        private float _sumMoney;
        private float _profit;

        #endregion

        #region public properties

        /// <summary>
        /// Ngày cần thống kê
        /// </summary>
        public DateTime Date { get => _date; set { if (value == _date) return;_date = value; } }
        /// <summary>
        /// Tổng sách bán ra trong ngày
        /// </summary>
        public int BookCount { get => _bookCount; set { if (value == _bookCount) return; _bookCount = value; } }
        /// <summary>
        /// Tổng tiền
        /// </summary>
        public float SumMoney { get => _sumMoney; set { if (value == _sumMoney) return; _sumMoney = value; } }
        /// <summary>
        /// Lợi nhuận
        /// </summary>
        public float Profit { get => _profit; set { if (value == _profit) return; _profit = value; } }
        /// <summary>
        /// Tiền nhập sách tương ứng
        /// </summary>
        public float BookInPrice { get => _bookInPrice; set { if (value == _bookInPrice) return; _bookInPrice = value; } }

        #endregion

        #region method


        /// <summary>
        /// Hàm trả về Thông tin
        /// </summary>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public List<CDateReport> MonthlyReport(int Month,int Year) 
        {
            List<CDateReport> List = new List<CDateReport>();
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    //Lấy ra list date trong danh sách hóa đơn (lấy ra những ngày khác nhau không lấy ra giờ vì trong một ngày có thể có nhiều hóa đơn)
                    //Lấy ra những tháng và năm như điều kiện nhập vào
                    //https://stackoverflow.com/questions/30588033/get-date-part-only-from-datetime-value-using-entity-framework

                    var ListDate = DB.Bills.Where(x => SqlFunctions.DatePart("year", x.Bill_Date) == Year
                    && SqlFunctions.DatePart("month", x.Bill_Date) == Month).Select(x => EntityFunctions.TruncateTime(x.Bill_Date)).Distinct();

                    if (ListDate.Count() > 0)
                    {
                        foreach (var date in ListDate)
                        {
                            //Tạo mới 1 Report
                            CDateReport Report = new CDateReport();

                            //Lấy ra danh sách thông tin hóa đơn theo ngày (như là groupby)
                            var dataBillInfo = DB.Bill_Detail.Where(x => EntityFunctions.TruncateTime(x.Bill.Bill_Date) == EntityFunctions.TruncateTime(date));

                            //Lấy ra tổng số sách bán trong ngày
                            Report.BookCount = dataBillInfo.Sum(x => x.Book_Count);

                            //Lấy ra tổng tiền thu được trong ngày
                            float ToltalMoney = (float)DB.Bills.Where(x => EntityFunctions.TruncateTime(x.Bill_Date) == EntityFunctions.TruncateTime(date)).Sum(x => x.Total_Money);
                            Report.SumMoney = ToltalMoney;

                            Report.Date = (DateTime)date;

                            float SumInMonney = 0;

                            foreach(var item in dataBillInfo)
                            {
                                var find = DB.Warehouse_Detail.Where(x => x.Book_ID == item.Book_ID && x.Warehouse_ID == item.Warehouse_ID).FirstOrDefault();
                                if (find != null)
                                {
                                    SumInMonney = SumInMonney + (float)find.Book_Price * item.Book_Count;
                                }
                            }

                            Report.BookInPrice = SumInMonney;

                            //Lợi nhuận
                            Report.Profit = ToltalMoney - SumInMonney;

                            List.Add(Report);
                        }
                    }
                }
            }
            catch
            {

            }
            return List;
        }

        public List<CDateReport> MonthlyReport(int Month, int Year,DateTime MinDate,DateTime MaxDate)
        {
            List<CDateReport> List = new List<CDateReport>();
            try
            {
                using (var DB = new BookStoreDataEntities())
                {
                    //Lấy ra list date trong danh sách hóa đơn (lấy ra những ngày khác nhau không lấy ra giờ vì trong một ngày có thể có nhiều hóa đơn)
                    //Lấy ra những tháng và năm như điều kiện nhập vào
                    //https://stackoverflow.com/questions/30588033/get-date-part-only-from-datetime-value-using-entity-framework

                    var ListDate = DB.Bills.Where(x => SqlFunctions.DatePart("year", x.Bill_Date) == Year
                    && SqlFunctions.DatePart("month", x.Bill_Date) == Month &&
                    EntityFunctions.TruncateTime(x.Bill_Date) >= EntityFunctions.TruncateTime(MinDate)
                    && EntityFunctions.TruncateTime(x.Bill_Date) <= EntityFunctions.TruncateTime(MaxDate)).Select(x => EntityFunctions.TruncateTime(x.Bill_Date)).Distinct();

                    if (ListDate.Count() > 0)
                    {
                        foreach (var date in ListDate)
                        {
                            //Tạo mới 1 Report
                            CDateReport Report = new CDateReport();

                            //Lấy ra danh sách thông tin hóa đơn theo ngày (như là groupby)
                            var dataBillInfo = DB.Bill_Detail.Where(x => EntityFunctions.TruncateTime(x.Bill.Bill_Date) == EntityFunctions.TruncateTime(date));

                            //Lấy ra tổng số sách bán trong ngày
                            Report.BookCount = dataBillInfo.Sum(x => x.Book_Count);

                            //Lấy ra tổng tiền thu được trong ngày
                            float ToltalMoney = (float)DB.Bills.Where(x => EntityFunctions.TruncateTime(x.Bill_Date) == EntityFunctions.TruncateTime(date)).Sum(x => x.Total_Money);
                            Report.SumMoney = ToltalMoney;

                            Report.Date = (DateTime)date;

                            float SumInMonney = 0;

                            foreach (var item in dataBillInfo)
                            {
                                var find = DB.Warehouse_Detail.Where(x => x.Book_ID == item.Book_ID && x.Warehouse_ID == item.Warehouse_ID).FirstOrDefault();
                                if (find != null)
                                {
                                    SumInMonney = SumInMonney + (float)find.Book_Price * item.Book_Count;
                                }
                            }

                            Report.BookInPrice = SumInMonney;

                            //Lợi nhuận
                            Report.Profit = ToltalMoney - SumInMonney;

                            List.Add(Report);
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
