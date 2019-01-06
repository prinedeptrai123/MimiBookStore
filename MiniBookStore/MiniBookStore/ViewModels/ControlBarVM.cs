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
    /// <summary>
    /// Lớp xử lý của controlbar
    /// </summary>
    public class ControlBarVM:BaseViewModel
    {
        #region commands

        public ICommand CloseWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MouseMoveCommand { get; set; }

        #endregion

        public ControlBarVM()
        {
            CloseWindowCommand = new RelayCommand<object>((p) =>
            {
                return p == null ? false : true;
            },
            (p) =>
            {
                //Lấy ra cha của usercontrol
                FrameworkElement window = getWindowParrent(p as UserControl);

                //Đóng cửa sổ
                (window as Window)?.Close();
            }
               );

            MaximizeWindowCommand = new RelayCommand<object>((p) =>
            {
                return p == null ? false : true;
            },
            (p) =>
            {
                //Lấy ra cha của usercontrol
                FrameworkElement window = getWindowParrent(p as UserControl);

                var w = window as Window;
                if (w != null)
                {
                    if(w.WindowState != WindowState.Maximized)
                    {
                        w.WindowState = WindowState.Maximized;
                    }
                    else
                    {
                        w.WindowState = WindowState.Normal;
                    }
                    
                }
                
            }
               );

            MinimizeWindowCommand = new RelayCommand<object>((p) =>
            {
                return p == null ? false : true;
            },
            (p) =>
            {
                //Lấy ra cha của usercontrol
                FrameworkElement window = getWindowParrent(p as UserControl);

                var w = window as Window;
                if (w != null)
                {
                    if (w.WindowState != WindowState.Minimized)
                    {
                        w.WindowState = WindowState.Minimized;
                    }
                    else
                    {
                        w.WindowState = WindowState.Maximized;
                    }
                }
            }
               );

            MouseMoveCommand = new RelayCommand<object>((p) =>
            {
                return p == null ? false : true;
            },
            (p) =>
            {
                //Lấy ra cha của usercontrol
                FrameworkElement window = getWindowParrent(p as UserControl);

                var w = window as Window;
                if (w != null)
                {
                    w.DragMove();
                }
            }
               );
        }

        /// <summary>
        /// Đệ quy để lấy ra được parent lớn nhất ở đây mong muốn là lấy ra được window chứa usercontrol
        /// </summary>
        /// <param name="obj"></param>
        FrameworkElement getWindowParrent(UserControl obj)
        {
            FrameworkElement parent = obj;
            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }
    }
}
