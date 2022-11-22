using ConsoleTools;
using System;
using System.Linq;
using J21LMC_HFT_2021222.Models;
using System.Collections.Generic;
using System.Reflection;

namespace J21LMC_HFT_2021222.Client
{
    static class MyExtension
    {
        public static void ToConsole<T>(this IEnumerable<T> input, string header)
        {
            Console.WriteLine($"************ {header} ************");
            foreach (T item in input)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine($"**********************************");
            Console.ReadLine();
        }
    }
    public class Program
    {
        static RestService rest;
        static void Create(string entity)
        {
            if (entity == "Pilot")
            {
                Console.Write("Enter Pilot Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Pilot's date of birth (int): ");
                int dateofbirth = int.Parse(Console.ReadLine());
                Console.Write("Enter Pilot's team id: ");
                int team_id = int.Parse(Console.ReadLine());
                rest.Post(new Pilot() { Name = name, dateofbirth = dateofbirth, team_id = team_id }, "pilot");
            }
            if (entity == "Race")
            {
                Console.Write("Enter Race code: ");
                string race_code = Console.ReadLine();
                Console.Write("Enter Race's date (yyyy-mm-dd): ");
                DateTime date = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter Racename: ");
                string racename = Console.ReadLine();
                Console.Write("Enter Race's location: ");
                string location = Console.ReadLine();
                Console.Write("Enter Race's laps: ");
                int? laps = int.Parse(Console.ReadLine());
                Console.Write("Enter Race's length: ");
                int? length = int.Parse(Console.ReadLine());
                rest.Post(new Race()
                {
                    race_code = race_code,
                    date = date,
                    racename = racename,
                    location = location,
                    laps = laps,
                    length = length
                }, "race");
            }
            if (entity == "Result")
            {
                Console.Write("Enter Result's pilotid: ");
                int pilot_id = int.Parse(Console.ReadLine());
                Console.Write("Enter Result's racecode: ");
                string racecode = Console.ReadLine();
                Console.Write("Enter Result's start postition: ");
                int? startpos = int.Parse(Console.ReadLine());
                Console.Write("Enter Result's finsih positon: ");
                int? finishpos = int.Parse(Console.ReadLine());
                rest.Post(new Result()
                {
                    pilot_Id = pilot_id,
                    race_code = racecode,
                    start_pos = startpos,
                    finish_pos = finishpos
                }, "result");
            }
            if (entity == "Team")
            {
                Console.Write("Enter Team's name: ");
                string teamname = Console.ReadLine();
                rest.Post(new Team() { team_name = teamname }, "team");
            }
        }
        static void List(string entity)
        {
            if (entity == "Pilot")
            {
                List<Pilot> pilots = rest.Get<Pilot>("pilot");
                foreach (var item in pilots)
                {
                    Console.WriteLine(item.pilot_Id + ": " + item.Name + ": " + item.team_id);
                }
            }
            if (entity == "Team")
            {
                List<Team> teams = rest.Get<Team>("team");
                foreach (var item in teams)
                {
                    Console.WriteLine(item.team_id + ": " + item.team_name);
                }
            }
            if (entity == "Race")
            {
                List<Race> races = rest.Get<Race>("race");
                foreach (var item in races)
                {
                    Console.WriteLine(item.race_code + ": " + item.racename + ": " + item.date + ": " + item.length + ": " + item.laps);
                }
            }          

            if (entity == "Result")
            {
                List<Result> results = rest.Get<Result>("result");                 

                foreach (var item in results)
                {
                    Console.WriteLine(item.result_Id + ": " + item.race_code + ": " + item.pilot_Id + ": " + item.start_pos + ": " + item.finish_pos);
                }
            }
            Console.ReadLine();

        }
        static void Listnoncrud(string entity)
        {
            if (entity == "PilotTeamInfo")
            {
                List<object> pilot_teaminfo = rest.Get<List<object>>("PilotTeamInfo", "Noncrud");
                pilot_teaminfo.ToConsole("Pilot team info: ");
            }
            if (entity == "Top2YoungestPilots")
            {
                List<object> Top2YoungestPilots = rest.Get<List<object>>("Top2_YoungestPilots", "Noncrud");
                Top2YoungestPilots.ToConsole("Top2 youngest pilots: ");
            }
            if (entity == "BestPilot")
            {
                List<object> Best_Pilot = rest.Get<List<object>>("Best_Pilot", "Noncrud");
                Best_Pilot.ToConsole("Best pilot: ");
            }
            if (entity == "MogyorodResults")
            {
                List<object> Mogyorod_Results = rest.Get<List<object>>("Mogyorod_Results", "Noncrud");
                Mogyorod_Results.ToConsole("Mogyorod results: ");
            }
            if (entity == "AveragefinishPos")
            {
                List<object> pilot_teaminfo = rest.Get<List<object>>("AveragefinishPos", "Noncrud");
                pilot_teaminfo.ToConsole("Average finish posistion: ");
            }
        }
        static void Update(string entity)
        {
            if (entity == "Pilot")
            {
                Console.Write("Enter Pilot's id to update: ");
                string id = Console.ReadLine();
                Pilot one = rest.Get<Pilot>(id, "pilot");
                Console.Write($"New name [old: {one.Name}]: ");
                string name = Console.ReadLine();
                one.Name = name;
                rest.Put(one, "pilot");
            }
            if (entity == "Race")
            {
                Console.Write("Enter Race's code to update: ");
                string id = Console.ReadLine();
                Race one = rest.Get<Race>(id, "race");
                Console.Write($"New racecode [old: {one.racename}]: ");
                string racename = Console.ReadLine();
                one.racename = racename;
                rest.Put(one, "race");
            }
            if (entity == "Result")
            {
                Console.Write("Enter Result's id to update: ");
                string id = Console.ReadLine();
                Result one = rest.Get<Result>(id, "result");
                Console.Write($"New result finis position [old: {one.finish_pos}]: ");
                int finish_pos = int.Parse(Console.ReadLine());
                one.finish_pos = finish_pos;
                rest.Put(one, "result");
            }
            if (entity == "Team")
            {
                Console.Write("Enter Team's id to update: ");
                string id = Console.ReadLine();
                Team one = rest.Get<Team>(id, "team");
                Console.Write($"New team name [old: {one.team_name}]: ");
                string team_name = Console.ReadLine();
                one.team_name = team_name;
                rest.Put(one, "team");
            }
        }
        static void Delete(string entity)
        {
            if (entity == "Pilot")
            {
                Console.Write("Enter Pilot's id to delete: ");
                string id = Console.ReadLine();
                rest.Delete((id), "pilot");
            }
            if (entity == "Race")
            {
                Console.Write("Enter Race's code to delete: ");
                string id = Console.ReadLine();
                rest.Delete(id, "race");
            }
            if (entity == "Result")
            {
                Console.Write("Enter Result's id to delete: ");
                string id = Console.ReadLine();
                rest.Delete((id), "result");
            }
            if (entity == "Team")
            {
                Console.Write("Enter Team's id to delete: ");
                string id = Console.ReadLine();
                rest.Delete((id), "team");
            }
        }
        static void Main(string[] args)
        {           

            rest = new RestService("http://localhost:53910/");

            var pilotSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Pilot"))
                .Add("Create", () => Create("Pilot"))
                .Add("Delete", () => Delete("Pilot"))
                .Add("Update", () => Update("Pilot"))
                .Add("Exit", ConsoleMenu.Close);

            var raceSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Race"))
                .Add("Create", () => Create("Race"))
                .Add("Delete", () => Delete("Race"))
                .Add("Update", () => Update("Race"))
                .Add("Exit", ConsoleMenu.Close);

            var resultSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Result"))
                .Add("Create", () => Create("Result"))
                .Add("Delete", () => Delete("Result"))
                .Add("Update", () => Update("Result"))
                .Add("Exit", ConsoleMenu.Close);

            var teamSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Team"))
                .Add("Create", () => Create("Team"))
                .Add("Delete", () => Delete("Team"))
                .Add("Update", () => Update("Team"))
                .Add("Exit", ConsoleMenu.Close);

            var noncrudsubmenu = new ConsoleMenu(args, level: 1)
               .Add("PilotTeamInfo", () => Listnoncrud("PilotTeamInfo"))
               .Add("Top2YoungestPilots", () => Listnoncrud("Top2YoungestPilots"))
               .Add("BestPilot", () => Listnoncrud("BestPilot"))
               .Add("MogyorodResults", () => Listnoncrud("MogyorodResults"))
               .Add("AveragefinishPos", () => Listnoncrud("AveragefinishPos"))               
               .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Pilot", () => pilotSubMenu.Show())
                .Add("Race", () => raceSubMenu.Show())
                .Add("Result", () => resultSubMenu.Show())
                .Add("Team", () => teamSubMenu.Show())
                .Add("Noncrud", () => noncrudsubmenu.Show())
                .Add("Exit", ConsoleMenu.Close);
            menu.Show();
        }

    }
}
