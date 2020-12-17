using System;

namespace RpsGame_NoDB
{
    public class Player
    {

        private Guid playerId = Guid.NewGuid();
        public Guid PlayerId
        {
            get => playerId;
        }

        private string fname;
        public string FirstName
        {
            get => fname;
            set
            {
                if (value is string && value.Length < 20 && value.Length > 1)
                {
                    fname = value;
                }
                else
                {
                    throw new Exception("Name you entered isn't valid.");
                }
            }
        }
        private string lname;
        public string LastName
        {
            get => lname;
            set
            {
                if (value is string && value.Length < 20 && value.Length > 1)
                {
                    lname = value;
                }
                else
                {
                    throw new Exception("Name you entered isn't valid.");
                }
            }
        }
        private int numWins;
        private int numLosses;

        public Player(string fname = null, string lname = null)
        {
            this.fname = fname;
            this.lname = lname;
        }
        public void AddWin()
        {
            numWins++;
        }

        public void AddLoss()
        {
            numLosses++;
        }

        public Array GetWinLossRecord()
        {
            int[] record = new int[2];
            record[0] = numWins;
            record[1] = numLosses;
            return record;
        }


    }
}