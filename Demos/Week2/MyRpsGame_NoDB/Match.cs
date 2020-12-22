using System;
using System.Collections.Generic;

namespace MyRpsGame_NoDB
{
    public class Match
    {
        private Guid matchId = Guid.NewGuid();
        public Guid MatchId
        {
            get => matchId;
        }
        public Player Bot { get; set; } //computer
        public Player User { get; set; } //user

        public List<Round> Rounds = new List<Round>();

        //private int userRoundWins;
        public int UserRoundWins { get; set; }

        //private int botRoundWins;
        public int BotRoundWins { get; set; }
        private int ties { get; set; }

        public void RoundWinner()
        {
            ties++;
        }
        public void RoundWinner(Player p = null)
        {
            if (p.PlayerId == Bot.PlayerId)
            {
                BotRoundWins++;
            }
            else if (p.PlayerId == User.PlayerId)
            {
                UserRoundWins++;
            }
            else
            {
                ties++;
            }
        }

        public Player MatchWinner()
        {
            if (BotRoundWins == 2)
            {
                return Bot;
            }
            else if (UserRoundWins == 2)
            {
                return User;
            }
            else
            {
                return null;
            }
        }






    }
}