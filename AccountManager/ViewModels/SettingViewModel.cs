using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.ViewModels
{
    public class SettingViewModel : ObservableRecipient
    {
        public string _id;
        public string ID
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
        public SettingViewModel()
        {
           
        }
    }
}
