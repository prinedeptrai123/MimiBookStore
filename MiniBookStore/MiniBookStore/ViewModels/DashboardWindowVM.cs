using MiniBookStore.Models;
using MiniBookStore.Models.MyClass;
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

        private Thickness _gridCursorMargin;
        /// <summary>
        /// Thuộc tính margin của thanh trượt
        /// </summary>
        public Thickness GridCursorMargin { get => _gridCursorMargin;set { if (value == _gridCursorMargin) return;_gridCursorMargin = value;OnPropertyChanged(); } }

        private Page _framePage;
        /// <summary>
        /// Thuộc tính content của Frame ở đây lưu page cần chuyển qua
        /// </summary>
        public Page FramePage { get => _framePage;set { if (value == _framePage) return;_framePage = value;OnPropertyChanged(); } }

        #endregion

        #region command binding

        public ICommand OpenMenuCommand { get; set; }
        public ICommand CloseMenuCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand PayCommand { get; set; }
        public ICommand BookCommand { get; set; }
        public ICommand PromotionCommand { get; set; }
        public ICommand ChartCommand { get; set; }
        public ICommand EmployeeCommand { get; set; }
        public ICommand CustomerCommand { get; set; }
        public ICommand AccountCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        #endregion

        public DashboardWindowVM()
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                //Khởi tạo
                CloseMenuVisibility = Visibility.Collapsed;
                OpenMenuVisibility = Visibility.Visible;

                GridCursorMargin = new Thickness(0, 70 + 1*60, 0, 0);
                FramePage = new BookMenuPage();

                DataTransfer.EmployeeInfo = new CEmployee
                {
                    Name = "Lê Tường Qui",
                    ID = 1
                };             
            }
               );

            PayCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {               
                GridCursorMargin = new Thickness(0, 70 + 0, 0, 0);
                FramePage = new SellMenuPage();
            }
               );

            BookCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GridCursorMargin = new Thickness(0, 70 + 1 * 60, 0, 0);
                FramePage = new BookMenuPage();
            }
               );

            PromotionCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GridCursorMargin = new Thickness(0, 70 + 2 * 60, 0, 0);
                FramePage = new PromotionMenuPage();
            }
               );

            ChartCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GridCursorMargin = new Thickness(0, 70 + 3 * 60, 0, 0);
                FramePage = new ReportMenuPage();
            }
               );

            EmployeeCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GridCursorMargin = new Thickness(0, 70 + 4 * 60, 0, 0);
            }
               );

            CustomerCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GridCursorMargin = new Thickness(0, 70 + 5 * 60, 0, 0);
            }
               );

            AccountCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GridCursorMargin = new Thickness(0, 70 + 6 * 60, 0, 0);
            }
               );

            ExitCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GridCursorMargin = new Thickness(0, 70 + 7 * 60, 0, 0);
                (p as Window)?.Close();
            }
               );

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
