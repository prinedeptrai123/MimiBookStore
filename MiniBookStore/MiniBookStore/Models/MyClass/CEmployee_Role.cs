using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models.MyClass
{
    public class CEmployee_Role
    {
        #region private properties

        private int _iD;
        private string _name;
        private string _decentralization;
        private float _salary;
        private int _count;

        #endregion

        #region public properties
        /// <summary>
        /// Id loại nhân viên
        /// </summary>
        public int ID { get => _iD;set { if (value == _iD) return;_iD = value; } }
        /// <summary>
        /// Tên loại nhân viên
        /// </summary>
        public string Name { get => _name; set { if (value == _name) return; _name = value; } }
        /// <summary>
        /// Quyền hạn của nhân viên
        /// </summary>
        public string Decentralization { get => _decentralization; set { if (value == _decentralization) return; _decentralization = value; } }
        /// <summary>
        /// Lương của loại nhân viên
        /// </summary>
        public float Salary { get => _salary; set { if (value == _salary) return; _salary = value; } }
        /// <summary>
        /// Số lượng loại nhân viên này
        /// </summary>
        public int Count { get => _count; set { if (value == _count) return; _count = value; } }

        #endregion

        #region method

        #endregion
    }
}
