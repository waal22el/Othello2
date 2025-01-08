using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Models
{
    public partial class GameBoard
    {
        private Player determineWinner()
        {
            int player1Points = 0, player2Points = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Board[i,j] == Player1.Color)
                    {
                        player1Points++;
                    }
                    else if (Board[i, j] == Player2.Color)
                    {
                        player2Points++;
                    }
                }
            }

            if (player1Points > player2Points)
            {
                return Player1;
            }
            else if (player2Points > player1Points)
            {
                return Player2;
            }
            else
            {
                return null;
            }

        }
    }
}
