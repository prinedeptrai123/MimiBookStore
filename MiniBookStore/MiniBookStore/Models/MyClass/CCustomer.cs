using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models.MyClass
{
    public class CCustomer:Human
    {
        private static CCustomer _ins;
        public static CCustomer Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new CCustomer();
                return _ins;
            }
            set
            {
                _ins = value;
            }
        }

        #region private properties

        private int _totalBook;
        private int _totalMoney;
        private DateTime _lastTransaction;

        #endregion

        #region public properties
        /// <summary>
        /// Tổng sách đã mua
        /// </summary>
        public int TotalBook { get => _totalBook; set { if (value == _totalBook) return;_totalBook = value; } }
        /// <summary>
        /// Tổng tiền đã trả cho cửa hàng
        /// </summary>
        public int TotalMoney { get => _totalMoney; set { if (value == _totalMoney) return; _totalMoney = value; } }
        /// <summary>
        /// Ngày mua hàng cuối cùng
        /// </summary>
        public DateTime LastTransaction { get => _lastTransaction; set { if (value == _lastTransaction) return; _lastTransaction = value; } }

        #endregion

        #region method

        /// <summary>
        /// Hàm trả về ID khách hàng theo sdt
        /// </summary>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public int isCustomer(string Phone)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var find = DB.Customers.Where(x => x.Customer_Phone == Phone).FirstOrDefault();
                    if (find != null)
                    {
                        return find.Customer_ID;
                    }
                }
            }
            catch
            {

            }
            return 0;
        }

        /// <summary>
        /// Hàm thêm mới một khách hàng, nếu thêm thành công thì trả về ID ngược lại trả về 0
        /// </summary>
        /// <param name="myCustomer"></param>
        /// <returns></returns>
        public int addCustomer(CCustomer myCustomer)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    //Tạo mới
                    Customer newCustomer = new Customer
                    {
                        Customer_Name = myCustomer.Name,
                        Customer_Phone = myCustomer.Phone,
                        Customer_Email = myCustomer.Email,
                        Customer_Address = myCustomer.Address,
                        Customer_Gender = myCustomer.Gender,
                        Exist = true
                    };

                    //Thêm vào
                    DB.Customers.Add(newCustomer);

                    //Lưu thay đổi
                    DB.SaveChanges();

                    //trả về ID của customer vừa tạo
                    int ID = isCustomer(myCustomer.Phone);
                    if (ID != 0)
                        return ID;
                }
            }
            catch
            {

            }
            return 0;
        }

        /// <summary>
        /// Hàm trả về danh sách khách hàng
        /// </summary>
        /// <returns></returns>
        public List<CCustomer> ListCustomerFilterPhone(string Phone)
        {
            List<CCustomer> List = new List<CCustomer>();
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var data = DB.Customers.ToList();
                    if(string.IsNullOrEmpty(Phone) || Phone == "")
                    {
                        //do nothing
                    }
                    else
                    {
                        data = data.Where(x => x.Customer_Phone.Contains(Phone)).ToList();
                    }

                    foreach(var item in data)
                    {
                        //Tạo mới một khách hàng
                        CCustomer myCustomer = new CCustomer
                        {
                            ID = item.Customer_ID,
                            Name = item.Customer_Name,
                            Address = item.Customer_Address,
                            Phone = item.Customer_Phone,
                            Email = item.Customer_Email,
                            Gender = item.Customer_Gender
                        };

                        List.Add(myCustomer);
                    }
                }
            }
            catch
            {

            }
            return List;
        }

        /// <summary>
        /// Hàm lọc khách hàng theo tên
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public List<CCustomer> ListCustomerFilterName(string Name)
        {
            List<CCustomer> List = new List<CCustomer>();
            try
            {
                using (var DB = new BookStoreDataEntities())
                {
                    var data = DB.Customers.ToList();
                    if (string.IsNullOrEmpty(Name) || Name == "")
                    {
                        //do nothing
                    }
                    else
                    {
                        data = data.Where(x => x.Customer_Name.ToLower().Contains(Name.ToLower())).ToList();
                    }

                    foreach (var item in data)
                    {
                        //Tạo mới một khách hàng
                        CCustomer myCustomer = new CCustomer
                        {
                            ID = item.Customer_ID,
                            Name = item.Customer_Name,
                            Address = item.Customer_Address,
                            Phone = item.Customer_Phone,
                            Email = item.Customer_Email,
                            Gender = item.Customer_Gender
                        };

                        List.Add(myCustomer);
                    }
                }
            }
            catch
            {

            }
            return List;
        }
        #endregion

    }
}
