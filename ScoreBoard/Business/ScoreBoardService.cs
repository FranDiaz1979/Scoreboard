namespace ScoreBoard
{
    using global::ScoreBoard.Models;
    using System;
    using System.Collections.Generic;

    public class ScoreBoardService
    {
        private ScoreBoard scoreBoard;

        public ScoreBoardService()
        {
            scoreBoard = new ScoreBoard();
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
            foreach (var score in scoreBoard.Scores)
            {
                if (score.HomeTeam.ToLower() == argument.ToLower() || score.AwayTeam.ToLower() == argument.ToLower())
                {
                    throw new ArgumentException();
                }
            }
        }

        public void StartGame(string homeTeam, string awayTeam)
        {
            CheckStringNotNull(homeTeam);
            CheckStringNotNull(awayTeam);
            CheckNotExist(homeTeam);
            CheckNotExist(awayTeam);

            scoreBoard.Scores.Add(new Score
            {
                HomeTeam = homeTeam,
                AwayTeam = awayTeam,
                HomeScore = 0,
                AwayScore = 0,
            });
        }

        public void FinishGame(string homeTeam, string awayTeam)
        {
            CheckStringNotNull(homeTeam);
            CheckStringNotNull(awayTeam);
            Score score = FindGame(homeTeam, awayTeam);
            scoreBoard.Scores.Remove(score);
        }

        private Score FindGame(string homeTeam, string awayTeam)
        {
            return scoreBoard.Scores.Find(x => x.HomeTeam.ToLower() == homeTeam.ToLower() && x.AwayTeam.ToLower() == awayTeam.ToLower());
        }

        public void UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
        {
            CheckStringNotNull(homeTeam);
            CheckStringNotNull(awayTeam);

            Score score = FindGame(homeTeam, awayTeam);
            CheckOnlyOneGoal(score, homeScore, awayScore);

            score.HomeScore = homeScore;
            score.AwayScore = awayScore;
        }

        private void CheckOnlyOneGoal(Score score, int homeScore, int awayScore)
        {
            // La diferencia en score de home==0 y away==1 o bien score de home==1 y away==0 o peta
            //      Se permite que el update sea 1 gol negativo por si el cliente se equivoca, que pueda rectificar
            //      Se permite que la diferencia sea 0 por si se envia n veces la misma peticion

            int differenceHomeScore = homeScore - score.HomeScore;
            if (differenceHomeScore < -1 || differenceHomeScore > 1)
            {
                throw new Exception("Score is not right");
            }

            int differenceAwayScore = awayScore - score.AwayScore;
            if (differenceAwayScore < -1 || differenceAwayScore > 1)
            {
                throw new Exception("Score is not right");
            }

            if (differenceHomeScore != 0 && differenceAwayScore != 0)
            {
                throw new Exception("Score is not right");
            }
        }

        public IEnumerable<Score> GetSummary()
        {
            return scoreBoard.Scores;
        }
    }
}