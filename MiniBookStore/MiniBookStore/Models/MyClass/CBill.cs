using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models.MyClass
{
    public class CBill
    {
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
    }
}
