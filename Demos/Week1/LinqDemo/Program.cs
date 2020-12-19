using System;
using System.Linq;
using System.Collections.Generic;

namespace LinqDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();

            for (int i = 0; i < 10; i++)
            {
                players.Add(new Player("Player-" + i, "" + (i - (i * 2))));
            }
            foreach (Player p in players)
            {
                Console.WriteLine($"First Name: {p.FirstName} | Last Name: {p.LastName}");
            }

            var result = players.Where(x => x.FirstName == "Player-4").FirstOrDefault();

            if (result != null)
            {
                Console.WriteLine($"Firstname: {result.FirstName} || Lastname: {result.LastName}");
            }
            else
            {
                throw new ArgumentNullException("player wasn't found");
            }

            int count = players.Count;
            players.Remove(result);

            result = players.Where(x => x.FirstName == "Player-4").FirstOrDefault();

            if (result != null)
            {
                Console.WriteLine($"Firstname: {result.FirstName} || Lastname: {result.LastName}");
            }
            else
            {
                throw new ArgumentNullException("player wasn't found");
            }

        }
    }
}
