using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Models
{
    public partial class GameBoard
    {
        private void typeBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write($"{Board[i, j]} ");
                }
                Console.WriteLine("");
            }
        }
    }

    public class Player
    {
        public int Color { get; set; }

        public Player(int color)
        {
            Color = color;
        }
        public int[] MakeMove(List<int[]> possibleMoves)
        {
            Random rand = new Random();
            int count = possibleMoves.Count;

            if (count != 0)
            {
                return possibleMoves[rand.Next(0, count)];
            }
            else
            {
                return null;
            }
        }
    }
}
