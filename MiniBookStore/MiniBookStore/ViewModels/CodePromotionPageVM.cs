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
    public class CodePromotionPageVM:BaseViewModel
    {

        #region Global 

        int currentPage = 1;
        int NumberPage = 10;

        #endregion

        #region data binding

        private ObservableCollection<CPromotion_Code> _listCode;
        public ObservableCollection<CPromotion_Code> ListCode { get => _listCode; set { if (value == _listCode) return; _listCode = value; OnPropertyChanged(); } }

        private CPromotion_Code _selectedItem;
        public CPromotion_Code SelectedItem { get => _selectedItem; set { if (value == _selectedItem) return; _selectedItem = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _listType;
        public ObservableCollection<string> ListType { get => _listType; set { if (value == _listType) return; _listType = value; OnPropertyChanged(); } }

        private string _iD;
        public string ID { get => _iD; set { if (value == _iD) return;_iD = value;OnPropertyChanged(); } }

        private string _name;
        public string Name { get => _name; set { if (value == _name) return; _name = value; OnPropertyChanged(); } }

        private string _type;
        public string Type { get => _type; set { if (value == _type) return; _type = value; OnPropertyChanged(); } }

        private DateTime _dateBegin;
        public DateTime DateBegin { get => _dateBegin; set { if (value == _dateBegin) return; _dateBegin = value; OnPropertyChanged(); } }

        private DateTime _dateEnd;
        public DateTime DateEnd { get => _dateEnd; set { if (value == _dateEnd) return; _dateEnd = value; OnPropertyChanged(); } }

        #endregion

        #region properties binding

        private bool _isChecked;
        public bool IsChecked { get => _isChecked; set { if (value == _isChecked) return; _isChecked = value; OnPropertyChanged(); } }

        #endregion

        #region command binding

        public ICommand LoadCommand { get; set; }
        public ICommand addCommand { get; set; }
        public ICommand deleteCommand { get; set; }
        public ICommand CheckedCommand { get; set; }
        public ICommand restoreCommand { get; set; }
        public ICommand editCommand { get; set; }

        #endregion

        public CodePromotionPageVM()
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                IsChecked = false;
                ListCode = new ObservableCollection<CPromotion_Code>(CBill.Ins.ListCode(IsChecked, currentPage, NumberPage));
                ListType = new ObservableCollection<string>(CBill.Ins.ListStringTypeOfPromotion());

                DateBegin = DateTime.Now;
                DateEnd = DateTime.Now;
            }
              );

            CheckedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListCode = new ObservableCollection<CPromotion_Code>(CBill.Ins.ListCode(IsChecked, currentPage, NumberPage));
            }
              );

            editCommand = new RelayCommand<object>((p) => {
                if (SelectedItem != null)
                {
                    if (string.IsNullOrEmpty(SelectedItem.ID) || string.IsNullOrEmpty(SelectedItem.Name))
                    {
                        SelectedItem.IstrueValue = false;
                    }
                    else
                    {                       
                        if (CBill.Ins.isCodeName(SelectedItem.Name) != SelectedItem.ID && CBill.Ins.isCodeName(SelectedItem.Name) != "")
                        {
                            SelectedItem.IstrueValue = false;
                        }
                        else
                        {                          
                            if (SelectedItem.DateBegin > SelectedItem.DateEnd)
                            {
                                SelectedItem.IstrueValue = false;
                            }
                            else
                            {
                                SelectedItem.IstrueValue = true;
                            }
                        }

                    }                                                       
                }
                return true;
            }, (p) =>
            {
                if (SelectedItem != null)
                {
                    if (CBill.Ins.updateCode(SelectedItem) == true)
                    {
                        MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        //Trả về thông tin như cũ
                        ListCode = new ObservableCollection<CPromotion_Code>(CBill.Ins.ListCode(IsChecked, currentPage, NumberPage));
                    }
                }
            }
              );

            restoreCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItem != null)
                {
                    if (CBill.Ins.restoreCode(SelectedItem.ID) == true)
                    {
                        MessageBox.Show("Khôi phục thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        ListCode = new ObservableCollection<CPromotion_Code>(CBill.Ins.ListCode(IsChecked, currentPage, NumberPage));
                    }
                    else
                    {
                        MessageBox.Show("Khôi phục thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
              );

            deleteCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItem != null)
                {
                    if (CBill.Ins.deleteCode(SelectedItem.ID) == true)
                    {
                        MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        ListCode = new ObservableCollection<CPromotion_Code>(CBill.Ins.ListCode(IsChecked, currentPage, NumberPage));
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
              );

            addCommand = new RelayCommand<object>((p) => {
                if(string.IsNullOrEmpty(ID) || string.IsNullOrEmpty(Name))
                {
                    return false;
                }
                if(DateBegin== null || DateEnd == null || Type == null)
                {
                    return false;
                }

                if (CBill.Ins.isCode(ID) !="")
                {
                    return false;
                }
                if (CBill.Ins.isCodeName(Name) !="")
                {
                    return false;
                }
                if (DateBegin > DateEnd)
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                //Tạo mới
                CPromotion_Code Code = new CPromotion_Code
                {
                    ID = ID,
                    Name = Name,
                    Type = Type,
                    DateBegin = DateBegin,
                    DateEnd = DateEnd
                };

                if (CBill.Ins.addNewCode(Code) == true)
                {
                    MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    ListCode = new ObservableCollection<CPromotion_Code>(CBill.Ins.ListCode(IsChecked, currentPage, NumberPage));
                    ID = "";
                    Name = "";
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
