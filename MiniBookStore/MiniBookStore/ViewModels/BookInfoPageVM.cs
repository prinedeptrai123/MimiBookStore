using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniBookStore.ViewModels
{
    public class BookInfoPageVM:BaseViewModel
    {
        #region properties binding

        #endregion

        #region command binding

        public ICommand addNewBookCommand { get; set; }

        #endregion

        public BookInfoPageVM()
        {
            addNewBookCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                //Khởi tạo
                AddNewBookWindow wd = new AddNewBookWindow();
                wd.ShowDialog();
               
            }
               );
        }
    }
}
