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
    public class TypeAndThemePageVM:BaseViewModel
    {
        #region data binding

        private ObservableCollection<CBookType> _listType;
        public ObservableCollection<CBookType> ListType { get => _listType; set { if (value == _listType) return; _listType = value; OnPropertyChanged(); } }

        private CBookType _typeSelectedItem;
        public CBookType TypeSelectedItem { get => _typeSelectedItem; set { if (value == _typeSelectedItem) return; _typeSelectedItem = value; OnPropertyChanged(); } }

        private ObservableCollection<CBookTheme> _listTheme;
        public ObservableCollection<CBookTheme> ListTheme { get => _listTheme; set { if (value == _listTheme) return; _listTheme = value; OnPropertyChanged(); } }

        private CBookTheme _themeSelectedItem;
        public CBookTheme ThemeSelectedItem { get => _themeSelectedItem; set { if (value == _themeSelectedItem) return; _themeSelectedItem = value; OnPropertyChanged(); } }

        private string _currentType;
        public string CurrentType { get => _currentType; set { if (value == _currentType) return; _currentType = value; OnPropertyChanged(); } }

        private bool _typeIsChecked;
        public bool TypeIsChecked { get => _typeIsChecked; set { if (value == _typeIsChecked) return; _typeIsChecked = value; OnPropertyChanged(); } }

        private string _typeName;
        public string TypeName { get => _typeName; set { if (value == _typeName) return; _typeName = value; OnPropertyChanged(); } }

        private bool _themeIsChecked;
        public bool ThemeIsChecked { get => _themeIsChecked; set { if (value == _themeIsChecked) return; _themeIsChecked = value; OnPropertyChanged(); } }

        private string _themeName;
        public string ThemeName { get => _themeName; set { if (value == _themeName) return; _themeName = value; OnPropertyChanged(); } }


        #endregion

        public ICommand LoadCommand { get; set; }
        public ICommand TypeSelectionChanged { get; set; }

        public ICommand editTypeCommand { get; set; }
        public ICommand deleteTypeCommand { get; set; }
        public ICommand restoreTypeCommand { get; set; }

        public ICommand TypeCheckedCommand { get; set; }

        public ICommand addTypeCommand { get; set; }

        public ICommand editThemeCommand { get; set; }
        public ICommand deleteThemeCommand { get; set; }
        public ICommand restoreThemeCommand { get; set; }

        public ICommand ThemeCheckedCommand { get; set; }

        public ICommand addThemeCommand { get; set; }

        #region command binding

        #endregion

        public TypeAndThemePageVM()
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                TypeIsChecked = false;
                ThemeIsChecked = false;
                ListType = new ObservableCollection<CBookType>(CBook.Ins.ListFullType(TypeIsChecked));
                CurrentType = "";
            }
              );

            ThemeCheckedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListTheme = new ObservableCollection<CBookTheme>(CBook.Ins.ListFullThemeOfType(ThemeIsChecked, TypeSelectedItem.ID));
            }
              );

            addThemeCommand = new RelayCommand<object>((p) => {
                if (string.IsNullOrEmpty(ThemeName))
                {
                    return false;
                }
                if (CBook.Ins.isTheme(ThemeName) != 0)
                    return false;

                return true;
            }, (p) =>
            {
                if (TypeSelectedItem != null)
                {
                    if (CBook.Ins.addTheme(ThemeName,TypeSelectedItem.ID) == true)
                    {
                        MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        ListTheme = new ObservableCollection<CBookTheme>(CBook.Ins.ListFullThemeOfType(ThemeIsChecked, TypeSelectedItem.ID));
                    }
                    else
                    {
                        MessageBox.Show("Thêm thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
              );

            deleteThemeCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (ThemeSelectedItem != null && TypeSelectedItem != null)
                {
                    if (ThemeSelectedItem.Count == 0)
                    {
                        if (CBook.Ins.deleteTheme(ThemeSelectedItem) == true)
                        {
                            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            //load lại
                            ListTheme = new ObservableCollection<CBookTheme>(CBook.Ins.ListFullThemeOfType(ThemeIsChecked, TypeSelectedItem.ID));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa chủ đề đang dùng", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                }
            }
              );

            restoreThemeCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (ThemeSelectedItem != null && TypeSelectedItem != null)
                {
                    if (CBook.Ins.restoreTheme(ThemeSelectedItem) == true)
                    {
                        MessageBox.Show("Khôi phục thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        ListTheme = new ObservableCollection<CBookTheme>(CBook.Ins.ListFullThemeOfType(ThemeIsChecked, TypeSelectedItem.ID));
                    }
                }
            }
              );

            editThemeCommand = new RelayCommand<object>((p) => {
                if (ThemeSelectedItem != null)
                {
                    if (string.IsNullOrEmpty(ThemeSelectedItem.Name))
                    {
                        ThemeSelectedItem.IsTrueValue = false;

                    }
                    else
                    {
                        if (CBook.Ins.isTheme(ThemeSelectedItem.Name) != ThemeSelectedItem.ID && CBook.Ins.isTheme(ThemeSelectedItem.Name) != 0)
                        {
                            ThemeSelectedItem.IsTrueValue = false;
                        }
                        else
                        {
                            ThemeSelectedItem.IsTrueValue = true;
                        }
                    }
                }
                return true;
            }, (p) =>
            {
                if (ThemeSelectedItem != null && TypeSelectedItem!=null)
                {
                    if (CBook.Ins.updateTheme(ThemeSelectedItem) == true)
                    {
                        MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);

                        //Load lại thông tin cho list
                        ListTheme = new ObservableCollection<CBookTheme>(CBook.Ins.ListFullThemeOfType(ThemeIsChecked, TypeSelectedItem.ID));
                    }

                }
            }
              );


            addTypeCommand = new RelayCommand<object>((p) => {
                if (string.IsNullOrEmpty(TypeName))
                {
                    return false;
                }
                if (CBook.Ins.isType(TypeName) != 0)
                    return false;

                return true;
            }, (p) =>
            {
                if (CBook.Ins.addType(TypeName) == true)
                {
                    MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    ListType = new ObservableCollection<CBookType>(CBook.Ins.ListFullType(TypeIsChecked));
                }
                else
                {
                    MessageBox.Show("Thêm thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                         
            }
              );

            TypeCheckedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListType = new ObservableCollection<CBookType>(CBook.Ins.ListFullType(TypeIsChecked));
            }
              );

            restoreTypeCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (TypeSelectedItem != null)
                {
                    if (CBook.Ins.restoreType(TypeSelectedItem) == true)
                    {
                        MessageBox.Show("Khôi phục thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        ListType = new ObservableCollection<CBookType>(CBook.Ins.ListFullType(TypeIsChecked));
                    }
                }
            }
              );

            deleteTypeCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (TypeSelectedItem != null)
                {
                    if (TypeSelectedItem.Count == 0)
                    {
                        if (CBook.Ins.deleteType(TypeSelectedItem) == true)
                        {
                            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            //load lại
                            ListType = new ObservableCollection<CBookType>(CBook.Ins.ListFullType(TypeIsChecked));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa thể loại đang dùng", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                }
            }
              );

            editTypeCommand = new RelayCommand<object>((p) => {
                if (TypeSelectedItem != null)
                {
                    if (string.IsNullOrEmpty(TypeSelectedItem.Name))
                    {
                        TypeSelectedItem.IsTrueValue = false;
                        
                    }
                    else
                    {
                        if(CBook.Ins.isType(TypeSelectedItem.Name) !=TypeSelectedItem.ID && CBook.Ins.isType(TypeSelectedItem.Name) != 0)
                        {
                            TypeSelectedItem.IsTrueValue = false;
                        }
                        else
                        {
                            TypeSelectedItem.IsTrueValue = true;
                        }
                    }
                }
                return true;
            }, (p) =>
            {
                if (TypeSelectedItem != null)
                {
                    if (CBook.Ins.updateType(TypeSelectedItem) == true)
                    {
                        MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);

                        //Load lại thông tin cho list
                        ListType = new ObservableCollection<CBookType>(CBook.Ins.ListFullType(TypeIsChecked));
                    }
                    
                }
            }
              );

            TypeSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (TypeSelectedItem != null)
                {
                    ListTheme = new ObservableCollection<CBookTheme>(CBook.Ins.ListFullThemeOfType(ThemeIsChecked, TypeSelectedItem.ID));
                    CurrentType = TypeSelectedItem.Name;
                }
            }
             );
        }

    }
}
