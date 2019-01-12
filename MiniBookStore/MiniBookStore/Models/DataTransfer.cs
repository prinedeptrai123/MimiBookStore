using MiniBookStore.Models.MyClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models
{
    public static class DataTransfer
    {
        /// <summary>
        /// Biến truyền thông tin nhân viên
        /// </summary>
        public static CEmployee EmployeeInfo;

        public static ObservableCollection<CBookBill> ListBookBill;

        public static int NumberProductInBill;

        static DataTransfer()
        {
            ListBookBill = new ObservableCollection<CBookBill>();
        }
    }
}
