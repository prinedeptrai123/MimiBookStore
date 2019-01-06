using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MiniBookStore.Models.MyClass
{
    /// <summary>
    /// Lưu thông tin của sách
    /// </summary>
    class CBook
    {
        #region private properties

        private int _iD;
        private string _name;
        private string _author;
        private string _type;
        private string _theme;
        private string _company;
        private float _outPrice;
        private float _promotion;
        private float _outPricePromotion;
        private int _inventory;
        private int _sold;
        private BitmapImage _image;

        #endregion

        #region public properties
        /// <summary>
        /// Id sách
        /// </summary>
        public int ID { get => _iD; set { if (value == _iD) return;_iD = value; } }
        /// <summary>
        /// Tên sách
        /// </summary>
        public string Name { get => _name; set { if (value == _name) return; _name = value; } }
        /// <summary>
        /// Tên tác giả
        /// </summary>
        public string Author { get => _author; set { if (value == _author) return; _author = value; } }
        /// <summary>
        /// Thể loại sách
        /// </summary>
        public string Type { get => _type; set { if (value == _type) return; _type = value; } }
        /// <summary>
        /// Chủ đề sách
        /// </summary>
        public string Theme { get => _theme; set { if (value == _theme) return; _theme = value; } }
        /// <summary>
        /// Nhà xuất bản
        /// </summary>
        public string Company { get => _company; set { if (value == _company) return; _company = value; } }
        /// <summary>
        /// Giá bán ra
        /// </summary>
        public float OutPrice { get => _outPrice; set { if (value == _outPrice) return; _outPrice = value; } }
        /// <summary>
        /// Phần trăm khuyến mãi
        /// </summary>
        public float Promotion { get => _promotion; set { if (value == _promotion) return; _promotion = value; } }
        /// <summary>
        /// Giá bán ra sau khi đã trừ đi khuyến mãi
        /// </summary>
        public float OutPricePromotion { get => _outPricePromotion; set { if (value == _outPricePromotion) return; _outPricePromotion = value; } }
        /// <summary>
        /// Tổng số sách còn tồn trong kho
        /// </summary>
        public int Inventory { get => _inventory; set { if (value == _inventory) return; _inventory = value; } }
        /// <summary>
        /// Tổng số lượng sách đã bán ra
        /// </summary>
        public int Sold { get => _sold; set { if (value == _sold) return; _sold = value; } }
        /// <summary>
        /// Ảnh bìa
        /// </summary>
        public BitmapImage Image { get => _image; set { if (value == _image) return; _image = value; } }

        #endregion

        #region method

        #endregion
    }
}
