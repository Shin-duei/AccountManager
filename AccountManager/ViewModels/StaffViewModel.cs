using AccountManager.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.ViewModels
{
    public class StaffViewModel : ObservableRecipient
    {
        /// <summary>
        /// 顯示用列表
        /// </summary>
        public ObservableCollection<StaffModel> StaffListDisplay { set; get; } = new ObservableCollection<StaffModel>();

        public string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;

                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public string _alias;
        public string Alias
        {
            get { return _alias; }
            set
            {
                if (_alias != value)
                {
                    _alias = value;

                    OnPropertyChanged(nameof(Alias));
                }
            }
        }
        public uint _id;
        public uint ID
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;

                    OnPropertyChanged(nameof(ID));
                }
            }
        }
        public string _position;
        public string Position
        {
            get { return _position; }
            set
            {
                if (_position != value)
                {
                    _position = value;

                    OnPropertyChanged(nameof(Position));
                }
            }
        }
        public string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;

                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public StaffViewModel()
        {
            AddNewStaffCommand = new RelayCommand(ExecuteAddNewStaffCommand);

        }
        public RelayCommand AddNewStaffCommand { get; }

        private void ExecuteAddNewStaffCommand()
        {
            var newStaff=new StaffModel()
            {
                Name = Name,
                Alias = Alias,
                ID = ID,
                Position = Position,
                Password= Password,
            };
            StaffListDisplay.Add(newStaff);
        }
    }
}
