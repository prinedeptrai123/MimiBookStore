using MiniBookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models.MyClass
{
    public class CPromotion_Code:BaseViewModel
    {
        #region private properties

        private string _iD;
        private string _name;
        private string _type;
        private DateTime _dateBegin;
        private DateTime _dateEnd;
        private bool _isExist;
        private bool _istrueValue;

        #endregion

        #region public properties

        public string ID { get => _iD; set { if (value == _iD) return;_iD = value; } }
        public string Name { get => _name; set { if (value == _name) return; _name = value; } }
        public string Type { get => _type; set { if (value == _type) return; _type = value; } }
        public DateTime DateBegin { get => _dateBegin; set { if (value == _dateBegin) return; _dateBegin = value; } }
        public DateTime DateEnd { get => _dateEnd; set { if (value == _dateEnd) return; _dateEnd = value; } }
        public bool IsExist { get => _isExist; set { if (value == _isExist) return; _isExist = value; } }
        public bool IstrueValue { get => _istrueValue; set { if (value == _istrueValue) return; _istrueValue = value; OnPropertyChanged(); } }

        #endregion
    }
}
