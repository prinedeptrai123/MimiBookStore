using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models.MyClass
{
    public class CWarehouse_History
    {
        #region private propeties

        private int _id;
        private DateTime _date;
        private int _totalCount;
        private float _toltalMoney;
        private float _inPrice;
        private string _type;

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


        #endregion
    }
}
