using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models.MyClass
{
    class CCustomer:Human
    {
        #region private properties

        private int _totalBook;
        private int _totalMoney;
        private DateTime _lastTransaction;

        #endregion

        #region public properties
        /// <summary>
        /// Tổng sách đã mua
        /// </summary>
        public int TotalBook { get => _totalBook; set { if (value == _totalBook) return;_totalBook = value; } }
        /// <summary>
        /// Tổng tiền đã trả cho cửa hàng
        /// </summary>
        public int TotalMoney { get => _totalMoney; set { if (value == _totalMoney) return; _totalMoney = value; } }
        /// <summary>
        /// Ngày mua hàng cuối cùng
        /// </summary>
        public DateTime LastTransaction { get => _lastTransaction; set { if (value == _lastTransaction) return; _lastTransaction = value; } }

        #endregion

        #region method

        #endregion

    }
}
