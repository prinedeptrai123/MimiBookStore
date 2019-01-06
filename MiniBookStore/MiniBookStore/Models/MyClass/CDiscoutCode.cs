using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models.MyClass
{
    class CDiscoutCode
    {
        #region private propeties

        private string _codeID;
        private string _name;
        private string _type;
        private DateTime _dateBegin;
        private DateTime _dateEnd;

        #endregion

        #region public properties

        /// <summary>
        /// Mã code
        /// </summary>
        public string CodeID { get => _codeID; set { if (value == _codeID) return;_codeID = value; } }
        /// <summary>
        /// Tên mã
        /// </summary>
        public string Name { get => _name; set { if (value == _name) return; _name = value; } }
        /// <summary>
        /// Loại mã
        /// </summary>
        public string Type { get => _type; set { if (value == _type) return; _type = value; } }
        /// <summary>
        /// Ngày bắt đầu áp dụng
        /// </summary>
        public DateTime DateBegin { get => _dateBegin; set { if (value == _dateBegin) return; _dateBegin = value; } }
        /// <summary>
        /// Ngày hết hạn
        /// </summary>
        public DateTime DateEnd { get => _dateEnd; set { if (value == _dateEnd) return; _dateEnd = value; } }

        #endregion
    }
}
