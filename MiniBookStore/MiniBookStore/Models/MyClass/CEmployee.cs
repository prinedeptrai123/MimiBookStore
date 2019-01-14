using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MiniBookStore.Models.MyClass
{
    public class CEmployee:Human
    {
        #region design pattern singleton

        private static CEmployee _ins;
        public static CEmployee Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new CEmployee();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }

        #endregion

        #region private properties

        private string _identity;
        private DateTime _dOB;
        private DateTime _firstDate;
        private int _sumDate;
        private int _dateWork;
        private BitmapImage _image;
        private CEmployee_Role _role;
        private float _monthSalary;
        private CAccount _account;

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

        /// <summary>
        /// Lương tháng này của nhân viên
        /// </summary>
        public float MonthSalary { get => _monthSalary; set { if (value == _monthSalary) return; _monthSalary = value; } }

        /// <summary>
        /// Thông tin tài khoản của nhân viên
        /// </summary>
        public CAccount Account { get => _account; set { if (value == _account) return; _account = value; } }

        #endregion

        #region method

        /// <summary>
        /// Hàm trả về thông tin nhân viên theo ID của nhân viên
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public CEmployee EmployeeInFo(int ID)
        {
            try
            {
                using (var DB = new BookStoreDataEntities())
                {
                    var find = DB.Employees.Find(ID);
                    if (find != null)
                    {
                        BitmapImage image = new BitmapImage(new Uri("pack://application:,,,/" + "./Images/Avatar.png"));

                        //Tạo mới một employee
                        CEmployee myEmployee = new CEmployee
                        {
                            ID = find.Employee_ID,
                            Name = find.Employee_Name,
                            Identity = find.Employee_Identity,
                            DateWork = find.Employee_Date_Work,
                            SumDate = find.Employee_Sum_Date,
                            FirstDate = find.Employee_FirstDate,
                            Address = find.Employee_Address,
                            Email = find.Employee_Email,
                            Gender = find.Employee_Gender,
                            Phone = find.Employee_Phone,
                            DOB = find.Employee_DOB,
                            Role = new CEmployee_Role
                            {
                                Name = find.Employee_Role1.Role_Name,
                                Salary = (float)find.Employee_Role1.Role_Salary,
                                ID = find.Employee_Role1.Role_ID,
                                Decentralization = find.Employee_Role1.Decentralization.Decentralization_Name
                            },
                            Image = find.Employee_Image == null ? image : Help.ByteToImage(find.Employee_Image),
                            MonthSalary = (float)find.Employee_Role1.Role_Salary * find.Employee_Date_Work,
                            Account = new CAccount { PassWord = find.Employee_Account.Select(x => x.Account_Password).FirstOrDefault() }
                        };

                        return myEmployee;
                    }
                }
            }
            catch
            {

            }
            return null;
        }

        /// <summary>
        /// Hàm tăng số ngày làm việc (điểm danh) của nhân viên khi đăng nhập 1, chỉ tính lần đăng nhập đầu tiên trong ngày
        /// </summary>
        /// <param name="Employee_Id">Id nhân viên</param>
        /// <returns></returns>
        public bool CheckIn(int Employee_Id)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var find = DB.Employee_Account.Where(x => x.Employee_ID == Employee_Id).First();
                    if (find != null)
                    {
                        //Kiểm tra đã từng đăng nhập chưa
                        var LastLogin = find.Account_LastLogin;

                        if (LastLogin != null)
                        {
                            //Kiểm tra ngày đăng nhập cuối cùng có trùng với ngày hôm này hay không

                            if (LastLogin.Year == DateTime.Now.Year && LastLogin.Month == DateTime.Now.Month && LastLogin.Day == DateTime.Now.Day)
                            {
                                return false;
                            }
                            else
                            {
                                //Tạo mới một đăng nhập
                                find.Account_LastLogin = DateTime.Now;

                                //Lưu lại thay đổi
                                DB.SaveChanges();

                                IncreateDate(Employee_Id);
                                return true;

                            }
                        }
                        else
                        {
                            //Tạo mới một đăng nhập
                            find.Account_LastLogin = DateTime.Now;

                            //Lưu lại thay đổi
                            DB.SaveChanges();

                            IncreateDate(Employee_Id);
                            return true;
                        }
                    }
                }
                
            }
            catch
            {
                
            }

            return false;
        }

        /// <summary>
        /// Hàm tăng số ngày làm việc
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public bool IncreateDate(int EmployeeID)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    //Lấy ra thông tin bên bảng employee_info
                    var FindInfo = DB.Employees.Where(x => x.Employee_ID == EmployeeID).FirstOrDefault();
                    if (FindInfo != null)
                    {
                        //Thêm ngày làm việc vào
                        FindInfo.Employee_Sum_Date = FindInfo.Employee_Sum_Date + 1;
                        FindInfo.Employee_Date_Work = FindInfo.Employee_Date_Work + 1;

                        //Lưu lại thay đổi
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
