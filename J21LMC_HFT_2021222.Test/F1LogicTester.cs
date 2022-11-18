using J21LMC_HFT_2021222.Logic;
using J21LMC_HFT_2021222.Models;
using J21LMC_HFT_2021222.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static J21LMC_HFT_2021222.Logic.PilotLogic;
using static J21LMC_HFT_2021222.Logic.ResultLogic;

namespace J21LMC_HFT_20212022.Test
{
    [TestFixture]
    public class F1LogicTester
    {
        #region instances
        Mock<IPilotRepository> mockPilotRepository;
        Mock<IRaceRepository> mockRaceRepository;

        PilotLogic pilot_logic;
        PilotLogic pilot_logic_mock;
        RaceLogic race_logic;
        RaceLogic race_logic_mock;
        ResultLogic result_logic;
        TeamLogic team_logic;

        IRepository<Team> team_repo;
        IRepository<Pilot> pilot_repo;
        IRepository<Race> race_repo;
        IRepository<Result> result_repo;

        public class FakeTeamRepo : IRepository<Team>


        {
            public void Create(Team item)
            {
                throw new NotImplementedException();
            }

            public void Delete(string id)
            {
                throw new NotImplementedException();
            }

            public void Update(Team item)
            {
                throw new NotImplementedException();
            }

            Team IRepository<Team>.Read(string id)
            {
                throw new NotImplementedException();
            }

            IQueryable<Team> IRepository<Team>.ReadAll()
            {
                return new List<Team>()
                {
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
            }.AsQueryable();
            }
        }
        public class FakePilotRepo : IRepository<Pilot>
        {
            public void Create(Pilot item)
            {
                throw new NotImplementedException();
            }

            public void Delete(string id)
            {
                throw new NotImplementedException();
            }

            public Pilot Read(string id)
            {
                throw new NotImplementedException();
            }

            public IQueryable<Pilot> ReadAll()
            {
                return new List<Pilot>()

                {
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
            }.AsQueryable();

            }

            public void Update(Pilot item)
            {
                throw new NotImplementedException();
            }
        }
        public class FakeReultRepo : IRepository<Result>
        {
            public void Create(Result item)
            {
                throw new NotImplementedException();
            }

            public void Delete(string id)
            {
                throw new NotImplementedException();
            }

            public Result Read(string id)
            {
                throw new NotImplementedException();
            }

            public IQueryable<Result> ReadAll()
            {
                return new List<Result>()
                {

                    new Result() { result_Id=1, pilot_Id = 1, race_code = "AUS", finish_pos = null, start_pos = null },
                    new Result() { result_Id=2,pilot_Id = 1, race_code = "BAH", finish_pos = 2, start_pos = 3 },
                    new Result() { result_Id=3,pilot_Id = 1, race_code = "GER", finish_pos = 2, start_pos = 5 },

                    new Result() {result_Id=4, pilot_Id = 2, race_code = "MON", finish_pos = null, start_pos = 6 },
                    new Result() {result_Id=5, pilot_Id = 2, race_code = "BAH", finish_pos = 12, start_pos = 12 },
                    new Result() {result_Id=6, pilot_Id = 2, race_code = "ITA", finish_pos = 7, start_pos = 7 },


                    new Result() {result_Id=7, pilot_Id = 3, race_code = "HUN", finish_pos = 1, start_pos = 8 },
                    new Result() {result_Id=8,  pilot_Id = 3, race_code = "BAH", finish_pos = 6, start_pos = null },
                    new Result() {result_Id=9, pilot_Id = 3, race_code = "GER", finish_pos = 16, start_pos = 3 },


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
                    new Result() {result_Id=20, pilot_Id = 7, race_code = "ITA", finish_pos = 1, start_pos = 9 },


                    new Result() {result_Id=21, pilot_Id = 8, race_code = "AUS", finish_pos = 6, start_pos = 9 },
                    new Result() {result_Id=22, pilot_Id = 8, race_code = "SPA", finish_pos = 7, start_pos = 2 },
                    new Result() {result_Id=23, pilot_Id = 8, race_code = "MON", finish_pos = null, start_pos = 5 },



                    new Result() {result_Id=24, pilot_Id = 12, race_code = "HUN", finish_pos = 1, start_pos = 4 },
                    new Result() {result_Id=25, pilot_Id = 12, race_code = "GER", finish_pos = 1, start_pos = 2 },
                    new Result() {result_Id=26, pilot_Id = 12, race_code = "MON", finish_pos = 1, start_pos = 3 }
                }.AsQueryable();

            }

