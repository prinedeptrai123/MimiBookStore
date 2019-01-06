using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models.MyClass
{
    class CBookBill:CBookInventory
    {
        #region private properties

        private int _count;
        private float _totalMoney;

        #endregion

        #region public properties

        /// <summary>
        /// Tổng số lượng sách mua trong đơn hàng này
        /// </summary>
        public int Count { get => _count; set { if (value == _count) return;_count = value; } }
        /// <summary>
        /// Tổng tiền tương ứng nhân với số lượng sách
        /// </summary>
        public float TotalMoney { get => _totalMoney; set { if (value == _totalMoney) return; _totalMoney = value; } }

        #endregion

        #region method

        #endregion
    }
}
