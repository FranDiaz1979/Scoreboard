namespace ScoreBoard.Models
{
    using global::ScoreBoard.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ScoreBoard
    {
        public ScoreBoard()
        {
            Scores = new List<Score>();
        }

        public List<Score> Scores { get; set; }
    }
}