            public void Update(Result item)
            {
                throw new NotImplementedException();
            }
        }
        public class FakeRaceRepo : IRepository<Race>
        {
            public void Create(Race item)
            {
                throw new NotImplementedException();
            }

            public void Delete(string id)
            {
                throw new NotImplementedException();
            }

            public Race Read(string id)
            {
                throw new NotImplementedException();
            }

            public IQueryable<Race> ReadAll()
            {
                return new List<Race>()
                {
                    new Race() { race_code = "AUS", date = DateTime.Parse("2021-03-15"), racename = "Ausztrál Nagydíj", location = "Melbourne", laps = 58, length = 5303 },
                    new Race() { race_code = "CHI", date = DateTime.Parse("2021-04-12"), racename = "Kínai Nagydíj", location = "Shanghai", laps = 56, length = 5451 },
                    new Race() { race_code = "BAH", date = DateTime.Parse("2021-04-19"), racename = "Bahreini Nagydíj", location = "Szahír", laps = 57, length = 5412 },
                    new Race() { race_code = "SPA", date = DateTime.Parse("2021-05-15"), racename = "Spanyol Nagydíj", location = "Barcelona", laps = 66, length = 4655 },
                    new Race() { race_code = "MON", date = DateTime.Parse("2021-05-24"), racename = "Monacói Nagydíj", location = "Montreal", laps = 70, length = 4361 },
                    new Race() { race_code = "GER", date = DateTime.Parse("2021-07-19"), racename = "Német Nagydíj", location = "TBD", laps = 67, length = 4574 },
                    new Race() { race_code = "HUN", date = DateTime.Parse("2021-07-26"), racename = "Magyar Nagydíj", location = "Mogyoród", laps = 70, length = 4381 },
                    new Race() { race_code = "BEL", date = DateTime.Parse("2021-08-23"), racename = "Belga Nagydíj", location = "Francorchamps", laps = 44, length = 7004 },
                    new Race() { race_code = "ITA", date = DateTime.Parse("2021-09-06"), racename = "Olasz Nagydíj", location = "Monza", laps = 53, length = 5793 }

                }.AsQueryable();
            }

