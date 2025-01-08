using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;

namespace Othello.ViewModels
{
    public class ComputerPlayer : Player
    {
        private Random _random;

        public ComputerPlayer(string name, Player.Disk disk) : base(name, disk)
        {
            _random = new Random();
        }
        public override int[] MakeMove(List<int[]> validMoves)
        {
            if (validMoves.Count > 0)
            {
                int index = _random.Next(validMoves.Count);
                return validMoves[index]; 
            }
            return null; 
        }
    }
}