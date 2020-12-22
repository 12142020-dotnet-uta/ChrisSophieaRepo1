using System;
using System.ComponentModel.DataAnnotations;

namespace RpsGameWithDb
{
    public class Round
    {
        private Guid roundID = Guid.NewGuid();
        [Key]
        public Guid RoundId { get { return roundID; } set { roundID = value; } }
        public Choice Player1Choice { get; set; } // always the computer
        public Choice Player2Choice { get; set; } // always the user
        public Player WinningPlayer { get; set; }
    }
}