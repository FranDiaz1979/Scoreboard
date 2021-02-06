namespace ScoreBoard.Models
{
    using global::ScoreBoard.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ScoreBoard
    {
        public IEnumerable<Score> Scores { get; set; }
    }
}
