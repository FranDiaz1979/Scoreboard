namespace ScoreBoard
{
    using global::ScoreBoard.Models;
    using System.Collections.Generic;

    public class ScoreBoardService
    {
        private ScoreBoard scoreBoard;

        public void StartGame(string HomeTeam, string AwayTeam)
        {
            // Si hometeam está vacio peta
            // si awayteam esta vacio peta
            // si hometeam ya existe en scoreboard peta
            // si awayteam ya existe en scoreboard peta

            // guardar el score
        }

        public void FinishGame(string HomeTeam, string AwayTeam)
        {
            // Si hometeam está vacio peta
            // si awayteam esta vacio peta
            // si hometeam o awayteam no existen en scoreboard peta

            // Quita el score del scoreboard
        }

        public void UpdateScore(string HomeTeam, string AwayTeam, int HomeScore, int AwayScore)
        {
            // Si hometeam está vacio peta
            // si awayteam esta vacio peta
            // si hometeam o awayteam no existen en scoreboard peta
            // La diferencia en score de home==0 y away==1 o bien score de home==1 y away==0 o peta
            //      Se permite que el update sea 1 gol negativo por si el cliente se equivoca

            // Cambiar el score
        }

        public IEnumerable<Score> GetSummary()
        {
            // Code
            return new List<Score>();
        }
    }
}