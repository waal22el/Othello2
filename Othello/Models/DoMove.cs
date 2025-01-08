using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Models
{
    public partial class GameBoard
    {
        public void DoMove(int[] tile, Player player)
        {
            int playerColor = player.Color;
            Board[tile[0], tile[1]] = playerColor; // First adds the player tile to the chosen tile
            List<int[]> TilesToFlip = new List<int[]>();

            TilesToFlip = FindTilesToFlip(TilesToFlip, tile, playerColor);

            foreach (int[] tileToFlip in TilesToFlip)
            {
                Board[tileToFlip[0], tileToFlip[1]] = playerColor;
            }
        }
    }
}
