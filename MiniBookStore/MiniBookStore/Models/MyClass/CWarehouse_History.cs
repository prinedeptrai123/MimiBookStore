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

        private DateTime _date;
        private int _totalCount;
        private float _toltalMoney;

        #endregion

        #region public propeties

        public DateTime Date { get => _date; set { if (value == _date) return;_date = value; } }
        public int TotalCount { get => _totalCount; set { if (value == _totalCount) return; _totalCount = value; } }
        public float ToltalMoney { get => _toltalMoney; set { if (value == _toltalMoney) return; _toltalMoney = value; } }

        #endregion
    }
}