            public void Update(Race item)
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        [SetUp]
        public void Init()
        {

            result_repo = new FakeReultRepo();
            team_repo = new FakeTeamRepo();
            race_repo = new FakeRaceRepo();
            pilot_repo = new FakePilotRepo();


            pilot_logic = new PilotLogic(pilot_repo);
            result_logic = new ResultLogic(result_repo);
            race_logic = new RaceLogic(race_repo);
            team_logic = new TeamLogic(team_repo);


            var pilots = new Pilot[] {
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
            }.AsQueryable();
            var races = new Race[] {
           new Race() { race_code = "AUS", date = DateTime.Parse("2021-03-15"), racename = "Ausztrál Nagydíj", location = "Melbourne", laps = 58, length = 5303 },
                    new Race() { race_code = "CHI", date = DateTime.Parse("2021-04-12"), racename = "Kínai Nagydíj", location = "Shanghai", laps = 56, length = 5451 },
                    new Race() { race_code = "BAH", date = DateTime.Parse("2021-04-19"), racename = "Bahreini Nagydíj", location = "Szahír", laps = 57, length = 5412 },
                    new Race() { race_code = "SPA", date = DateTime.Parse("2021-05-15"), racename = "Spanyol Nagydíj", location = "Barcelona", laps = 66, length = 4655 },
                    new Race() { race_code = "MON", date = DateTime.Parse("2021-05-24"), racename = "Monacói Nagydíj", location = "Montreal", laps = 70, length = 4361 },
                    new Race() { race_code = "GER", date = DateTime.Parse("2021-07-19"), racename = "Német Nagydíj", location = "TBD", laps = 67, length = 4574 },
                    new Race() { race_code = "HUN", date = DateTime.Parse("2021-07-26"), racename = "Magyar Nagydíj", location = "Mogyoród", laps = 70, length = 4381 },
                    new Race() { race_code = "BEL", date = DateTime.Parse("2021-08-23"), racename = "Belga Nagydíj", location = "Francorchamps", laps = 44, length = 7004 },
                    new Race() { race_code = "ITA", date = DateTime.Parse("2021-09-06"), racename = "Olasz Nagydíj", location = "Monza", laps = 53, length = 5793 }

                }.AsQueryable();

            mockRaceRepository = new Mock<IRaceRepository>();
            mockPilotRepository = new Mock<IPilotRepository>();
            mockPilotRepository
                .Setup(p => p.ReadAll())
                .Returns(pilots);
            mockRaceRepository
                .Setup(r => r.ReadAll())
                .Returns(races);
            pilot_logic_mock = new PilotLogic(mockPilotRepository.Object);
            race_logic_mock = new RaceLogic(mockRaceRepository.Object);
        }

        // crud & Exception tests
        [Test]
        public void CreatePilotTestWithhInCorrectTeamId()
        {
            var pilot = new Pilot() { Name = "proba", team_id = 20, pilot_Id = 15, dateofbirth = 2000, };
            try
            {
                pilot_logic_mock.Create(pilot);
            }
            catch
            {

            }
            mockPilotRepository.Verify(r => r.Create(pilot), Times.Never);
        }
        [Test]
        public void CreatePilotTestWithCorrectTeamId()
        {
            var pilot = new Pilot() { Name = "Pastor Maldonado", team_id = 2 };

            //ACT
            pilot_logic_mock.Create(pilot);

            //ASSERT
            mockPilotRepository.Verify(p => p.Create(pilot), Times.Once);
        }

