using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveCharts;
using LiveCharts.Wpf;
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
            SeriesCollection = new SeriesCollection()
            {
                new LineSeries{ Values=new ChartValues<double>{3,5,7,4 } },
                new ColumnSeries{ Values=new ChartValues<double>{5,6,2,7 } }
            };
        }

        public SeriesCollection SeriesCollection { set; get; }
    }
}
