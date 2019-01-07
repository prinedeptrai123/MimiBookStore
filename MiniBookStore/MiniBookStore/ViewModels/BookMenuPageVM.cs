using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MiniBookStore.ViewModels
{
    public class BookMenuPageVM:BaseViewModel
    {
        #region properties binding

        private Page _framePage;
        /// <summary>
        /// Thuộc tính content của Frame ở đây lưu page cần chuyển qua
        /// </summary>
        public Page FramePage { get => _framePage; set { if (value == _framePage) return; _framePage = value; OnPropertyChanged(); } }

        private Thickness _gridCursorMargin;
        /// <summary>
        /// Thuộc tính margin của thanh trượt
        /// </summary>
        public Thickness GridCursorMargin { get => _gridCursorMargin; set { if (value == _gridCursorMargin) return; _gridCursorMargin = value; OnPropertyChanged(); } }

        #endregion

        #region command binding

        public ICommand LoadCommand { get; set; }

        #endregion

        public BookMenuPageVM()
        {

            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                //Khởi tạo

                GridCursorMargin = new Thickness(10, 0, 0, 0);
                FramePage = new BookInfoPage();
            }
               );

        }
    }
}
