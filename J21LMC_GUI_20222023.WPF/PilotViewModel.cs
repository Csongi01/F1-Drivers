using J21LMC_GUI_20222023.WPF;
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
    public class PilotViewModel: ObservableRecipient
    {
        public RestCollection<Pilot> Pilots { get; set; }

        public ICommand CreatePilotCommand { get; set; }
        public ICommand DeletePilotCommand { get; set; }
        public ICommand UpdatePilotCommand { get; set; }

        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Pilot> pilot { get; set; }

        private Pilot selectedPilot;

        public Pilot SelectedPilot
        {
            get { return selectedPilot; }
            set
            {
                if (value != null)
                {
                    selectedPilot = new Pilot()
                    {

                        pilot_Id = value.pilot_Id,
                        Name = value.Name,
                        dateofbirth = value.dateofbirth

                    };
                    OnPropertyChanged();
                    (UpdatePilotCommand as RelayCommand).NotifyCanExecuteChanged();
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
        static class Util
        {
            public static Random rnd = new Random(); 
        }

        public PilotViewModel()
        {
            if (!IsInDesignMode)
            {
                Pilots = new RestCollection<Pilot>("http://localhost:53910/", "pilot", "hub");
                CreatePilotCommand = new RelayCommand(() =>
                {
                    Pilots.Add(new Pilot()
                    {
                        Name = SelectedPilot.Name,
                        dateofbirth = SelectedPilot.dateofbirth,
                        team_id = Util.rnd.Next(1, 12)

                    });
                });

                UpdatePilotCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Pilots.Update(SelectedPilot);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeletePilotCommand = new RelayCommand(() =>
                {
                    Pilots.Delete(SelectedPilot.pilot_Id.ToString());
                },
                () =>
                {
                    return SelectedPilot != null;
                });
                SelectedPilot = new Pilot();
            }
        }        
    }
}
