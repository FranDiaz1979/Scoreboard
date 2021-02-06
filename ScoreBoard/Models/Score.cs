namespace ScoreBoard.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Score
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }
}
