using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MiniBookStore.Models.MyClass
{
    public class CEmployee:Human
    {
        #region private properties

        private string _identity;
        private DateTime _dOB;
        private DateTime _firstDate;
        private int _sumDate;
        private int _dateWork;
        private BitmapImage _image;
        private CEmployee_Role _role;

        #endregion

        #region public properties
        /// <summary>
        /// Số chứng minh nhân dân của nhân viên
        /// </summary>
        public string Identity { get => _identity; set { if (value == _identity) return; _identity = value; } }
        /// <summary>
        /// Ngày sinh của nhân viên
        /// </summary>
        public DateTime DOB { get => _dOB; set { if (value == _dOB) return; _dOB = value; } }
        /// <summary>
        /// Ngày bắt đầu làm việc của nhân viên
        /// </summary>
        public DateTime FirstDate { get => _firstDate; set { if (value == _firstDate) return; _firstDate = value; } }
        /// <summary>
        /// Tổng ngày làm việc của nhân viên
        /// </summary>
        public int SumDate { get => _sumDate; set { if (value == _sumDate) return; _sumDate = value; } }
        /// <summary>
        /// Tổng ngày làm việc trong tháng này của nhân viên
        /// </summary>
        public int DateWork { get => _dateWork; set { if (value == _dateWork) return; _dateWork = value; } }
        /// <summary>
        /// Ảnh đại diện của nhân viên
        /// </summary>
        public BitmapImage Image { get => _image; set { if (value == _image) return; _image = value; } }
        /// <summary>
        /// Chức vụ của nhân viên
        /// </summary>
        public CEmployee_Role Role { get => _role; set { if (value == _role) return; _role = value; } }

        #endregion

        #region method

        

        #endregion
    }
}
