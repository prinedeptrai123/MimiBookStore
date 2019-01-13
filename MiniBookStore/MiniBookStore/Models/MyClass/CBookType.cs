using MiniBookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models.MyClass
{
    public class CBookType:BaseViewModel
    {
        #region private properties

        private int _iD;
        private string _name;
        private int _count;
        private bool _isExist;
        private bool _isTrueValue;

        #endregion

        #region public proerties

        public int ID { get => _iD; set { if (value == _iD) return;_iD = value; } }
        public string Name { get => _name; set { if (value == _name) return; _name = value; } }
        public int Count { get => _count; set { if (value == _count) return; _count = value; } }
        public bool IsExist { get => _isExist; set { if (value == _isExist) return; _isExist = value; } }
        public bool IsTrueValue { get => _isTrueValue; set { if (value == _isTrueValue) return;_isTrueValue = value;OnPropertyChanged(); } }

        #endregion
    }
}
