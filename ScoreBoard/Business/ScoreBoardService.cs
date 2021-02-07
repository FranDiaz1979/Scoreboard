namespace ScoreBoard
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using global::ScoreBoard.Models;

    public class ScoreBoardService
    {
        private readonly ScoreBoard scoreBoard;

        public ScoreBoardService()
        {
            this.scoreBoard = new ScoreBoard();
        }

        public void StartGame(string homeTeam, string awayTeam)
        {
            this.CheckStringNotNull(homeTeam);
            this.CheckStringNotNull(awayTeam);
            this.CheckNotExist(homeTeam);
            this.CheckNotExist(awayTeam);

            this.scoreBoard.Scores.Add(new Score
            {
                HomeTeam = homeTeam,
                AwayTeam = awayTeam,
                HomeScore = 0,
                AwayScore = 0,
            });
        }

        public void FinishGame(string homeTeam, string awayTeam)
        {
            this.CheckStringNotNull(homeTeam);
            this.CheckStringNotNull(awayTeam);
            Score score = this.FindGame(homeTeam, awayTeam);
            this.scoreBoard.Scores.Remove(score);
        }

        public void UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
        {
            this.CheckStringNotNull(homeTeam);
            this.CheckStringNotNull(awayTeam);

            Score score = this.FindGame(homeTeam, awayTeam);
            this.CheckOnlyOneGoal(score, homeScore, awayScore);

            score.HomeScore = homeScore;
            score.AwayScore = awayScore;
        }

        public IEnumerable<Score> GetSummary()
        {
            return this.scoreBoard.Scores.OrderByDescending(x => x.HomeScore + x.AwayScore).ThenByDescending(x => x.Datetime);
        }

        private Score FindGame(string homeTeam, string awayTeam)
        {
            var result = this.scoreBoard.Scores.Find(x => x.HomeTeam.ToLower() == homeTeam.ToLower() && x.AwayTeam.ToLower() == awayTeam.ToLower());
            if (result == null)
            {
                throw new ArgumentException("Game not finded");
            }

            return result;
        }

        private void CheckOnlyOneGoal(Score score, int homeScore, int awayScore)
        {
            int differenceHomeScore = homeScore - score.HomeScore;
            if (differenceHomeScore < -1 || differenceHomeScore > 1)
            {
                throw new ArgumentException("Score is not right");
            }

            int differenceAwayScore = awayScore - score.AwayScore;
            if (differenceAwayScore < -1 || differenceAwayScore > 1)
            {
                throw new ArgumentException("Score is not right");
            }

            if (differenceHomeScore != 0 && differenceAwayScore != 0)
            {
                throw new ArgumentException("Score is not right");
            }
        }

        private void CheckStringNotNull(string argument)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentNullException(nameof(argument));
            }
        }

        private void CheckNotExist(string argument)
        {
            foreach (var score in this.scoreBoard.Scores)
            {
                if (score.HomeTeam.ToLower() == argument.ToLower())
                {
                    throw new ArgumentException(nameof(score.HomeTeam));
                }
                if (score.AwayTeam.ToLower() == argument.ToLower())
                {
                    throw new ArgumentException(nameof(score.AwayTeam));
                }
            }
        }
    }
}