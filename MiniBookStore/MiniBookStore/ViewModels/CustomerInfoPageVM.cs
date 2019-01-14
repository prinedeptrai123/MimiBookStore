using MiniBookStore.Models;
using MiniBookStore.Models.MyClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiniBookStore.ViewModels
{
    public class CustomerInfoPageVM:BaseViewModel
    {
        #region Global 

        private int _currentPage;
        public int CurrentPage { get => _currentPage; set { if (value == _currentPage) return; _currentPage = value; OnPropertyChanged(); } }

        int NumberPage = 15;

        #endregion
      
        #region data binding

        private ObservableCollection<CCustomer> _listCustomer;
        public ObservableCollection<CCustomer> ListCustomer { get => _listCustomer; set { if (value == _listCustomer) return; _listCustomer = value; OnPropertyChanged(); } }

        private CCustomer _listCustomerSelectedItem;
        public CCustomer ListCustomerSelectedItem { get => _listCustomerSelectedItem; set { if (value == _listCustomerSelectedItem) return; _listCustomerSelectedItem = value; OnPropertyChanged(); } }

        private string _filterString;
        public string FilterString { get => _filterString; set { if (value == _filterString) return; _filterString = value; OnPropertyChanged(); } }

        #endregion

        #region command binding

        public ICommand LoadCommand { get; set; }
        public ICommand searchCommand { get;set; }

        public ICommand PreviousPageCommand { get; set; }
        public ICommand NextPageCommand { get; set; }


        #endregion

        public CustomerInfoPageVM()
        {
            PreviousPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (CurrentPage > 1)
                {
                    CurrentPage = CurrentPage - 1;
                    if (Help.isInt(FilterString) == true)
                    {
                        ListCustomer = new ObservableCollection<CCustomer>(CCustomer.Ins.ListCustomerFilterPhone(FilterString, CurrentPage, NumberPage));
                    }
                    else
                    {
                        ListCustomer = new ObservableCollection<CCustomer>(CCustomer.Ins.ListCustomerFilterName(FilterString, CurrentPage, NumberPage));
                    }
                }
            }
              );

            NextPageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentPage = CurrentPage + 1;
                if (Help.isInt(FilterString) == true)
                {
                    ListCustomer = new ObservableCollection<CCustomer>(CCustomer.Ins.ListCustomerFilterPhone(FilterString, CurrentPage, NumberPage));
                }
                else
                {
                    ListCustomer = new ObservableCollection<CCustomer>(CCustomer.Ins.ListCustomerFilterName(FilterString, CurrentPage, NumberPage));
                }
            }
              );

            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentPage = 1;
                FilterString = "";

                ListCustomer = new ObservableCollection<CCustomer>(CCustomer.Ins.ListCustomerFilterPhone(FilterString, CurrentPage, NumberPage));
            }
               );

            searchCommand = new RelayCommand<object>((p) => {
                return true;
            }, (p) =>
            {
                if (Help.isInt(FilterString) == true)
                {
                    ListCustomer = new ObservableCollection<CCustomer>(CCustomer.Ins.ListCustomerFilterPhone(FilterString, CurrentPage, NumberPage));
                }
                else
                {
                    ListCustomer = new ObservableCollection<CCustomer>(CCustomer.Ins.ListCustomerFilterName(FilterString, CurrentPage, NumberPage));
                }

            }
              );
        }
    }
}
