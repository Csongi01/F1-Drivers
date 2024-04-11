using J21LMC_HFT_2021222.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using J21LMC_HFT_2021222.Logic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static J21LMC_HFT_2021222.Logic.PilotLogic;
using static J21LMC_HFT_2021222.Logic.ResultLogic;

namespace J21LMC_GUI_20222023.WPF
{
    public class NonCrudViewModel: ObservableRecipient
    {
        public RestCollection<Pilot> Pilots { get; set; }
        public RestCollection<Team> Teams { get; set; }
        public  IEnumerable<PilotTeamInfo> PTF { get; set; }
        public IEnumerable<Top2YoungestPilots> TOP2 { get; set; }
        public IEnumerable<MogyorodResults> Mogyorod { get; set; }
        public IEnumerable<AveragefinishPos> AVG { get; set; }
        public IEnumerable<BestPilot> Best { get; set; }
        public List<Tuple<string, string>> PTF_out = new List<Tuple<string, string>>();
        public List<Tuple<string, string>> TOP2_out = new List<Tuple<string, string>>();
        public List<Tuple<string, string>> Best_out = new List<Tuple<string, string>>();
        public List<Tuple<string, string, string>> Mogyorod_out = new List<Tuple<string, string, string>>();
        public List<Tuple<string, string>> AVG_out = new List<Tuple<string, string>>();

        static RestService rest;


        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public NonCrudViewModel()
        {
            if (!IsInDesignMode)
            {

                rest = new RestService("http://localhost:53910/");

                PTF = rest.Get<PilotTeamInfo>("Noncrud/PilotTeamInfo");
                TOP2 = rest.Get<Top2YoungestPilots>("Noncrud/Top2_YoungestPilots");
                Best = rest.Get<BestPilot>("Noncrud/Best_Pilot");
                Mogyorod = rest.Get<MogyorodResults>("Noncrud/Mogyorod_Results");
                AVG = rest.Get<AveragefinishPos>("/Noncrud/AveragefinishPos");

                
                foreach (var item in PTF)
                {
                    PTF_out.Add(Tuple.Create(item.TeamName, item.PilotNum.ToString()));
                }               
                foreach (var item in TOP2)
                {
                    TOP2_out.Add(Tuple.Create(item.PilotName, item.Dateofbirth.ToString()));
                }
                foreach (var item in Best)
                {
                    Best_out.Add(Tuple.Create(item.Pilot_Name, item.Wins.ToString()));
                }
                foreach (var item in Mogyorod)
                {
                    string nev = item.Race_name;
                    string start = item.Start_pos.ToString();
                    string finish = item.Finish_pos.ToString();
                    var tuple = new Tuple<string, string, string>(nev, start, finish);
                    Mogyorod_out.Add(tuple);
                }
                foreach (var item in AVG)
                {
                    AVG_out.Add(Tuple.Create(item.Pilot_name, item.averageFinishPos.ToString()));
                }



            }
        }
        
        
    }
}
