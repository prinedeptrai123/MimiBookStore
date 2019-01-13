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
    public class SellMenuPageVM : BaseViewModel
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

        #region data binding

        public ICommand LoadCommand { get; set; }
        public ICommand ProductCommand { get; set; }
        public ICommand HistoryCommand { get; set; }

        #endregion

        public SellMenuPageVM()
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GridCursorMargin = new Thickness(10, 0, 0, 0);
                FramePage = new ProductPage();
            }
               );
           

            ProductCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GridCursorMargin = new Thickness(10, 0, 0, 0);
                FramePage = new ProductPage();
            }
               );

            HistoryCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GridCursorMargin = new Thickness(10 + 150, 0, 0, 0);
                FramePage = new HistoryBillPage();
            }
              );
        }
    }
}

