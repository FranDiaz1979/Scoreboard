namespace ScoreBoard
{
    using global::ScoreBoard.Models;
    using System;
    using System.Collections.Generic;

    public class ScoreBoardService
    {
        private ScoreBoard scoreBoard;

        public void StartGame(string HomeTeam, string AwayTeam)
        {
            // Code
        }

        public void FinishGame(string HomeTeam, string AwayTeam)
        {
            // Code
        }

        public void UpdateScore(string HomeTeam, string AwayTeam, int HomeScore, int AwayScore)
        {
            // Code
        }

        public IEnumerable<Score> GetSummary()
        {
            // Code
            return new List<Score>();
        }


    }
}
