using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models.MyClass
{
    public class CAccount
    {
        #region design pattern singleton

        private static CAccount _ins;
        public static CAccount Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new CAccount();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }

        #endregion

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

        #region method
        
        /// <summary>
        /// Hàm trả về id nhân viên ương ứng với account nếu không có thì trả về 0
        /// </summary>
        /// <param name="myAccount"></param>
        /// <returns></returns>
        public int  isAccount(CAccount myAccount)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var find = DB.Employee_Account.Where(x => x.Account_User == myAccount.UserName && x.Account_Password == myAccount.PassWord).FirstOrDefault();
                    if (find != null)
                    {
                        return find.Employee_ID;
                    }
                }
            }
            catch
            {

            }
            return 0;
        }

        /// <summary>
        /// Hàm thay đổi mật khẩu của nhân viên
        /// </summary>
        /// <param name="Employee_Id"></param>
        /// <param name="newPass"></param>
        /// <returns></returns>
        public bool ChangePassword(int Employee_Id, string newPass)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var find = DB.Employee_Account.Where(x => x.Employee_ID == Employee_Id).First();
                    if (find != null)
                    {
                        //Thay đổi
                        find.Account_Password = Help.Base64Encode(newPass);

                        //Lưu lại
                        DB.SaveChanges();

                        return true;
                    }
                }
               
            }
            catch
            {

            }
            return false;
        }

        #endregion
    }
}
