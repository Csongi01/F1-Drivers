using J21LMC_HFT_2021222.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace J21LMC_GUI_20222023.WPF
{
    public class RaceViewModel : ObservableRecipient
    {
        public RestCollection<Race> Races { get; set; }

        public ICommand CreateRaceCommand { get; set; }
        public ICommand DeleteRaceCommand { get; set; }
        public ICommand UpdateRaceCommand { get; set; }

        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Race> race { get; set; }

        private Race selectedRace;

        public Race SelectedRace
        {
            get { return selectedRace; }
            set
            {
                if (value != null)
                {
                    selectedRace = new Race()
                    {
                        race_code = value.race_code,
                        racename = value.racename,
                        location = value.location
                    };
                    OnPropertyChanged();
                    (UpdateRaceCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        static class Helper
        {
            public static Random rnd = new Random();
        }

        public RaceViewModel()
        {
            if (!IsInDesignMode)
            {
                Races = new RestCollection<Race>("http://localhost:53910/", "race", "hub");
                CreateRaceCommand = new RelayCommand(() =>
                {
                    DateTime startDate = new DateTime(2021, 1, 1);
                    DateTime endDate = new DateTime(2023, 12, 31);
                    int totalDays = (endDate - startDate).Days;
                    int randomDays = Helper.rnd.Next(totalDays);
                  
                    Races.Add(new Race()
                    {
                        race_code = SelectedRace.race_code,
                        racename = SelectedRace.racename,
                        location = SelectedRace.location,
                        laps = Helper.rnd.Next(50, 71),
                        length = Helper.rnd.Next(5000,7001),
                        date = startDate.AddDays(randomDays)

                });
                });

                UpdateRaceCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Races.Update(SelectedRace);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteRaceCommand = new RelayCommand(() =>
                {

                    Races.Delete(selectedRace.race_code);
                },
                () =>
                {
                    return SelectedRace != null;
                });
                SelectedRace = new Race();
            }
        }
    }
}

