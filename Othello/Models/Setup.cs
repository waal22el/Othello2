using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Models
{
    public partial class GameBoard
    {
        public int[,] Board { get; set; } // A matrix describing the current game board.
                                           // numbers mean as following: 0 = empty. 1 = black, 2 = white
        public Player Player1 = new Player(1); //player 1 is black
        public Player Player2 = new Player(2); //player 2 is white
        public GameBoard()
        {
            Board = SetBoard();
            Player1 = new Player(1); //player 1 is black
            Player2 = new Player(2); //player 2 is white
        }

        private int[,] SetBoard()
        {
            int[,] board = { {0, 0, 0, 0, 0, 0, 0, 0},
                             {0, 0, 0, 0, 0, 0, 0, 0},
                             {0, 0, 0, 0, 0, 0, 0, 0},
                             {0, 0, 0, 2, 1, 0, 0, 0},
                             {0, 0, 0, 1, 2, 0, 0, 0},
                             {0, 0, 0, 0, 0, 0, 0, 0},
                             {0, 0, 0, 0, 0, 0, 0, 0},
                             {0, 0, 0, 0, 0, 0, 0, 0}};
            

            return board;
        }
    }
}
