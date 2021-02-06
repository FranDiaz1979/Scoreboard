namespace TestScoreBoard
{
    using NUnit.Framework;
    using ScoreBoard;
    using System;

    // public void StartGame(string HomeTeam, string AwayTeam)
    // public void FinishGame(string HomeTeam, string AwayTeam)
    // public void UpdateScore(string HomeTeam, string AwayTeam, int HomeScore, int AwayScore)
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
        public void NotReturnNull()
        {
            throw new NotImplementedException();
        }


    }
}