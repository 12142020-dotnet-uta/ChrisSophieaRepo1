using System;
using System.Collections;
using System.Collections.Generic;

namespace MyRpsGame_NoDB
{
    class Program
    {
        static RpsGameRepositoryLayer gameContext = new RpsGameRepositoryLayer();
        static int roundNumber;
        static Match match;
        static Player user;
        static Player bot = new Player("bot", "player");
        static void Main(string[] args)
        {

            while (true)
            {

                int loginOrQuit = gameContext.MainMenuResponse();
                if (loginOrQuit == -1)
                {
                    Console.WriteLine("Invalid input. Select 1 or 2.");
                    continue;
                }
                else if (loginOrQuit == 2)
                {
                    break;
                }

                user = gameContext.LoginMenuResponse();

                while (true)
                {
                    int gameMenuResponse = gameContext.GameMenuResponse();
                    if (gameMenuResponse < 1 || gameMenuResponse > 2)
                    {
                        Console.WriteLine("Invalid input. Pick option 1 or 2.");
                        continue;
                    }
                    else if (gameMenuResponse == 2)
                    {
                        break;
                    }


                }

            }



            public static void PlayGame()
            {
                match = gameContext.StartNewMatch(bot, user);
                roundNumber = 1;
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
                    gameContext.rounds.Add(round);
                    roundNumber++;
                    match.Rounds.Add(round);

                    if (match.UserRoundWins == 2 || match.BotRoundWins == 2)
                    {
                        gameContext.matches.Add(match);
                        Console.Write($"\nMatch winner: {match.MatchWinner().FirstName}");
                        gameContext.PrintStats();

                        Console.WriteLine("\n\nWould you like to play another match?\n\tType y for Yes\n\tType n for No\n");
                        string playAgain = Console.ReadLine(); // User input and add into playAgain
                        if (playAgain.Equals("y", StringComparison.OrdinalIgnoreCase) || playAgain.Equals("yes", StringComparison.OrdinalIgnoreCase))
                        {
                            match = gameContext.StartNewMatch(bot, user);
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
    }


