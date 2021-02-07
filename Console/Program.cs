namespace ConsoleApp
{
    using System;
    using ScoreBoard;

    internal static class Program
    {
        private static readonly string titulo = "ScoreBoard (Revival frontend for backend developers)";
        private static readonly ScoreBoardService scoreBoardService = new ScoreBoardService();

        private static void Main()
        {
            Console.Title = titulo;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Menu();
        }

        private static void Menu()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Menu");
                Console.WriteLine("====");
                Console.WriteLine();
                Console.WriteLine("0. Initialize with a sample");
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
                    case '0': InitializeWithSamples(); break;

                    case '1': OptionStartGame(); break;

                    case '2': OptionFinishGame(); break;

                    case '3': OptionUpdateScore(); break;

                    case '4': OptionGetSummary(); break;

                    case 'x':
                    case 'X': Environment.Exit(0); break;

                    default: Menu(); break;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine();
                Console.WriteLine("An error has occurred: {0}", exception.Message);
                ReturnToMenu();
            }
        }

        private static void InitializeWithSamples()
        {
            Console.Clear();
            Console.WriteLine("Initialize Scoreboard");
            Console.WriteLine("---------------------");

            string homeTeam = "Mexico";
            string awayTeam = "Canada";
            scoreBoardService.StartGame(homeTeam, awayTeam);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 0, 1);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 0, 2);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 0, 3);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 0, 4);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 0, 5);

            homeTeam = "Spain";
            awayTeam = "Brazil";
            scoreBoardService.StartGame(homeTeam, awayTeam);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 1, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 2, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 3, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 4, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 5, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 6, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 7, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 8, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 9, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 10, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 10, 1);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 10, 2);

            homeTeam = "Germany";
            awayTeam = "France";
            scoreBoardService.StartGame(homeTeam, awayTeam);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 1, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 2, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 2, 1);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 2, 2);

            homeTeam = "Uruguay";
            awayTeam = "Italy";
            scoreBoardService.StartGame(homeTeam, awayTeam);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 1, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 2, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 3, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 4, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 5, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 6, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 6, 1);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 6, 2);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 6, 3);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 6, 4);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 6, 5);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 6, 6);

            homeTeam = "Argentina";
            awayTeam = "Australia";
            scoreBoardService.StartGame(homeTeam, awayTeam);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 1, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 2, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 3, 0);
            scoreBoardService.UpdateScore(homeTeam, awayTeam, 3, 1);

            Console.WriteLine();
            Console.WriteLine("Scoreboard initialized");
            ReturnToMenu();
        }

        private static void OptionGetSummary()
        {
            var scores = scoreBoardService.GetSummary();

            Console.Clear();
            Console.WriteLine("Get Summary");
            Console.WriteLine("-----------");
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
            Console.WriteLine("------------");
            Console.WriteLine();
            string homeTeam = ReadArgument("Home Team");
            string awayTeam = ReadArgument("Away Team");
            string homeScoreString = ReadArgument("Home Score");
            int homeScore = ConvertToInt(homeScoreString);
            string awayScoreString = ReadArgument("Away Score");
            int awayScore = ConvertToInt(awayScoreString);

            scoreBoardService.UpdateScore(homeTeam, awayTeam, homeScore, awayScore);

            Console.WriteLine();
            Console.WriteLine("Score Updated");
            ReturnToMenu();
        }

        private static void OptionFinishGame()
        {
            Console.Clear();
            Console.WriteLine("Finish a game");
            Console.WriteLine("-------------");
            Console.WriteLine();
            string homeTeam = ReadArgument("Home Team");
            string awayTeam = ReadArgument("Away Team");

            scoreBoardService.FinishGame(homeTeam, awayTeam);

            Console.WriteLine();
            Console.WriteLine("Game finished");
            ReturnToMenu();
        }

        private static void OptionStartGame()
        {
            Console.Clear();
            Console.WriteLine("Start a game");
            Console.WriteLine("------------");
            Console.WriteLine();
            string homeTeam = ReadArgument("Home Team");
            string awayTeam = ReadArgument("Away Team");

            scoreBoardService.StartGame(homeTeam, awayTeam);

            Console.WriteLine();
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
                throw new ArgumentException("Score is not right");
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