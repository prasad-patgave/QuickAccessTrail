using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAccessTrail.Model
{
    public class FloorTypeItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _floorName;
        public string FloorName
        {
            get { return _floorName; }
            set
            {
                _floorName = value;
                OnPropertyChanged(nameof(FloorName));
            }
        }

        private string _levelName;
        public string LevelName
        {
            get { return _levelName; }
            set
            {
                _levelName = value;
                OnPropertyChanged(nameof(LevelName));
            }
        }

        private double _floorArea;
        public double FloorArea
        {
            get { return _floorArea; }
            set
            {
                _floorArea = value;
                OnPropertyChanged(nameof(FloorArea));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
