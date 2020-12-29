using System;

namespace RpsGame_NoDB
{
    public class Round
    {
        private Guid roundId = Guid.NewGuid();
        public Guid RoundId
        {
            get => roundId;
        }
        public Choice BotChoice { get; set; }
        public Choice UserChoice { get; set; }
        public Player WinningPlayer { get; set; } = null;


    }
}