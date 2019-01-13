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
                            Address =find.Employee_Address,
                            Email=find.Employee_Email,
                            Gender=find.Employee_Gender,
                            Phone=find.Employee_Phone,
                            DOB = find.Employee_DOB,
                            Role = new CEmployee_Role
                            {
                                Name = find.Employee_Role1.Role_Name,
                                Salary = (float)find.Employee_Role1.Role_Salary,
                                ID = find.Employee_Role1.Role_ID,
                                Decentralization = find.Employee_Role1.Decentralization.Decentralization_Name
                            },
                            Image = find.Employee_Image == null ? image : Help.ByteToImage(find.Employee_Image)
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

        #endregion
    }
}
