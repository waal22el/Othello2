using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Models
{
    public partial class GameBoard
    {

        private bool endCheck(int[] player1Move, int[] player2Move)
        {
            if (player1Move == null && player2Move == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
