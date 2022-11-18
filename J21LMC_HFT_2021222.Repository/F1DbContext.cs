using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J21LMC_HFT_2021222.Models;
using Microsoft.EntityFrameworkCore;

namespace J21LMC_HFT_2021222.Repository
{  
    public partial class F1DbContext : DbContext
    {

        public virtual DbSet<Pilot> Pilots { get; set; }
        public virtual DbSet<Race> Races { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Team> Teams { get; set; }


        public F1DbContext()
        {
            this.Database.EnsureCreated();
        }

        public F1DbContext(DbContextOptions<F1DbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseInMemoryDatabase("f1").UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Navigation propertyk összekötése. */

            modelBuilder.Entity<Pilot>(entity =>
            {
                entity.HasOne(Pilot => Pilot.Team)
                .WithMany(team => team.Pilots)
                .HasForeignKey(pilot => pilot.team_id).OnDelete(DeleteBehavior.ClientSetNull);

            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.HasOne(result => result.Pilot)
                .WithMany(pilot => pilot.Results)
                .HasForeignKey(result => result.pilot_Id).OnDelete(DeleteBehavior.ClientSetNull);

            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.HasOne(result => result.Race)
                .WithMany(race => race.Results)
                .HasForeignKey(result => result.race_code).OnDelete(DeleteBehavior.ClientSetNull);

            });


            modelBuilder.Entity<Result>().HasKey(result => new { result.pilot_Id, result.race_code });


            #region pilots
            Pilot[] pilots = new Pilot[] {
            new Pilot() { pilot_Id = 1, Name = "Alexander Rossi", dateofbirth = 1991, team_id = 4 },
            new Pilot() { pilot_Id = 2, Name = "Carlos Sainz Jnr", dateofbirth = 1994, team_id = 9 },
            new Pilot() { pilot_Id = 3, Name = "Daniel Ricciardo", dateofbirth = 1989, team_id = 7 },
            new Pilot() { pilot_Id = 4, Name = "Daniil Kvyat", dateofbirth = 1981, team_id = 10 },
            new Pilot() { pilot_Id = 5, Name = "Felipe Massa", dateofbirth = 1981, team_id = 10 },
            new Pilot() { pilot_Id = 6, Name = "Fernando Alonso", dateofbirth = 1980, team_id = 5 },
            new Pilot() { pilot_Id = 7, Name = "Jenson Button", dateofbirth = 1980, team_id = 5 },
            new Pilot() { pilot_Id = 8, Name = "Max Verstappen", dateofbirth = 1997, team_id = 9 },
            new Pilot() { pilot_Id = 10, Name = "Sebastian Vettel", dateofbirth = 1987, team_id = 1 },
            new Pilot() { pilot_Id = 11, Name = "Valtteri Bottas", dateofbirth = 1989, team_id = 10 },
            new Pilot() { pilot_Id = 12, Name = "Lewis Hamilton", dateofbirth = 1985, team_id = 6 }
            };
            #endregion
            #region races
            Race[] races = new Race[] {
            new Race() { race_code = "AUS", date = DateTime.Parse("2021-03-15"), racename = "Ausztrál Nagydíj", location = "Melbourne", laps = 58, length = 5303 },
            new Race() { race_code = "CHI", date = DateTime.Parse("2021-04-12"), racename = "Kínai Nagydíj", location = "Shanghai", laps = 56, length = 5451 },
            new Race() { race_code = "BAH", date = DateTime.Parse("2021-04-19"), racename = "Bahreini Nagydíj", location = "Szahír", laps = 57, length = 5412 },
            new Race() { race_code = "SPA", date = DateTime.Parse("2021-05-15"), racename = "Spanyol Nagydíj", location = "Barcelona", laps = 66, length = 4655 },
            new Race() { race_code = "MON", date = DateTime.Parse("2021-05-24"), racename = "Monacói Nagydíj", location = "Montreal", laps = 70, length = 4361 },
            new Race() { race_code = "GER", date = DateTime.Parse("2021-07-19"), racename = "Német Nagydíj", location = "TBD", laps = 67, length = 4574 },
            new Race() { race_code = "HUN", date = DateTime.Parse("2021-07-26"), racename = "Magyar Nagydíj", location = "Mogyoród", laps = 70, length = 4381 },
            new Race() { race_code = "BEL", date = DateTime.Parse("2021-08-23"), racename = "Belga Nagydíj", location = "Francorchamps", laps = 44, length = 7004 },
            new Race() { race_code = "ITA", date = DateTime.Parse("2021-09-06"), racename = "Olasz Nagydíj", location = "Monza", laps = 53, length = 5793 }
            };
            #endregion
            #region teams
            Team[] teames = new Team[] {
            new Team() { team_id = 1, team_name = "Ferrari" },
            new Team() { team_id = 2, team_name = "Force India Mercedes" },
            new Team() { team_id = 3, team_name = "Lotus Mercedes" },
            new Team() { team_id = 4, team_name = "Marussia Ferrari" },
            new Team() { team_id = 5, team_name = "McLaren Honda" },
            new Team() { team_id = 6, team_name = "Mercedes" },
            new Team() { team_id = 7, team_name = "Red Bull Renault" },
            new Team() { team_id = 8, team_name = "Sauber Ferrari" },
            new Team() { team_id = 9, team_name = "STR Renault" },
            new Team() { team_id = 10, team_name = "Williams Mercedes" }
            };
            #endregion
            #region results
            Result[] results = new Result[] {
            new Result() { result_Id=1, pilot_Id = 1, race_code = "AUS", finish_pos = null, start_pos = null },
            new Result() { result_Id=2,pilot_Id = 1, race_code = "BAH", finish_pos = 2, start_pos = 3 },
            new Result() { result_Id=3,pilot_Id = 1, race_code = "GER", finish_pos = 2, start_pos = 5 },

            new Result() {result_Id=4, pilot_Id = 2, race_code = "MON", finish_pos = null, start_pos = 6 },
            new Result() {result_Id=5, pilot_Id = 2, race_code = "BAH", finish_pos = 12, start_pos = 12 },
            new Result() {result_Id=6, pilot_Id = 2, race_code = "ITA", finish_pos = 7, start_pos = 7 },


            new Result() {result_Id=7, pilot_Id = 3, race_code = "HUN", finish_pos = 1, start_pos = 8 },
            new Result() {result_Id=8,  pilot_Id = 3, race_code = "BAH", finish_pos = 6, start_pos = null },
            new Result() {result_Id=9, pilot_Id = 3, race_code = "ITA", finish_pos = 3, start_pos = 3 },


            new Result() {result_Id=10, pilot_Id = 4, race_code = "BEL", finish_pos = 5, start_pos = 6 },
            new Result() {result_Id=11, pilot_Id = 4, race_code = "BAH", finish_pos = null, start_pos = 4 },
            new Result() {result_Id=12, pilot_Id = 4, race_code = "SPA", finish_pos = 11, start_pos = 2 },


            new Result() {result_Id=13, pilot_Id = 5, race_code = "AUS", finish_pos = 2, start_pos = null },
            new Result() {result_Id=14, pilot_Id = 5, race_code = "CHI", finish_pos = 3, start_pos = null },
            new Result() {result_Id=15, pilot_Id = 5, race_code = "GER", finish_pos = null, start_pos = 12 },


            new Result() {result_Id=16, pilot_Id = 6, race_code = "AUS", finish_pos = 9, start_pos = null },
            new Result() {result_Id=17, pilot_Id = 6, race_code = "BAH", finish_pos = 14, start_pos = 7 },



            new Result() {result_Id=18, pilot_Id = 7, race_code = "SPA", finish_pos = 10, start_pos = 14 },
            new Result() {result_Id=19, pilot_Id = 7, race_code = "HUN", finish_pos = 5, start_pos = 3 },
            new Result() {result_Id=20, pilot_Id = 7, race_code = "GER", finish_pos = 6, start_pos = 9 },


            new Result() {result_Id=21, pilot_Id = 8, race_code = "AUS", finish_pos = 6, start_pos = 9 },
            new Result() {result_Id=22, pilot_Id = 8, race_code = "SPA", finish_pos = 7, start_pos = 2 },
            new Result() {result_Id=23, pilot_Id = 8, race_code = "MON", finish_pos = null, start_pos = 5 },



            new Result() {result_Id=24, pilot_Id = 12, race_code = "HUN", finish_pos = 1, start_pos = 4 },
            new Result() {result_Id=25, pilot_Id = 12, race_code = "GER", finish_pos = 1, start_pos = 2 },
            new Result() {result_Id=26, pilot_Id = 12, race_code = "MON", finish_pos = 1, start_pos = 3 }
            };

            #endregion

            modelBuilder.Entity<Pilot>().HasData(pilots);
            modelBuilder.Entity<Race>().HasData(races);
            modelBuilder.Entity<Team>().HasData(teames);
            modelBuilder.Entity<Result>().HasData(results);
        }
    }
}
