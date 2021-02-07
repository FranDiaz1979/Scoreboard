using System;

namespace ScoreBoard.Models
{
    public class Score
    {
        public Score()
        {
            Datetime = DateTime.Now;
        }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public int HomeScore { get; set; }

        public int AwayScore { get; set; }

        public DateTime Datetime { get; }
    }
}