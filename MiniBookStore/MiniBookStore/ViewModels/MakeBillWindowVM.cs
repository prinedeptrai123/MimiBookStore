using MiniBookStore.Models;
using MiniBookStore.Models.MyClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows;

namespace MiniBookStore.ViewModels
{
    public class MakeBillWindowVM:BaseViewModel
    {
        #region data binding

        private ObservableCollection<CBookBill> _listBook;
        public ObservableCollection<CBookBill> ListBook { get => _listBook; set { if (value == _listBook) return; _listBook = value; OnPropertyChanged(); } }

        private CBookBill _listBookSelectedItem;
        public CBookBill ListBookSelectedItem { get => _listBookSelectedItem; set { if (value == _listBookSelectedItem) return;_listBookSelectedItem = value;OnPropertyChanged(); } }

        private int _listBookSelectedIndex;
        public int ListBookSelectedIndex { get => _listBookSelectedIndex; set { if (value == _listBookSelectedIndex) return;_listBookSelectedIndex = value;OnPropertyChanged(); } }

        private BitmapImage _coverImage;
        public BitmapImage CoverImage { get => _coverImage; set { if (value == _coverImage) return; _coverImage = value; OnPropertyChanged(); } }

        private string _employeeName;
        public string EmployeeName { get => _employeeName; set { if (value == _employeeName) return; _employeeName = value; OnPropertyChanged(); } }

        private string _dateNow;
        public string DateNow { get => _dateNow; set { if (value == _dateNow) return; _dateNow = value; OnPropertyChanged(); } }

        #endregion

        #region command binding

        public ICommand LoadCommand { get; set; }
        public ICommand ListBookSelectionChanged { get; set; }
        public ICommand deleteCommand { get; set; }
        public ICommand editCommand { get; set; }

        #endregion

        public MakeBillWindowVM()
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListBook = DataTransfer.ListBookBill;
                EmployeeName = DataTransfer.EmployeeInfo.Name;
                DateNow = DateTime.Now.ToShortDateString();
                if (ListBook.Count == 1)
                {
                    CoverImage = ListBook[0].Image;
                }
            }
               );

            editCommand = new RelayCommand<object>((p) => {
                if (ListBookSelectedItem != null)
                {
                    if (ListBookSelectedItem.Count <= 0)
                    {
                        ListBookSelectedItem.IsTrueValue = false;
                    }
                    else
                    {
                        if(ListBookSelectedItem.Inventory+1 - ListBookSelectedItem.Count < 0)
                        {
                            ListBookSelectedItem.IsTrueValue = false;

                        }
                        else
                        {
                            ListBookSelectedItem.IsTrueValue = true;
                        }                                          
                    }
                }
                return true;
            }, (p) =>
            {
                if (ListBookSelectedItem != null)
                {
                    ListBookSelectedItem.Inventory = ListBookSelectedItem.Inventory + 1 - ListBookSelectedItem.Count;
                }
            }
               );

            deleteCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (ListBookSelectedItem != null)
                {
                    //Xóa theo index
                    ListBook.RemoveAt(ListBookSelectedIndex);
                                    
                }
            }
               );

            ListBookSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (ListBookSelectedItem != null)
                {
                    CoverImage = ListBookSelectedItem.Image;
                }
            }
               );
        }
    }
}
