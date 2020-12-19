using System;
using System.Collections;
using System.Collections.Generic;

namespace RpsGame_NoDB
{
    class Program
    {

        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();
            List<Round> rounds = new List<Round>();
            List<Match> matches = new List<Match>();
            int roundNumber = 1;

            Player bot = new Player()
            {
                FirstName = "bot",
                LastName = "player"
            };
            players.Add(bot);

            while (true)
            {


                Console.WriteLine("----- Main Menu -----");
                Console.WriteLine("\nPlease choose an option.");
                Console.WriteLine("\t1. Login \n\t2. Quit");

                if (!int.TryParse(Console.ReadLine(), out int menuResponse) || menuResponse < 1 || menuResponse > 2)
                {
                    Console.WriteLine("Invalid input. Pick option 1 or 2.");
                    continue;
                }
                else if (menuResponse == 2)
                {
                    break;
                }

                Console.WriteLine("\n----- Login -----");
                Console.Write("\nEnter first name: ");
                string firstName = Console.ReadLine();
                Console.Write("\nEnter last name: ");
                string lastName = Console.ReadLine();
                Player user = new Player(firstName, lastName);

                if (!PlayerExists(user, players))
                {
                    Console.WriteLine("\nUser created.");
                    players.Add(user);
                }


                while (true)
                {
                    Console.WriteLine("\n----- Game Menu -----");
                    Console.WriteLine("\nPlease choose an option.");
                    Console.WriteLine("\t1. Play Game\n\t2. Logout");
                    if (!int.TryParse(Console.ReadLine(), out int gameMenuResponse) || gameMenuResponse < 1 || gameMenuResponse > 2)
                    {
                        Console.WriteLine("Invalid input. Pick option 1 or 2.");
                        continue;
                    }
                    else if (gameMenuResponse == 2)
                    {
                        break;
                    }

                    PlayGame();

                }



                void PlayGame()
                {
                    Match match = new Match();
                    rounds = new List<Round>();
                    roundNumber = 1;
                    match.Bot = bot;
                    match.User = user;

                    while (true)
                    {


                        Round round = new Round();
                        Choice userSelection;
                        int botSelection;
                        string userResponse;

                        Console.WriteLine($"\nStarting Round {roundNumber}...");
                        Console.WriteLine("\nPlease choose Rock, Paper, or Scissors by typing 1, 2, or 3 and hitting enter.");
                        Console.WriteLine("\t1. Rock \n\t2. Paper \n\t3. Scissors");
                        userResponse = Console.ReadLine();

                        // parse the users input to an int

                        if (!Choice.TryParse(userResponse, out userSelection) || (int)userSelection > 3 || (int)userSelection < 1) // give a message if the users input was invalid
                        {
                            Console.WriteLine("Your response is invalid.");
                            continue;
                        }
                        round.UserChoice = userSelection;

                        Random rnd = new Random();
                        botSelection = rnd.Next(1, 4);

                        round.BotChoice = (Choice)botSelection;

                        switch (round.UserChoice)
                        {
                            case Choice.Rock:
                                switch (round.BotChoice)
                                {
                                    case Choice.Rock:
                                        Console.WriteLine("Tie. You chose rock. Bot chose rock.");
                                        match.RoundWinner();
                                        break;
                                    case Choice.Paper:
                                        Console.WriteLine("Lose. You chose rock. Bot chose paper.");
                                        round.WinningPlayer = bot;
                                        match.RoundWinner(bot);
                                        break;
                                    default:
                                        Console.WriteLine("Win. You chose rock. Bot chose scissors.");
                                        round.WinningPlayer = user;
                                        match.RoundWinner(user);
                                        break;
                                }
                                break;
                            case Choice.Paper:
                                switch (round.BotChoice)
                                {
                                    case Choice.Rock:
                                        Console.WriteLine("Win. You chose paper. Bot chose rock.");
                                        round.WinningPlayer = user;
                                        match.RoundWinner(user);
                                        break;
                                    case Choice.Paper:
                                        Console.WriteLine("Tie. You chose paper. Bot chose paper.");
                                        match.RoundWinner();
                                        break;
                                    default:
                                        Console.WriteLine("Lose. You chose paper. Bot chose scissors.");
                                        round.WinningPlayer = bot;
                                        match.RoundWinner(bot);
                                        break;
                                }
                                break;
                            default:
                                switch (round.BotChoice)
                                {
                                    case Choice.Rock:
                                        Console.WriteLine("Lose. You chose scissors. Bot chose rock.");
                                        round.WinningPlayer = bot;
                                        match.RoundWinner(bot);
                                        break;
                                    case Choice.Paper:
                                        Console.WriteLine("Win. You chose scissors. Bot chose paper.");
                                        round.WinningPlayer = user;
                                        match.RoundWinner(user);
                                        break;
                                    default:
                                        Console.WriteLine("Tie. You chose scissors. Bot chose scissors.");
                                        match.RoundWinner();
                                        break;
                                }
                                break;
                        }
                        rounds.Add(round);
                        roundNumber++;
                        match.Rounds.Add(round);

                        if (match.UserRoundWins == 2 || match.BotRoundWins == 2)
                        {
                            matches.Add(match);
                            Console.Write($"\nMatch winner: {match.MatchWinner().FirstName}");
                            PrintStats();

                            Console.WriteLine("\n\nWould you like to play another match?\n\tType y for Yes\n\tType n for No\n");
                            string playAgain = Console.ReadLine(); // User input and add into playAgain
                            if (playAgain.Equals("y", StringComparison.OrdinalIgnoreCase) || playAgain.Equals("yes", StringComparison.OrdinalIgnoreCase))
                            {
                                match = new Match();
                                rounds = new List<Round>();
                                match.Bot = bot;
                                match.User = user;
                                roundNumber = 1;
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("\nGood bye.");
                                break;
                            }

                        }
                        else
                        {
                            continue;
                        }

                    }
                }





            }

            bool PlayerExists(Player p, List<Player> l)
            {
                foreach (Player pl in l)
                {
                    if (pl.FirstName == p.FirstName && pl.LastName == p.LastName)
                    {
                        Console.WriteLine("\nUser already exists. Logging In.");
                        return true;
                    }
                }
                return false;
            }

            void PrintStats()
            {
                Console.WriteLine("\nRound Winner List:");
                int counter = 1;
                foreach (Round r in rounds)
                {

                    if (r.WinningPlayer == null)
                    {
                        Console.WriteLine($"Round #{counter}: Was a tie");
                    }
                    else
                    {
                        Console.WriteLine($"Round #{counter}: {r.WinningPlayer.FirstName}");
                    }
                    counter++;
                }
            }
        }




    }

}

