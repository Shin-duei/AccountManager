using AccountManager.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.ViewModels
{
    public class StaffDataGridViewModel : ObservableRecipient
    {

        public StaffDataGridViewModel(StaffModel staffModel)
        {
            Alias = staffModel.Alias;
            ID = staffModel.ID;
            Name = staffModel.Name;
            Position = staffModel.Position;
        }

        private string _name;
        /// <summary>
        /// 姓名
        /// </summary>
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
        private string _alias;
        /// <summary>
        /// 別名
        /// </summary>
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
        private string _id;
        /// <summary>
        /// 員工編號
        /// </summary>
        public string ID
        {
            get { return ID; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }
        private string _position;
        /// <summary>
        /// 職稱
        /// </summary>
        public string Position
        {
            get { return _position; }
            set
            {
                if (_position != value)
                {
                    _position = value;
                    OnPropertyChanged(nameof(_position));
                }
            }
        }
    }
}
