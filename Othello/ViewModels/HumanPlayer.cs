using System.Collections.Generic;
using System.Numerics;

namespace Othello.ViewModels
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, Disk disk) : base(name, disk)
        {
            Score = 0;
        }

        public override int[] MakeMove(List<int[]> validMoves)
        {
            return validMoves.FirstOrDefault();
        }
    }
}