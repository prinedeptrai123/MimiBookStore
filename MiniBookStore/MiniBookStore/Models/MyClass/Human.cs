using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models.MyClass
{
    public class Human
    {
        #region private properties

        private int _iD;//id
        private string _name;//tên
        private string _address;//địa chỉ
        private string _email;//email
        private string _phone;//sdt
        private string _gender;//giới tính

        #endregion


        #region public properties

        /// <summary>
        /// Id
        /// </summary>
        public int ID { get => _iD; set { if (value == _iD) return; _iD = value; } }
        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get => _name; set { if (value == _name) return; _name = value; } }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get => _address; set { if (value == _address) return; _address = value; } }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get => _email; set { if (value == _email) return; _email = value; } }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get => _phone; set { if (value == _phone) return; _phone = value; } }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string Gender { get => _gender; set { if (value == _gender) return; _gender = value; } }

        #endregion

        #region method

        #endregion

    }
}
