using System;
using System.Collections;
using System.Collections.Generic;


namespace MyRpsGame_NoDB
{
    public class RpsGameRepositoryLayer
    {
        public List<Player> players = new List<Player>();
        public List<Round> rounds = new List<Round>();
        public List<Match> matches = new List<Match>();

        public Player CreatePlayer(string fname = "null", string lname = "null")
        {
            Player player = new Player(fname, lname);

            if (!PlayerExists(player, players))
            {
                Console.WriteLine($"\nUser created: {player.FirstName} {player.LastName}.");
                players.Add(player);
                return player;
            }
            else
            {
                return player;
            }


            bool PlayerExists(Player p, List<Player> l)
            {
                foreach (Player pl in l)
                {
                    if (pl.FirstName == p.FirstName && pl.LastName == p.LastName)
                    {
                        Console.WriteLine($"\nUser '{pl.FirstName} {pl.LastName}' already exists. Logging in.");
                        return true;
                    }
                }
                return false;
            }

        }

        public int AnswerToInt(string input)
        {
            int result;
            bool parsedSuccessful = int.TryParse(input, out result);
            if (parsedSuccessful == false)
            {
                Console.WriteLine("Invalid input.");
                return -1;
            }
            else
            {
                return result;
            }
        }

        public int MainMenuResponse()
        {
            Console.WriteLine("\n----- Main Menu -----");
            Console.WriteLine("\nPlease choose an option.");
            Console.WriteLine("\t1. Login \n\t2. Quit");

            int loginOrQuit = AnswerToInt(Console.ReadLine());
            return loginOrQuit;

        }

        public Player LoginMenuResponse()
        {
            Console.WriteLine("\n----- Login -----");
            Console.Write("\nEnter first name: ");
            string firstName = Console.ReadLine();
            Console.Write("\nEnter last name: ");
            string lastName = Console.ReadLine();

            return CreatePlayer(firstName, lastName);
        }
        public int GameMenuResponse()
        {
            Console.WriteLine("\n----- Game Menu -----");
            Console.WriteLine("\nPlease choose an option.");
            Console.WriteLine("\t1. Play Game\n\t2. Logout");
            return AnswerToInt(Console.ReadLine());
        }

        public Match StartNewMatch(Player p1, Player p2)
        {
            Match match = new Match();
            rounds = new List<Round>();
            match.Bot = p1;
            match.User = p2;
            return match;
        }

        public void PrintStats()
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