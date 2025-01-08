using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Models
{
    public partial class GameBoard
    {
        private List<int[]> FindTilesToFlip(List<int[]> TilesToFlip, int[] tile, int player)
        {
            List<int[]> tempList = new List<int[]>();

            tempList = FindTilesToFlipRecursion(TilesToFlip, tile, player, new int[] { -1, -1 }); // Check NW
            if (tempList != null)
            {
                TilesToFlip = tempList;
            }

            tempList = FindTilesToFlipRecursion(TilesToFlip, tile, player, new int[] { -1, 0 }); // Check N
            if (tempList != null)
            {
                TilesToFlip = tempList;
            }

            tempList = FindTilesToFlipRecursion(TilesToFlip, tile, player, new int[] { -1, 1 }); // Check NE
            if (tempList != null)
            {
                TilesToFlip = tempList;
            }

            tempList = FindTilesToFlipRecursion(TilesToFlip, tile, player, new int[] { 0, 1 }); // Check E
            if (tempList != null)
            {
                TilesToFlip = tempList;
            }

            tempList = FindTilesToFlipRecursion(TilesToFlip, tile, player, new int[] { 1, 1 }); // Check SE
            if (tempList != null)
            {
                TilesToFlip = tempList;
            }

            tempList = FindTilesToFlipRecursion(TilesToFlip, tile, player, new int[] { 1, 0 }); // Check S
            if (tempList != null)
            {
                TilesToFlip = tempList;
            }

            tempList = FindTilesToFlipRecursion(TilesToFlip, tile, player, new int[] { 1, -1 }); // Check SW
            if (tempList != null)
            {
                TilesToFlip = tempList;
            }

            tempList = FindTilesToFlipRecursion(TilesToFlip, tile, player, new int[] { 0, -1 }); // Check W
            if (tempList != null)
            {
                TilesToFlip = tempList;
            }

            return TilesToFlip;
        }

        private List<int[]> FindTilesToFlipRecursion(List<int[]> TilesToFlip, int[] currentTile, int player, int[] direction)
        {
            try
            {
                int[] nextTile = { currentTile[0] + direction[0], currentTile[1] + direction[1] };
                List<int[]> tempList = new List<int[]>();

                if (Board[nextTile[0], nextTile[1]] == player) //if the tile we are looking at is the players brick
                {
                    return TilesToFlip; //stop checking
                }
                else if (Board[nextTile[0], nextTile[1]] == 0) //if the next tile is empty
                {
                    return null;
                }
                else //else (if it is opponents brick)
                {
                    tempList = FindTilesToFlipRecursion(TilesToFlip, nextTile, player, direction);
                    if (tempList != null)
                    {
                        TilesToFlip = tempList;
                        TilesToFlip.Add(nextTile);
                    }
                    return TilesToFlip;
                }
            }
            catch (Exception e) //if we get an IndexOutOfRangeException we keep going as the current direction is invalid
            {
                if (!(e is IndexOutOfRangeException)) //if we get some other exception throw it
                {
                    throw;
                }
                return null;
            }

        }
    }
}
