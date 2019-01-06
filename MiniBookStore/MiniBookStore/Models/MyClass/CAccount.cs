using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models.MyClass
{
    class CAccount
    {
        #region private properties

        private int _iD;
        private CEmployee _employeeInfo;
        private string _userName;
        private string _passWord;

        #endregion

        #region public properties

        /// <summary>
        /// Mã tài khoản
        /// </summary>
        public int iD { get => _iD; set { if (value == _iD) return;_iD = value; } }
        /// <summary>
        /// Thông tin nhân viên sở hữu tài khoản
        /// </summary>
        public CEmployee EmployeeInfo { get => _employeeInfo; set { if (value == _employeeInfo) return; _employeeInfo = value; } }
        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        public string UserName { get => _userName; set { if (value == _userName) return; _userName = value; } }
        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string PassWord { get => _passWord; set { if (value == _passWord) return; _passWord = value; } }

        #endregion
    }
}
