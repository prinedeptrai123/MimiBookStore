using MiniBookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBookStore.Models.MyClass
{
    public class CPromotion_Type:BaseViewModel
    {
        #region private propeties

        private int _iD;
        private string _name;
        private int _bookCount;
        private float _promotion;
        private int _applied;
        private bool _isExist;
        private bool _isTrueValue;

        #endregion

        #region public propeties

        public int ID { get => _iD; set { if (value == _iD) return;_iD = value; } }
        public string Name { get => _name; set { if (value == _name) return; _name = value; } }
        public int BookCount { get => _bookCount; set { if (value == _bookCount) return; _bookCount = value; } }
        public float Promotion { get => _promotion; set { if (value == _promotion) return; _promotion = value; } }
        public int Applied { get => _applied; set { if (value == _applied) return; _applied = value; } }
        public bool IsExist { get => _isExist; set { if (value == _isExist) return; _isExist = value; } }
        public bool IsTrueValue { get => _isTrueValue; set { if (value == _isTrueValue) return; _isTrueValue = value;OnPropertyChanged(); } }

        #endregion
    }
}
