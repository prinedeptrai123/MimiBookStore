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
        private bool _isTrueValue;

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
        public int Inventory { get => _inventory; set { if (value == _inventory) return; _inventory = value;OnPropertyChanged(); } }
        /// <summary>
        /// Tổng số lượng sách đã bán ra
        /// </summary>
        public int Sold { get => _sold; set { if (value == _sold) return; _sold = value; } }
        /// <summary>
        /// Ảnh bìa
        /// </summary>
        public BitmapImage Image { get => _image; set { if (value == _image) return; _image = value; OnPropertyChanged(); } }

        public bool IsTrueValue { get => _isTrueValue; set { if (value == _isTrueValue) return;_isTrueValue = value;OnPropertyChanged(); } }

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
                    int ThemeID = isTheme(BookInfo.Theme);
                    int TypeID = isType(BookInfo.Type);
                    int CompanyID = isCompany(BookInfo.Company);
                    if(ThemeID!=0 && TypeID!=0 && CompanyID != 0)
                    {
                        var find = DB.Books.Where(x => x.Book_Name.ToLower() == BookInfo.Name.ToLower()
                    && x.Book_Author.ToLower() == BookInfo.Author.ToLower() && x.Book_Theme == ThemeID
                    && x.Book_Type == TypeID && x.Book_Company == CompanyID).Select(x => x.Book_ID).FirstOrDefault();

                        if (find != 0)
                        {
                            return find;
                        }
                    }                   
                }
            }
            catch
            {

            }
            return 0;
        }

        /// <summary>
        /// Hàm trả về tổng số sách có trong kho
        /// </summary>
        /// <returns></returns>
        public int sumBook()
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    var sum = DB.Books.Sum(x => x.Book_Count);

                    return sum;
                }
            }
            catch
            {

            }
            return 0;
        }

        /// <summary>
        /// Đánh dấu là dữ liệu đã bị xóa
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        public bool deleteBook(int BookID)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    //Tìm sách theo ID
                    var find = DB.Books.Find(BookID);
                    if (find != null)
                    {
                        //Thay đổi dữ liệu
                        find.Exist = true;

                        //Lưu thay đổi
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

        /// <summary>
        /// Hàm cập nhật thông tin mới cho sách
        /// </summary>
        /// <param name="BookInfo"></param>
        /// <returns></returns>
        public bool updateBookInfo(CBook BookInfo)
        {
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    //Tìm sách theo ID
                    var find = DB.Books.Find(BookInfo.ID);
                    if (find != null)
                    {
                        //Tìm ID
                        int ThemeID = isTheme(BookInfo.Theme);
                        int TypeID = isType(BookInfo.Type);
                        int CompanyID = isCompany(BookInfo.Company);

                        if(ThemeID!=0 && TypeID != 0 || CompanyID != 0)
                        {
                            //Cập nhật thông tin mới cho sách
                            find.Book_Name = BookInfo.Name;
                            find.Book_Author = BookInfo.Author;
                            find.Book_Theme = ThemeID;
                            find.Book_Type = TypeID;
                            find.Book_Company = CompanyID;
                            find.Book_Image = Help.ImageToByte(BookInfo.Image);
                            find.Book_Price = BookInfo.OutPrice;
                            find.Book_Promotion = BookInfo.Promotion;

                            //Lưu thay đổi
                            DB.SaveChanges();

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
        /// Hàm trả về danh sách sách lọc leo điều kiện
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Author"></param>
        /// <param name="Type"></param>
        /// <param name="Theme"></param>
        /// <param name="Company"></param>
        /// <param name="currentPage"></param>
        /// <param name="NumberPage"></param>
        /// <returns></returns>
        public List<CBook> ListBook(string Name,string Author,string Type,string Theme,string Company,int currentPage, int NumberPage)
        {
            List<CBook> List = new List<CBook>();
            try
            {
                using(var DB = new BookStoreDataEntities())
                {
                    List<Book> data = DB.Books.ToList();

                    //Lọc theo tên
                    if(Name.ToLower()=="tất cả" ||string.IsNullOrEmpty(Name))
                    {
                        //do nothing
                    }
                    else
                    {
                        data = data.Where(x => x.Book_Name.ToLower().Contains(Name.ToLower())).ToList();
                    }

                    //Lọc theo thể loại
                    if(Type.ToLower()=="tất cả")
                    {
                        //do nothing
                    }
                    else
                    {
                        int TypeID = isType(Type);
                        if (TypeID != 0)
                        {
                            data = data.Where(x => x.Book_Type == TypeID).ToList();
                        }
                    }

                    //Lọc theo chủ đề
                    if(Theme.ToLower() =="tất cả")
                    {
                        //do nothing
                    }
                    else
                    {
                        int ThemeID = isTheme(Theme);
                        if (ThemeID != 0)
                        {
                            data = data.Where(x => x.Book_Theme == ThemeID).ToList();
                        }                        
                    }

                    //Lọc theo NXB
                    if(Company.ToLower()=="tất cả")
                    {
                        //do nothing
                    }
                    else
                    {
                        int CompanyID = isCompany(Company);
                        if (CompanyID != 0)
                        {
                            data = data.Where(x => x.Book_Company == CompanyID).ToList();
                        }
                    }

                    //Lọc theo tác giả
                    if(Author.ToLower() =="tất cả")
                    {
                        //do nothing
                    }
                    else
                    {
                        data = data.Where(x => x.Book_Author.ToLower().Contains(Author.ToLower())).ToList();
                    }

                    //Lấy từng trang
                    if (currentPage > 0 && NumberPage > 0)
                    {
                        data = data.Skip((currentPage - 1) * NumberPage).Take(NumberPage).ToList();
                    }

                    if (data.Count > 0)
                    {
                        //Thêm vào trong danh sách
                        foreach (var item in data)
                        {

                            //Tính tổng sách đã bán ra
                            int totalNumber = 0;
                            if (DB.Bill_Detail.Where(x => x.Book_ID == item.Book_ID).Count() > 0)
                            {
                                totalNumber = DB.Bill_Detail.Where(x => x.Book_ID == item.Book_ID).Sum(x => x.Book_Count);
                            }

                            if (item.Exist == false)
                            {
                                //Tạo mới sách
                                CBook Book = new CBook
                                {
                                    ID = item.Book_ID,
                                    Name = item.Book_Name,
                                    Author = item.Book_Author,
                                    Company = item.Publishing_Company.Company_Name,
                                    Type = item.Book_Type1.Type_Names,
                                    Theme = item.Book_Theme1.Theme_Name,
                                    Inventory = item.Book_Count,
                                    OutPrice = (float)item.Book_Price,
                                    Promotion = (float)item.Book_Promotion,
                                    OutPricePromotion = item.Book_Promotion == 0 ? (float)item.Book_Price : (float)(item.Book_Price - item.Book_Price * item.Book_Promotion),
                                    Image = Help.ByteToImage(item.Book_Image),
                                    Sold = totalNumber
                                };

                                //Thêm vào danh sách
                                List.Add(Book);
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
        /// Hàm trả về danh sách lọc theo điều kiện
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Author"></param>
        /// <param name="Type"></param>
        /// <param name="Theme"></param>
        /// <param name="Company"></param>
        /// <param name="isSale"></param>
        /// <param name="currentPage"></param>
        /// <param name="NumberPage"></param>
        /// <returns></returns>
        public List<CBook> ListBook(string Name, string Author, string Type, string Theme, string Company,bool isSale, int currentPage, int NumberPage)
        {
            List<CBook> List = new List<CBook>();
            try
            {
                using (var DB = new BookStoreDataEntities())
                {
                    List<Book> data = DB.Books.Where(x => x.Book_Promotion != 0).ToList();

                    //Lọc theo tên
                    if (Name.ToLower() == "tất cả")
                    {
                        //do nothing
                    }
                    else
                    {
                        data = data.Where(x => x.Book_Name.ToLower().Contains(Name.ToLower())).ToList();
                    }

                    //Lọc theo thể loại
                    if (Type.ToLower() == "tất cả")
                    {
                        //do nothing
                    }
                    else
                    {
                        int TypeID = isType(Type);
                        if (TypeID != 0)
                        {
                            data = data.Where(x => x.Book_Type == TypeID).ToList();
                        }
                    }

                    //Lọc theo chủ đề
                    if (Theme.ToLower() == "tất cả")
                    {
                        //do nothing
                    }
                    else
                    {
                        int ThemeID = isTheme(Theme);
                        if (ThemeID != 0)
                        {
                            data = data.Where(x => x.Book_Theme == ThemeID).ToList();
                        }
                    }

                    //Lọc theo NXB
                    if (Company.ToLower() == "tất cả")
                    {
                        //do nothing
                    }
                    else
                    {
                        int CompanyID = isCompany(Company);
                        if (CompanyID != 0)
                        {
                            data = data.Where(x => x.Book_Company == CompanyID).ToList();
                        }
                    }

                    //Lọc theo tác giả
                    if (Author.ToLower() == "tất cả")
                    {
                        //do nothing
                    }
                    else
                    {
                        data = data.Where(x => x.Book_Author.ToLower().Contains(Author.ToLower())).ToList();
                    }

                    //Lấy từng trang
                    if (currentPage > 0 && NumberPage > 0)
                    {
                        data = data.Skip((currentPage - 1) * NumberPage).Take(NumberPage).ToList();
                    }

                    if (data.Count > 0)
                    {
                        //Thêm vào trong danh sách
                        foreach (var item in data)
                        {

                            //Tính tổng sách đã bán ra
                            int totalNumber = 0;
                            if (DB.Bill_Detail.Where(x => x.Book_ID == item.Book_ID).Count() > 0)
                            {
                                totalNumber = DB.Bill_Detail.Where(x => x.Book_ID == item.Book_ID).Sum(x => x.Book_Count);
                            }

                            if (item.Exist == false)
                            {
                                //Tạo mới sách
                                CBook Book = new CBook
                                {
                                    ID = item.Book_ID,
                                    Name = item.Book_Name,
                                    Author = item.Book_Author,
                                    Company = item.Publishing_Company.Company_Name,
                                    Type = item.Book_Type1.Type_Names,
                                    Theme = item.Book_Theme1.Theme_Name,
                                    Inventory = item.Book_Count,
                                    OutPrice = (float)item.Book_Price,
                                    Promotion = (float)item.Book_Promotion,
                                    OutPricePromotion = item.Book_Promotion == 0 ? (float)item.Book_Price : (float)(item.Book_Price - item.Book_Price * item.Book_Promotion),
                                    Image = Help.ByteToImage(item.Book_Image),
                                    Sold = totalNumber
                                };

                                //Thêm vào danh sách
                                List.Add(Book);
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
        /// Hàm tăng số lượng của sách trong kho
        /// </summary>
        /// <param name="BookID"></param>
        /// <param name="Number"></param>
        /// <returns></returns>
        public bool increaseBook(int BookID,int Number)
        {
            try
            {
                if(Number <=0)
                {
                    return false;
                }

                using(var DB = new BookStoreDataEntities())
                {
                    //Tìm sách theo ID
                    var find = DB.Books.Find(BookID);
                    if (find != null)
                    {
                        //Cập nhật số lượng sách mới
                        find.Book_Count = find.Book_Count + Number;

                        //Lưu thay đổi
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

        /// <summary>
        /// Hàm trả về danh sách tác giả
        /// </summary>
        /// <returns></returns>
        public List<string> ListAuthor()
        {
            List<string> List = new List<string>();
            try
            {
                using (var DB = new BookStoreDataEntities())
                {
                    var data = DB.Books.Select(x => x.Book_Author).Distinct();
                    if (data.Count() > 0)
                    {
                        foreach (var item in data)
                        {
                            List.Add(item);
                        }
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
