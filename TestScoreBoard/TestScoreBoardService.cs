namespace TestScoreBoard
{
    using NUnit.Framework;
    using ScoreBoard;
    using ScoreBoard.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // public void StartGame(string HomeTeam, string AwayTeam)
    //      Si hometeam está vacio peta
    //      si awayteam esta vacio peta
    //      si hometeam ya existe en scoreboard peta
    //      si awayteam ya existe en scoreboard peta
    //      OK

    // public void FinishGame(string HomeTeam, string AwayTeam)
    //      Si hometeam está vacio peta
    //      si awayteam esta vacio peta
    //      si hometeam o awayteam no existen en scoreboard peta
    //      Quita el score del scoreboard

    // public void UpdateScore(string HomeTeam, string AwayTeam, int HomeScore, int AwayScore)
    //      Si hometeam está vacio peta
    //      si awayteam esta vacio peta
    //      si hometeam o awayteam no existen en scoreboard peta
    //      La diferencia en score de home==0 y away==1 o bien score de home==1 y away==0 o peta
    //          Se permite que el update sea 1 gol negativo por si el cliente se equivoca
    //      Cambiar el score


    // public IEnumerable<Score> GetSummary()

    public class Tests
    {
        private ScoreBoardService scoreBoardService;

        [SetUp]
        public void Setup()
        {
            scoreBoardService = new ScoreBoardService();
        }

        [Test]
        public void StartGame_HomeTeamVoid()
        {
            Assert.Throws<ArgumentNullException>(() => scoreBoardService.StartGame("", "test"));
        }

        [Test]
        public void StartGame_AwayTeamVoid()
        {
            Assert.Throws<ArgumentNullException>(() => scoreBoardService.StartGame("test", ""));
        }

        [Test]
        public void StartGame_HomeTeamExists()
        {
            scoreBoardService.StartGame("test", "test2");

            Assert.Throws<ArgumentException>(() => scoreBoardService.StartGame("test", "test3"));
        }

        [Test]
        public void StartGame_AwayTeamExists()
        {
            scoreBoardService.StartGame("test", "test2");

            Assert.Throws<ArgumentException>(() => scoreBoardService.StartGame("test3", "test2"));
        }

        [Test]
        public void StartGame_Ok()
        {
            scoreBoardService.StartGame("test", "test2");
            Score result = ((List<Score>)scoreBoardService.GetSummary()).First();

            Assert.AreEqual("test", result.HomeTeam);
            Assert.AreEqual("test2", result.AwayTeam);
            Assert.AreEqual(0, result.HomeScore);
            Assert.AreEqual(0, result.AwayScore);
        }

        [Test]
        public void FinishGame_HomeTeamVoid()
        {
            Assert.Throws<ArgumentNullException>(() => scoreBoardService.FinishGame("", "test"));
        }

        [Test]
        public void FinishGame_AwayTeamVoid()
        {
            Assert.Throws<ArgumentNullException>(() => scoreBoardService.FinishGame("test", ""));
        }

        [Test]
        public void FinishGame_HomeTeamNotExists()
        {
            scoreBoardService.StartGame("test", "test2");

            Assert.Throws<ArgumentException>(() => scoreBoardService.FinishGame("test33", "test2"));
        }

        [Test]
        public void FinishGame_AwayTeamNotExists()
        {
            scoreBoardService.StartGame("test", "test2");

            Assert.Throws<ArgumentException>(() => scoreBoardService.FinishGame("test", "test22"));
        }

        [Test]
        public void FinishGame_Ok()
        {
            scoreBoardService.StartGame("test", "test2");
            scoreBoardService.FinishGame("test", "test2");
        }

        [Test]
        public void UpdateScore_HomeTeamVoid()
        {
            Assert.Throws<ArgumentNullException>(() => scoreBoardService.UpdateScore("", "test", 0, 1));
        }

        [Test]
        public void UpdateScore_AwayTeamVoid()
        {
            Assert.Throws<ArgumentNullException>(() => scoreBoardService.UpdateScore("test", "", 0, 1));
        }

        [Test]
        public void UpdateScore_HomeTeamNotExists()
        {
            scoreBoardService.StartGame("test", "test2");

            Assert.Throws<ArgumentException>(() => scoreBoardService.UpdateScore("test33", "test2", 0, 1));
        }

        [Test]
        public void UpdateScore_AwayTeamNotExists()
        {
            scoreBoardService.StartGame("test", "test2");

            Assert.Throws<ArgumentException>(() => scoreBoardService.UpdateScore("test", "test22", 0, 1));
        }

        [TestCase("test", "test2", 0, 2)]
        [TestCase("test", "test2", 2, 0)]
        [TestCase("test", "test2", 1, 1)]
        public void UpdateScore_BadScore(string HomeTeam, string AwayTeam, int HomeScore, int AwayScore)
        {
            scoreBoardService.StartGame(HomeTeam, AwayTeam);

            Assert.Throws<Exception>(() => scoreBoardService.UpdateScore(HomeTeam, AwayTeam, HomeScore, AwayScore));
        }

        [TestCase("test", "test2", 0, 0)]
        [TestCase("test", "test2", 2, 2)]
        [TestCase("test", "test2", 1, 1)]
        [TestCase("test", "test2", 0, 4)]
        public void UpdateScore_BadScoreStarted(string HomeTeam, string AwayTeam, int HomeScore, int AwayScore)
        {
            scoreBoardService.StartGame(HomeTeam, AwayTeam);
            scoreBoardService.UpdateScore(HomeTeam, AwayTeam, 0, 1);
            scoreBoardService.UpdateScore(HomeTeam, AwayTeam, 0, 2);

            Assert.Throws<Exception>(() => scoreBoardService.UpdateScore(HomeTeam, AwayTeam, HomeScore, AwayScore));
        }

        [Test]
        public void UpdateScore_OkAdding()
        {
            scoreBoardService.StartGame("test", "test2");

            scoreBoardService.UpdateScore("test", "test2", 0, 1);
        }

        public void UpdateScore_OkRemoving()
        {
            scoreBoardService.StartGame("test", "test2");
            scoreBoardService.UpdateScore("test", "test2", 0, 1);
            scoreBoardService.UpdateScore("test", "test2", 0, 2);

            scoreBoardService.UpdateScore("test", "test2", 0, 1);
        }

        [Test]
        public void GetSummary_NotReturnNull()
        {
            Assert.NotNull(scoreBoardService.GetSummary());
        }

        [Test]
        public void GetSummary_Ok()
        {
            scoreBoardService.StartGame("test1", "test2");
            scoreBoardService.UpdateScore("test1", "test2", 0, 1);

            scoreBoardService.StartGame("testA", "testB");
            scoreBoardService.UpdateScore("testA", "testB", 1, 0);
            scoreBoardService.UpdateScore("testA", "testB", 2, 0);

            List<Score> result = (List<Score>)scoreBoardService.GetSummary();

            Score score1 = result.First(x => x.HomeTeam == "test1");
            Score score2 = result.First(x => x.HomeTeam == "testA");

            Assert.AreEqual("test2", score1.AwayTeam);
            Assert.AreEqual(0, score1.HomeScore);
            Assert.AreEqual(1, score1.AwayScore);

            Assert.AreEqual("testB", score2.AwayTeam);
            Assert.AreEqual(2, score2.HomeScore);
            Assert.AreEqual(0, score2.AwayScore);

        }

    }
}