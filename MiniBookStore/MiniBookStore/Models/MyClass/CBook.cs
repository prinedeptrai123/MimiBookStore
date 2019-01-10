using MiniBookStore.ViewModels;
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
    public class CBook:BaseViewModel
    {
        #region design pattern singleton

        private static CBook _ins;
        public static CBook Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new CBook();
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
        public string Name { get => _name; set { if (value == _name) return; _name = value; OnPropertyChanged(); } }
        /// <summary>
        /// Tên tác giả
        /// </summary>
        public string Author { get => _author; set { if (value == _author) return; _author = value; OnPropertyChanged(); } }
        /// <summary>
        /// Thể loại sách
        /// </summary>
        public string Type { get => _type; set { if (value == _type) return; _type = value; OnPropertyChanged(); } }
        /// <summary>
        /// Chủ đề sách
        /// </summary>
        public string Theme { get => _theme; set { if (value == _theme) return; _theme = value; OnPropertyChanged(); } }
        /// <summary>
        /// Nhà xuất bản
        /// </summary>
        public string Company { get => _company; set { if (value == _company) return; _company = value; OnPropertyChanged(); } }
        /// <summary>
        /// Giá bán ra
        /// </summary>
        public float OutPrice { get => _outPrice; set { if (value == _outPrice) return; _outPrice = value; OnPropertyChanged(); } }
        /// <summary>
        /// Phần trăm khuyến mãi
        /// </summary>
        public float Promotion { get => _promotion; set { if (value == _promotion) return; _promotion = value; OnPropertyChanged(); } }
        /// <summary>
        /// Giá bán ra sau khi đã trừ đi khuyến mãi
        /// </summary>
        public float OutPricePromotion { get => _outPricePromotion; set { if (value == _outPricePromotion) return; _outPricePromotion = value; OnPropertyChanged(); } }
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
        public BitmapImage Image { get => _image; set { if (value == _image) return; _image = value; OnPropertyChanged(); } }

        #endregion

        #region method

        /// <summary>
        /// Hàm trả về danh sách loại sách
        /// </summary>
        /// <returns></returns>
        public List<string> ListType()
        {
            List<string> List = new List<string>();
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var data = DB.Book_Type;

                    if (data.Count() > 0)
                    {
                        foreach(var item in data)
                        {
                            List.Add(item.Type_Names);
                        }
                    }
                }
            }
            catch
            {

            }

            return List;
        }

        /// <summary>
        /// Hàm trả về danh sách chủ đề theo thể loại sách
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public List<string> ListThemeOfType(string Type)
        {
            List<string> List = new List<string>();
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    //Tìm id của thể loại
                    var find = DB.Book_Type.Where(x => x.Type_Names.ToLower() == Type.ToLower()).Select(x=>x.Type_IDs).FirstOrDefault();
                    if (find != 0)
                    {
                        var data = DB.Book_Theme.Where(x => x.Type_IDs == find);
                        if (data.Count() > 0)
                        {
                            foreach (var item in data)
                            {
                                List.Add(item.Theme_Name);
                            }
                        }
                        
                    }
                }
            }
            catch
            {

            }

            return List;
        }

        /// <summary>
        /// Hàm trả về danh sách nhà xuất bản
        /// </summary>
        /// <returns></returns>
        public List<string> ListCompany()
        {
            List<string> List = new List<string>();
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var data = DB.Publishing_Company;
                    if (data.Count() > 0)
                    {
                        foreach(var item in data)
                        {
                            List.Add(item.Company_Name);
                        }
                    }
                }
            }
            catch
            {

            }
            return List;
        }

        /// <summary>
        /// hàm trả về id của thể loại nếu có, nếu không có trả về 0
        /// </summary>
        /// <param name="Theme"></param>
        /// <returns></returns>
        public int isType(string Type)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var data = DB.Book_Type.Where(x => x.Type_Names.ToLower() == Type.ToLower()).Select(x => x.Type_IDs).FirstOrDefault();
                    if (data != 0)
                    {
                        return data;
                    }
                }
            }
            catch
            {

            }
            return 0;
        }

        /// <summary>
        /// hàm trả về id của chủ đề nếu có
        /// </summary>
        /// <param name="Theme"></param>
        /// <returns></returns>
        public int isTheme(string Theme)
        {
            try
            {
                using (var DB = new BookStoreDataEntities())
                {
                    var data = DB.Book_Theme.Where(x => x.Theme_Name.ToLower() == Theme.ToLower()).Select(x => x.Theme_ID).FirstOrDefault();
                    if (data != 0)
                    {
                        return data;
                    }
                }
            }
            catch
            {

            }
            return 0;
        }

        /// <summary>
        /// hàm trả về id nhà xuất bản nếu có
        /// </summary>
        /// <param name="Company"></param>
        /// <returns></returns>
        public int isCompany(string Company)
        {
            try
            {
                using (var DB = new BookStoreDataEntities())
                {
                    var data = DB.Publishing_Company.Where(x => x.Company_Name.ToLower() == Company.ToLower()).Select(x => x.Company_ID).FirstOrDefault();
                    if (data != 0)
                    {
                        return data;
                    }
                }
            }
            catch
            {

            }
            return 0;
        }

        /// <summary>
        /// Hàm trả về id sách nếu sách có tồn tại
        /// </summary>
        /// <param name="BookInfo"></param>
        /// <returns></returns>
        public int isExistBook(CBook BookInfo)
        {
            try
            {
                using(var DB= new BookStoreDataEntities())
                {
                    var find = DB.Books.Where(x => x.Book_Name.ToLower() == BookInfo.Name.ToLower()
                    && x.Book_Author.ToLower() == BookInfo.Author.ToLower() && x.Book_Theme == isTheme(BookInfo.Theme)
                    && x.Book_Type == isType(BookInfo.Type) && x.Book_Company == isCompany(BookInfo.Company)).Select(x => x.Book_ID).FirstOrDefault();

                    if (find != 0)
                    {
                        return find;
                    }
                    
                }
            }
            catch
            {

            }
            return 0;
        }

        #endregion
    }
}