        [Test]
        public void CreateRaceTestWithhInCorrectRace()
        {
            var race = new Race() { race_code = "HUNGARY" };

            try
            {
                race_logic_mock.Create(race);
            }
            catch
            {

            }
            mockRaceRepository.Verify(r => r.Create(race), Times.Never);

        }
        [Test]
        public void PilotDelete()
        {
            pilot_logic_mock.Delete("1");

            // Assert
            mockPilotRepository
                .Verify(r => r.Delete(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void TestReadWithInvalidIdThrowsException()
        {
            // Arrange
            mockPilotRepository
                .Setup(r => r.Read(It.IsAny<string>()))
                .Returns(value: null);

            // Act + Assert
            Assert.Throws<ArgumentException>(() => pilot_logic_mock.Read("13"));

        }

        [Test]
        public void TestReadWithValidIdReturnsExpectedObject()
        {
            // Arrange
            Pilot expected = new Pilot()
            {
                pilot_Id = 6,
                Name = "Fernando Alonso",
                dateofbirth = 1980,
                team_id = 5
            };

            mockPilotRepository
                .Setup(r => r.Read("6"))
                .Returns(expected);

            // Act
            var actual = pilot_logic_mock.Read("6");

            // Assert
            Assert.That(actual, Is.EqualTo(expected));

        }



        //non crud tests

        [Test]
        public void Average_FinishPos()
        {
            var actual = result_logic.AverageFinishPos(pilot_logic).ToList();
            var expected = new List<AveragefinishPos>()
            {
                 new AveragefinishPos()
                {
                    Pilot_name ="Alexander Rossi",
                    averageFinishPos = 2

                },
                  new AveragefinishPos()
                {
                    Pilot_name ="Carlos Sainz Jnr",
                    averageFinishPos = 9

                },
                   new AveragefinishPos()
                {
                    Pilot_name ="Daniel Ricciardo",
                    averageFinishPos = 7

                },
                  new AveragefinishPos()
                {
                    Pilot_name ="Daniil Kvyat",
                    averageFinishPos = 8

                },

                new AveragefinishPos()
                {
                    Pilot_name ="Felipe Massa",
                    averageFinishPos = 2

                },
                  new AveragefinishPos()
                {
                    Pilot_name ="Fernando Alonso",
                    averageFinishPos = 11
                },
                  new AveragefinishPos()
                {
                    Pilot_name ="Jenson Button",
                    averageFinishPos = 5

                },

                new AveragefinishPos()
                {
                    Pilot_name ="Max Verstappen",
                    averageFinishPos = 6

                },
                  new AveragefinishPos()
                {
                    Pilot_name ="Lewis Hamilton",
                    averageFinishPos = 1

                }
            };

            Assert.AreEqual(expected, actual);
            ;

        }
        [Test]
        public void Pilot_TeamInfo()
        {
            var actual = pilot_logic.Pilot_TeamInfo(team_logic).ToList();
            var expected = new List<PilotTeamInfo>()
            {
                 new PilotTeamInfo()
                {
                    PilotNum =1,
                    TeamName = "Marussia Ferrari"

                },
                  new PilotTeamInfo()
                {
                    PilotNum =2,
                    TeamName = "STR Renault",

                },
                  new PilotTeamInfo()
                {
                    PilotNum =1,
                    TeamName = "Red Bull Renault"

                },
                 new PilotTeamInfo()
                {
                     PilotNum=3,
                    TeamName = "Williams Mercedes"

                },

                new PilotTeamInfo()
                {
                    PilotNum =2,
                    TeamName = "McLaren Honda"

                },
                new PilotTeamInfo()
                {
                     PilotNum=1,
                    TeamName = "Ferrari"

                },



                new PilotTeamInfo()
                {
                    PilotNum=1,
                    TeamName = "Mercedes"

                }

            };

            Assert.AreEqual(expected, actual);
            ;

        }

        [Test]
        public void Best_Pilot()
        {
            var actual = result_logic.Best_Pilot(pilot_logic).ToList();
            var expected = new List<BestPilot>()
            {
                 new BestPilot()
                 {
                     Pilot_Name ="Lewis Hamilton",
                     Wins =3

                 }
            };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Mogyorod_Results()
        {
            var actual = result_logic.Mogyorod_Results(race_logic).ToList();
            var expected = new List<MogyorodResults>()
            {
                new MogyorodResults() {
                    Race_name ="Magyar Nagydíj",
                    Finish_pos = 1,
                    Start_pos = 8
                },
                new MogyorodResults() {
                    Race_name ="Magyar Nagydíj",
                    Finish_pos = 5,
                    Start_pos =3
                },
                new MogyorodResults() {
                    Race_name ="Magyar Nagydíj",
                    Finish_pos =1 ,
                    Start_pos =4
                }
            };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Top2_YoungestPilots()
        {
            var actual = pilot_logic.Top2_YoungestPilots(result_logic).ToList();
            var expected = new List<PilotLogic.Top2YoungestPilots>()
            {
                 new Top2YoungestPilots()
                {
                    PilotName ="Lewis Hamilton",
                    Dateofbirth = 1985,
                    Results = 1

                },
                  new Top2YoungestPilots()
                {
                    PilotName ="Alexander Rossi",
                    Dateofbirth = 1991,
                    Results = 2
                }
            };
            Assert.AreEqual(expected, actual);
        }

    }
}
