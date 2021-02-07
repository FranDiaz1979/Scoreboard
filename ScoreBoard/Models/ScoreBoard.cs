namespace ScoreBoard.Models
{
    using System.Collections.Generic;

    public class ScoreBoard
    {
        public ScoreBoard()
        {
            this.Scores = new List<Score>();
        }

        public List<Score> Scores { get; set; }
    }
}