using QuickAccessTrail.Helper;
using QuickAccessTrail.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickAccessTrail.ViewModels
{
    public class FloorTypeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private List<FloorTypeItem> _allFloorType;
        public ObservableCollection<FloorTypeItem> FilteredFloors { get; set; }
        public ObservableCollection<string> FilteredLevels { get; set; }
        public FloorTypeViewModel(List<FloorTypeItem> types)
        {
            _allFloorType = types;
            FilteredFloors = new ObservableCollection<FloorTypeItem>(types);
            var levels = types.Select(x => x.LevelName).Distinct().OrderBy(x => x).ToList();
            levels.Insert(0, "All Levels");
            FilteredLevels = new ObservableCollection<string>(levels);
            _selectedLevel = "All Levels";
            OnPropertyChanged(nameof(SelectedLevel));
            UpdateStatus();


        }
        private string _selectedLevel;
        public string SelectedLevel
        {
            get { return _selectedLevel; }
            set
            {
                _selectedLevel = value;
                OnPropertyChanged(nameof(SelectedLevel));
                ApplyFilter();
            }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        private string _floorArea;
        public string FloorArea
        {
            get { return _floorArea; }
            set
            {
                _floorArea = value;
                OnPropertyChanged(nameof(FloorArea));
            }
        }

        private FloorTypeItem _selectedFloor;
        public FloorTypeItem SelectedFloor
        {
            get { return _selectedFloor; }
            set
            {
                _selectedFloor = value;
                OnPropertyChanged(nameof(SelectedFloor));
                UpdateArea();

            }
        }

        private void UpdateArea()
        {
            if (SelectedFloor == null)
            {
                FloorArea = string.Empty;
                return;
            }

            var area = UnitConversionSqM.ToSqM(SelectedFloor.FloorArea);

            FloorArea = $"Selected Floor Area = {area:F2} m²";
        }

        private void ApplyFilter()  
        {
            FilteredFloors.Clear();
            var filter = SelectedLevel == "All Levels" ? _allFloorType : _allFloorType.Where(x => x.LevelName == SelectedLevel).ToList();
            foreach (var  floorTy in filter)
            {
                FilteredFloors.Add(floorTy);
            }
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            Status = $"Total Floor = {FilteredFloors.Count.ToString()} loaded";
        }

        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
