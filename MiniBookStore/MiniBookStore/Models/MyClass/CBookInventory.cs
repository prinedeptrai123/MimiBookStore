using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models.MyClass
{
    /// <summary>
    /// Lưu thông tin của sách nhập theo đợt
    /// </summary>
    class CBookInventory:CBook
    {
        #region private properties

        private int _warehouseID;      
        private int _warehouseInventory;
        private float _inPrice;

        #endregion

        #region public properties
        /// <summary>
        /// Mã đợt nhập kho
        /// </summary>
        public int WarehouseID { get => _warehouseID; set { if (value == _warehouseID) return;_warehouseID = value; } }
        /// <summary>
        /// Số lượng sách tồn kho của đợt nhập này
        /// </summary>
        public int WarehouseInventory { get => _warehouseInventory; set { if (value == _warehouseInventory) return; _warehouseInventory = value; } }
        /// <summary>
        /// Giá nhập sách của đợt nhập này
        /// </summary>
        public float InPrice { get => _inPrice; set { if (value == _inPrice) return; _inPrice = value; } }
   

        #endregion

        #region method

        #endregion
    }
}
