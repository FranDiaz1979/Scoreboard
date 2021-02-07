namespace TestScoreBoard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using ScoreBoard;
    using ScoreBoard.Models;

    public class Tests
    {
        private ScoreBoardService scoreBoardService;

        [SetUp]
        public void Setup()
        {
            this.scoreBoardService = new ScoreBoardService();
        }

        [Test]
        public void StartGame_HomeTeamVoid()
        {
            Assert.Throws<ArgumentException>(() => this.scoreBoardService.StartGame(string.Empty, "test"));
        }

        [Test]
        public void StartGame_AwayTeamVoid()
        {
            Assert.Throws<ArgumentException>(() => this.scoreBoardService.StartGame("test", string.Empty));
        }

        [Test]
        public void StartGame_HomeTeamExists()
        {
            this.scoreBoardService.StartGame("test", "test2");

            Assert.Throws<ArgumentException>(() => this.scoreBoardService.StartGame("test", "test3"));
        }

        [Test]
        public void StartGame_AwayTeamExists()
        {
            this.scoreBoardService.StartGame("test", "test2");

            Assert.Throws<ArgumentException>(() => this.scoreBoardService.StartGame("test3", "test2"));
        }

        [Test]
        public void StartGame_Ok()
        {
            this.scoreBoardService.StartGame("test", "test2");
            Score result = this.scoreBoardService.GetSummary().First();

            Assert.AreEqual("test", result.HomeTeam);
            Assert.AreEqual("test2", result.AwayTeam);
            Assert.AreEqual(0, result.HomeScore);
            Assert.AreEqual(0, result.AwayScore);
        }

        [Test]
        public void FinishGame_HomeTeamVoid()
        {
            Assert.Throws<ArgumentException>(() => this.scoreBoardService.FinishGame(string.Empty, "test"));
        }

        [Test]
        public void FinishGame_AwayTeamVoid()
        {
            Assert.Throws<ArgumentException>(() => this.scoreBoardService.FinishGame("test", string.Empty));
        }

        [Test]
        public void FinishGame_HomeTeamNotExists()
        {
            this.scoreBoardService.StartGame("test", "test2");

            Assert.Throws<ArgumentException>(() => this.scoreBoardService.FinishGame("test33", "test2"));
        }

        [Test]
        public void FinishGame_AwayTeamNotExists()
        {
            this.scoreBoardService.StartGame("test", "test2");

            Assert.Throws<ArgumentException>(() => this.scoreBoardService.FinishGame("test", "test22"));
        }

        [Test]
        public void FinishGame_Ok()
        {
            this.scoreBoardService.StartGame("test", "test2");
            this.scoreBoardService.FinishGame("test", "test2");

            Assert.Pass();
        }

        [Test]
        public void UpdateScore_HomeTeamVoid()
        {
            Assert.Throws<ArgumentException>(() => this.scoreBoardService.UpdateScore(string.Empty, "test", 0, 1));
        }

        [Test]
        public void UpdateScore_AwayTeamVoid()
        {
            Assert.Throws<ArgumentException>(() => this.scoreBoardService.UpdateScore("test", string.Empty, 0, 1));
        }

        [Test]
        public void UpdateScore_HomeTeamNotExists()
        {
            this.scoreBoardService.StartGame("test", "test2");

            Assert.Throws<ArgumentException>(() => this.scoreBoardService.UpdateScore("test33", "test2", 0, 1));
        }

        [Test]
        public void UpdateScore_AwayTeamNotExists()
        {
            this.scoreBoardService.StartGame("test", "test2");

            Assert.Throws<ArgumentException>(() => this.scoreBoardService.UpdateScore("test", "test22", 0, 1));
        }

        [TestCase("test", "test2", 0, 2)]
        [TestCase("test", "test2", 2, 0)]
        [TestCase("test", "test2", 1, 1)]
        public void UpdateScore_BadScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
        {
            this.scoreBoardService.StartGame(homeTeam, awayTeam);

            Assert.Throws<ArgumentException>(() => this.scoreBoardService.UpdateScore(homeTeam, awayTeam, homeScore, awayScore));
        }

        [TestCase("test", "test2", 0, 0)]
        [TestCase("test", "test2", 2, 2)]
        [TestCase("test", "test2", 1, 1)]
        [TestCase("test", "test2", 0, 4)]
        public void UpdateScore_BadScoreStarted(string homeTeam, string awayTeam, int homeScore, int awayScore)
        {
            this.scoreBoardService.StartGame(homeTeam, awayTeam);
            this.scoreBoardService.UpdateScore(homeTeam, awayTeam, 0, 1);
            this.scoreBoardService.UpdateScore(homeTeam, awayTeam, 0, 2);

            Assert.Throws<ArgumentException>(() => this.scoreBoardService.UpdateScore(homeTeam, awayTeam, homeScore, awayScore));
        }

        [Test]
        public void UpdateScore_OkAdding()
        {
            this.scoreBoardService.StartGame("test", "test2");

            this.scoreBoardService.UpdateScore("test", "test2", 0, 1);

            Assert.Pass();
        }

        public void UpdateScore_OkRemoving()
        {
            this.scoreBoardService.StartGame("test", "test2");
            this.scoreBoardService.UpdateScore("test", "test2", 0, 1);
            this.scoreBoardService.UpdateScore("test", "test2", 0, 2);

            this.scoreBoardService.UpdateScore("test", "test2", 0, 1);
        }

        [Test]
        public void GetSummary_NotReturnNull()
        {
            Assert.NotNull(this.scoreBoardService.GetSummary());
        }

        [Test]
        public void GetSummary_Ok()
        {
            this.scoreBoardService.StartGame("test1", "test2");
            this.scoreBoardService.UpdateScore("test1", "test2", 0, 1);

            this.scoreBoardService.StartGame("testA", "testB");
            this.scoreBoardService.UpdateScore("testA", "testB", 1, 0);
            this.scoreBoardService.UpdateScore("testA", "testB", 2, 0);

            var result = this.scoreBoardService.GetSummary();

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