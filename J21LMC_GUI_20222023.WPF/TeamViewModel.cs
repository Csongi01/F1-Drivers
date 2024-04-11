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
    internal class TeamViewModel: ObservableRecipient
    {
      
        public RestCollection<Team> Teams { get; set; }

        public ICommand CreateTeamCommand { get; set; }
        public ICommand DeleteTeamCommand { get; set; }
        public ICommand UpdateTeamCommand { get; set; }

        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Team> team { get; set; }

        private Team selectedTeam;

        public Team SelectedTeam
        {
            get { return selectedTeam; }
            set
            {
                if (value != null)
                {
                    selectedTeam = new Team()
                    {
                        team_id = value.team_id,
                        team_name = value.team_name,                        
                    };
                    OnPropertyChanged();
                    (UpdateTeamCommand as RelayCommand).NotifyCanExecuteChanged();
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
        public TeamViewModel()
        {
            if (!IsInDesignMode)
            {
                Teams = new RestCollection<Team>("http://localhost:53910/", "team", "hub");
                CreateTeamCommand = new RelayCommand(() =>
                {
                    if (selectedTeam != null)
                    {
                        Teams.Add(new Team()
                        {
                            team_name = SelectedTeam.team_name
                        });
                    }
                });

                UpdateTeamCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Teams.Update(SelectedTeam);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteTeamCommand = new RelayCommand(() =>
                {
                    Teams.Delete(SelectedTeam.team_id.ToString());
                },
                () =>
                {
                    return SelectedTeam != null;
                });
                SelectedTeam = new Team();
            }
        }
    }
}
