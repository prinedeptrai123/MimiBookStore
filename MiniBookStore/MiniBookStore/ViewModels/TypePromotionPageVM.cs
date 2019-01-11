using MiniBookStore.Models;
using MiniBookStore.Models.MyClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace MiniBookStore.ViewModels
{
    public class TypePromotionPageVM:BaseViewModel
    {
        #region Global 

        int currentPage = 1;
        int NumberPage = 10;

        #endregion

        #region data binding

        private ObservableCollection<CPromotion_Type> _listType;
        public ObservableCollection<CPromotion_Type> ListType { get => _listType; set { if (value == _listType) return; _listType = value; OnPropertyChanged(); } }

        private CPromotion_Type _selectedItem;
        public CPromotion_Type SelectedItem { get => _selectedItem; set { if (value == _selectedItem) return; _selectedItem = value; OnPropertyChanged(); } }

        private string _name;
        public string Name { get => _name; set { if (value == _name) return;_name = value;OnPropertyChanged(); } }

        private string _count;
        public string Count { get => _count; set { if (value == _count) return; _count = value; OnPropertyChanged(); } }

        private string _promotion;
        public string Promotion { get => _promotion; set { if (value == _promotion) return; _promotion = value; OnPropertyChanged(); } }

        private string _filterString;
        public string FilterString { get => _filterString; set { if (value == _filterString) return; _filterString = value; OnPropertyChanged(); } }

        #endregion

        #region properties binding

        private bool _isChecked;
        public bool IsChecked { get => _isChecked; set { if (value == _isChecked) return; _isChecked = value; OnPropertyChanged(); } } 

        #endregion

        #region command binding

        public ICommand addCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand deleteCommand { get; set; }
        public ICommand editCommand { get; set; }
        public ICommand CheckedCommand { get; set; }
        public ICommand restoreCommand { get; set; }
        public ICommand searchCommand { get; set; }

        #endregion

        public TypePromotionPageVM()
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsChecked = false;
                ListType = new ObservableCollection<CPromotion_Type>(CBill.Ins.ListTypeOfPromotion(IsChecked, currentPage,NumberPage));
            }
               );

            searchCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (FilterString == "")
                {
                    ListType = new ObservableCollection<CPromotion_Type>(CBill.Ins.ListTypeOfPromotion(IsChecked, currentPage, NumberPage));
                }
                else
                {
                    ListType = new ObservableCollection<CPromotion_Type>(CBill.Ins.ListTypeOfPromotion(FilterString, IsChecked, currentPage, NumberPage));
                }

            }
               );

            CheckedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListType = new ObservableCollection<CPromotion_Type>(CBill.Ins.ListTypeOfPromotion(IsChecked, currentPage, NumberPage));

            }
               );

            restoreCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItem != null)
                {
                    if (CBill.Ins.restorePromotionType(SelectedItem.ID) == true)
                    {
                        MessageBox.Show("Khôi phục thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        ListType = new ObservableCollection<CPromotion_Type>(CBill.Ins.ListTypeOfPromotion(IsChecked, currentPage, NumberPage));
                    }
                    else
                    {
                        MessageBox.Show("Khôi phục thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
               );

            editCommand = new RelayCommand<object>((p) => {
                if (SelectedItem != null)
                {
                    if (string.IsNullOrEmpty(SelectedItem.Name))
                    {
                        SelectedItem.IsTrueValue = false;                     
                    }
                    else
                    {
                        if (CBill.Ins.isPromotionType(SelectedItem.Name) != SelectedItem.ID && CBill.Ins.isPromotionType(SelectedItem.Name) != 0)
                        {
                            SelectedItem.IsTrueValue = false;
                            
                        }
                        else
                        {
                            if (SelectedItem.BookCount < 0)
                            {
                                SelectedItem.IsTrueValue = false;
                            }
                            else
                            {
                                if (SelectedItem.Promotion < 0 || SelectedItem.Promotion > 1)
                                {
                                    SelectedItem.IsTrueValue = false;
                                }
                                else
                                {
                                    SelectedItem.IsTrueValue = true;
                                }
                            }
                        }
                    }

                }
                return true;
            }, (p) =>
            {
                if (SelectedItem != null)
                {
                    if (CBill.Ins.updatePromotionType(SelectedItem) == true)
                    {
                        MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        ListType = new ObservableCollection<CPromotion_Type>(CBill.Ins.ListTypeOfPromotion(IsChecked, currentPage, NumberPage));
                    }
                }
            }
               );

            deleteCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItem != null)
                {
                    if(SelectedItem.Applied != 0)
                    {
                        MessageBox.Show("Không thể xóa loại đã được áp dụng", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        if (CBill.Ins.deletePromotionType(SelectedItem.ID) == true)
                        {
                            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            //load lại 
                            ListType = new ObservableCollection<CPromotion_Type>(CBill.Ins.ListTypeOfPromotion(IsChecked, currentPage, NumberPage));
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                
            }
               );

            addCommand = new RelayCommand<object>((p) => {
                if(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Promotion) || string.IsNullOrEmpty(Count))
                {
                    return false;
                }
                if (CBill.Ins.isPromotionType(Name) != 0)
                {
                    return false;
                }
                if (Help.isInt(Count) == false || Help.isFloat(Promotion) == false)
                {
                    return false;
                }
                if(int.Parse(Count) <0 || float.Parse(Promotion)<=0 || float.Parse(Promotion) > 1)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                //Tạo mới một Type
                CPromotion_Type Type = new CPromotion_Type
                {
                    Name = Name,
                    BookCount = int.Parse(Count),
                    Promotion = float.Parse(Promotion)
                };

                if (CBill.Ins.addNewPromotionType(Type) == true)
                {
                    MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    ListType = new ObservableCollection<CPromotion_Type>(CBill.Ins.ListTypeOfPromotion(IsChecked, currentPage, NumberPage));
                }
                else
                {
                    MessageBox.Show("Thêm thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
               );
        }
    }
}
