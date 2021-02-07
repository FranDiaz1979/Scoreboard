namespace ConsoleApp
{
    //// TODO: Ordenar el resultado
    //// TODO: Repasar README.md
    //// TODO: Probar a mano el codigo

    using System;
    using System.Linq;
    using ScoreBoard;
    using ScoreBoard.Models;

    internal static class Program
    {
        private static readonly string titulo = "ScoreBoard (Revival frontend for backend developers)";
        private static readonly ScoreBoardService scoreBoardService = new ScoreBoardService();

        private static void Main()
        {
            Console.Title = titulo;
            Menu();
        }

        private static void Menu()
        {
            Console.Clear();
            Console.WriteLine("Menu");
            Console.WriteLine();
            Console.WriteLine("1. Start a game");
            Console.WriteLine("2. Finish a game");
            Console.WriteLine("3. Update a score");
            Console.WriteLine("4. Get summary");
            Console.WriteLine();
            Console.WriteLine("x. Close");
            Console.WriteLine();
            Console.Write("Press a key to select an option...");
            var caracter = Console.ReadKey().KeyChar;

            switch (caracter)
            {
                case '1': OptionStartGame(); break;

                case '2': OptionFinishGame(); break;

                case '3': OptionUpdateScore(); break;

                case '4': OptionGetSummary(); break;

                case 'x':
                case 'X': Environment.Exit(0); break;

                default: Menu(); break;
            }
        }

        private static void OptionGetSummary()
        {
            IQueryable<Score> scores = (IQueryable<Score>)scoreBoardService.GetSummary();

            Console.Clear();
            Console.WriteLine("Get Summary");
            Console.WriteLine();
            int index = 0;
            foreach (var score in scores)
            {
                Console.WriteLine("{0}. {1} {2} - {3} {4}", ++index, score.HomeTeam, score.HomeScore, score.AwayTeam, score.AwayScore);
            }

            ReturnToMenu();
        }

        private static void OptionUpdateScore()
        {
            Console.Clear();
            Console.WriteLine("Update Score");
            Console.WriteLine();
            string homeTeam = ReadArgument("Home Team");
            string awayTeam = ReadArgument("Away Team");
            string homeScoreString = ReadArgument("Home Score");
            int homeScore = ConvertToInt(homeScoreString);
            string awayScoreString = ReadArgument("Away Score");
            int awayScore = ConvertToInt(awayScoreString);

            scoreBoardService.UpdateScore(homeTeam, awayTeam, homeScore, awayScore);

            Console.WriteLine("Score Updated");
            ReturnToMenu();
        }

        private static void OptionFinishGame()
        {
            Console.Clear();
            Console.WriteLine("Finish a game");
            Console.WriteLine();
            string homeTeam = ReadArgument("Home Team");
            string awayTeam = ReadArgument("Away Team");

            scoreBoardService.FinishGame(homeTeam, awayTeam);

            Console.WriteLine("Game finished");
            ReturnToMenu();
        }

        private static void OptionStartGame()
        {
            Console.Clear();
            Console.WriteLine("Start a game");
            Console.WriteLine();
            string homeTeam = ReadArgument("Home Team");
            string awayTeam = ReadArgument("Away Team");

            scoreBoardService.StartGame(homeTeam, awayTeam);

            Console.WriteLine("Game started");
            ReturnToMenu();
        }

        private static void ReturnToMenu()
        {
            Console.WriteLine();
            Console.Write("Press a key to return to menu...");
            Console.ReadKey();
            Menu();
        }

        private static int ConvertToInt(string integerString)
        {
            bool success = int.TryParse(integerString, out int result);
            if (!success)
            {
                throw new ArgumentException("Home Score is not a valid number");
            }
            return result;
        }

        private static string ReadArgument(string text)
        {
            Console.Write("{0}: ", text);
            return Console.ReadLine();
        }
    }
}