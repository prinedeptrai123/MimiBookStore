using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models.MyClass
{
    public class CPromotion
    {
        #region private properties

        private int _iD;
        private string _name;
        private int _bookCount;
        private float _promotion;

        #endregion

        #region public properties
        /// <summary>
        /// Mã loại
        /// </summary>
        public int ID { get => _iD; set { if (value == _iD) return;_iD = value; } }
        /// <summary>
        /// Tên loại khuyến mãi: combo, 
        /// </summary>
        public string Name { get => _name; set { if (value == _name) return; _name = value; } }
        /// <summary>
        /// Số lượng sách cần để áp dụng nếu như là loại mã combo
        /// </summary>
        public int BookCount { get => _bookCount; set { if (value == _bookCount) return; _bookCount = value; } }
        /// <summary>
        /// % khuyến mãi
        /// </summary>
        public float Promotion { get => _promotion; set { if (value == _promotion) return; _promotion = value; } }

        #endregion
    }
}
