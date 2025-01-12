using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Othello.ViewModels.Player;

namespace Othello.Models
{
    public partial class GameBoard
    {
        public Disk[,] Board { get; set; } // A matrix describing the current game board.
                                            

        public GameBoard()
        {
            Board = SetBoard();
        }

        private static Disk[,] SetBoard()
        {
            // numbers mean as following: 0 = empty. 1 = black, 2 = white
            int[,] tempBoard = { {0, 0, 0, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 1, 2, 0, 0, 0},
                                 {0, 0, 0, 2, 1, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0}};

            Disk[,] board = new Disk[8,8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    switch (tempBoard[i,j])
                    {
                        case(0):
                            board[i, j] = Disk.Empty;
                            break;

                        case(1):
                            board[i, j] = Disk.Black;
                            break;

                        case(2):
                            board[i, j] = Disk.White;
                            break;
                    }
                }
            }
            

            return board;
        }
    }
}
