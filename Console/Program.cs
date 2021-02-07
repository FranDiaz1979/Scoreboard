namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ScoreBoard;
    using ScoreBoard.Models;

    class Program
    {
        private static string titulo = "ScoreBoard (Revival frontend for backend developers)";
        private static ScoreBoardService scoreBoardService = new ScoreBoardService();
        static void Main()
        {
            Console.Title = titulo;
            Menu();
        }

        private static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Menu");
            Console.WriteLine("");
            Console.WriteLine("1. Start a game");
            Console.WriteLine("2. Finish a game");
            Console.WriteLine("3. Update a score");
            Console.WriteLine("4. Get summary");
            Console.WriteLine("");
            Console.WriteLine("x. Close");
            Console.WriteLine("");
            Console.Write("Press a key to select an option...");
            var caracter = Console.ReadKey().KeyChar;

            switch (caracter)
            {
                case '1':
                    OptionStartGame();
                    break;
                case '2':
                    OptionFinishGame();
                    break;
                case '3':
                    OptionUpdateScore();
                    break;
                case '4':
                    OptionGetSummary();
                    break;
                case 'x':
                case 'X':
                    Environment.Exit(0);
                    break;
                default:
                    Menu();
                    break;
            }
        }

        private static void OptionGetSummary()
        {
            IQueryable<Score> scores = (IQueryable<Score>)scoreBoardService.GetSummary();

            Console.Clear();
            Console.WriteLine("Get Summary");
            Console.WriteLine("");
            int index = 0;
            foreach (var score in scores)
            {
                Console.WriteLine("{0}. {1} {2} - {3} {4}",++index,score.HomeTeam,score.HomeScore,score.AwayTeam, score.AwayScore);

            }
            Console.WriteLine("");
            Console.Write("Press a key to return to menu...");
            Console.ReadKey();
            Menu();
        }

        private static void OptionUpdateScore()
        {
            Console.Clear();
            Console.WriteLine("Update Score");
            Console.WriteLine("");
            Console.Write("Home Team: ");
            string homeTeam = Console.ReadLine();
            Console.Write("Away Team: ");
            string awayTeam = Console.ReadLine();
            Console.Write("Home Score: ");
            string homeScoreString = Console.ReadLine();
            int homeScore;
            bool success = int.TryParse(homeScoreString, out homeScore);
            if (!success)
            {
                throw new ArgumentException("Home Score is not a number");
            }
            Console.Write("Away Score: ");
            string awayScoreString = Console.ReadLine();
            int awayScore;
            success = int.TryParse(awayScoreString, out awayScore);
            if (!success)
            {
                throw new ArgumentException("Away Score is not a number");
            }

            scoreBoardService.UpdateScore(homeTeam, awayTeam, homeScore, awayScore);

            Console.WriteLine("Score Updated");
            Console.WriteLine("");
            Console.Write("Press a key to return to menu...");
            Console.ReadKey();
            Menu();
        }

        private static void OptionFinishGame()
        {
            Console.Clear();
            Console.WriteLine("Finish a game");
            Console.WriteLine("");
            Console.Write("Home Team: ");
            string homeTeam = Console.ReadLine();
            Console.Write("Away Team: ");
            string awayTeam = Console.ReadLine();

            scoreBoardService.FinishGame(homeTeam, awayTeam);

            Console.WriteLine("Game finished");
            Console.WriteLine("");
            Console.Write("Press a key to return to menu...");
            Console.ReadKey();
            Menu();
        }

        private static void OptionStartGame()
        {
            Console.Clear();
            Console.WriteLine("Start a game");
            Console.WriteLine("");
            Console.Write("Home Team: ");
            string homeTeam = Console.ReadLine();
            Console.Write("Away Team: ");
            string awayTeam = Console.ReadLine();

            scoreBoardService.StartGame(homeTeam, awayTeam);

            Console.WriteLine("Game started");
            Console.WriteLine("");
            Console.Write("Press a key to return to menu...");
            Console.ReadKey();
            Menu();
        }
    }
}
