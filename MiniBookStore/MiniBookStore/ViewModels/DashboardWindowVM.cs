using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MiniBookStore.ViewModels
{
    class DashboardWindowVM:BaseViewModel
    {
        #region properties binding

        private Visibility _closeMenuVisibility;
        /// <summary>
        /// Thuộc tính ẩn của button đóng menu
        /// </summary>
        public Visibility CloseMenuVisibility { get => _closeMenuVisibility; set { if (value == _closeMenuVisibility) return;_closeMenuVisibility = value;OnPropertyChanged(); } }

        private Visibility _openMenuVisibility;
        /// <summary>
        /// Thuộc tính ẩn của button mở menu
        /// </summary>
        public Visibility OpenMenuVisibility { get => _openMenuVisibility; set { if (value == _openMenuVisibility) return; _openMenuVisibility = value; OnPropertyChanged(); } }

        #endregion

        #region command binding

        public ICommand OpenMenuCommand { get; set; }
        public ICommand CloseMenuCommand { get; set; }

        #endregion

        public DashboardWindowVM()
        {
            //Khởi tạo
            CloseMenuVisibility = Visibility.Collapsed;
            OpenMenuVisibility = Visibility.Visible;

            OpenMenuCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CloseMenuVisibility = Visibility.Visible;
                OpenMenuVisibility = Visibility.Collapsed;
            }
               );

            CloseMenuCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CloseMenuVisibility = Visibility.Collapsed;
                OpenMenuVisibility = Visibility.Visible;
            }
              );

        }
    }
}
