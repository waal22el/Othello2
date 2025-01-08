using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Othello.Models
{
    public partial class GameBoard
    {
        
        public void run()
        {           
            int[] player1Move = null;
            int[] player2Move = null;

            while (true) 
            {
                typeBoard();
                Console.WriteLine("-------------------------------------");

                player1Move = Player1.MakeMove(findValidMoves(Player1.Color));
                if (player1Move != null )
                {
                    DoMove(player1Move, Player1);
                    player1Move = null;
                }
                // else player1 could not make a move

                Console.Clear();
                typeBoard();
                Console.WriteLine("-------------------------------------");

                player2Move = Player2.MakeMove(findValidMoves(Player2.Color));
                if (player2Move != null)
                {
                    DoMove(player2Move, Player2);
                }
                // else player2 could not make a move

                if (endCheck(player1Move, player2Move))
                {
                    Console.WriteLine("!!!Game has ended!!!");
                    Console.Clear();
                    typeBoard();
                    break;
                }

                Console.Clear();
            }


            Player Winner = determineWinner();
            if (Winner == null)
            {
                Console.WriteLine("It's a Draw!!!");
            }
            else
            {
                Console.WriteLine($"Winner is Color: {Winner.Color}");
            }
        }


        
    }
}
