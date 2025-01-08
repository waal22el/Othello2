﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.Models
{
    public partial class GameBoard
    {
        public List<int[]> findValidMoves(int player)
        {
            int opponent;
            List<int[]> Validmoves = new List<int[]>();
            int[] tryDirection = null;

            if (player == 1) //if the player is white 
            {
                opponent = 2; //opponent is black
            }
            else // else player is black
            {
                opponent = 1; //opponent is white
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (this.Board[i, j] == 0) // if the tile is empty start checking valid moves
                    {                          // You cannot place a brick on a tile that already has a brick on it
                        tryDirection = CheckValidDirection(new int[] { i, j }, new int[] { -1, -1 }, player, opponent, false); // Check NW
                        if (tryDirection != null)
                        {
                            Validmoves.Add(tryDirection);
                        }

                        if (tryDirection == null) //if there is already a direction that causes a valid move there is no reason to check the rest
                        {
                            tryDirection = CheckValidDirection(new int[] { i, j }, new int[] { -1, 0 }, player, opponent, false); // Check N
                            if (tryDirection != null)
                            {
                                Validmoves.Add(tryDirection);
                            }
                        }

                        if (tryDirection == null) //if there is already a direction that causes a valid move there is no reason to check the rest
                        {
                            tryDirection = CheckValidDirection(new int[] { i, j }, new int[] { -1, 1 }, player, opponent, false); // Check NE
                            if (tryDirection != null)
                            {
                                Validmoves.Add(tryDirection);
                            }
                        }

                        if (tryDirection == null) //if there is already a direction that causes a valid move there is no reason to check the rest
                        {
                            tryDirection = CheckValidDirection(new int[] { i, j }, new int[] { 0, 1 }, player, opponent, false); // Check E
                            if (tryDirection != null)
                            {
                                Validmoves.Add(tryDirection);
                            }
                        }

                        if (tryDirection == null) //if there is already a direction that causes a valid move there is no reason to check the rest
                        {
                            tryDirection = CheckValidDirection(new int[] { i, j }, new int[] { 1, 1 }, player, opponent, false); // Check SE
                            if (tryDirection != null)
                            {
                                Validmoves.Add(tryDirection);
                            }
                        }

                        if (tryDirection == null) //if there is already a direction that causes a valid move there is no reason to check the rest
                        {
                            tryDirection = CheckValidDirection(new int[] { i, j }, new int[] { 1, 0 }, player, opponent, false); // Check S
                            if (tryDirection != null)
                            {
                                Validmoves.Add(tryDirection);
                            }
                        }

                        if (tryDirection == null) //if there is already a direction that causes a valid move there is no reason to check the rest
                        {
                            tryDirection = CheckValidDirection(new int[] { i, j }, new int[] { 1, -1 }, player, opponent, false); // Check SW
                            if (tryDirection != null)
                            {
                                Validmoves.Add(tryDirection);
                            }
                        }

                        if (tryDirection == null) //if there is already a direction that causes a valid move there is no reason to check the rest
                        {
                            tryDirection = CheckValidDirection(new int[] { i, j }, new int[] { 0, -1 }, player, opponent, false); // Check W
                            if (tryDirection != null)
                            {
                                Validmoves.Add(tryDirection);
                            }
                        }

                        tryDirection = null; //reset back to null for next tile
                    }
                }
            }

            return Validmoves;
        }

        private int[] CheckValidDirection(int[] currentTile, int[] direction, int player, int opponent, bool passedOther)
        {
            int[] nextTile = { currentTile[0] + direction[0], currentTile[1] + direction[1] }; // this variable is not strictly needed but improves readability significantly
            try
            {
                if (this.Board[nextTile[0], nextTile[1]] == player) //if the next tile is the players tile and we have passed a tile of opponent
                {
                    if (passedOther) //if we have passed a tile of the opponent it's a valid move
                    {
                        return new int[] { 0, 0 }; //doesn't relly matter what we send back as long as it isn't null
                    }
                    else //otherwise this direction does not lead to a valid move
                    {
                        return null;
                    }
                }
                else if (this.Board[nextTile[0], nextTile[1]] == opponent) //if the next tile is an opponent
                {
                    if (CheckValidDirection(nextTile, direction, player, opponent, true) != null) //true because we have now passed our opponent
                    {
                        return currentTile;
                    }
                    return null;
                }
                else //if the next tile is empty
                {
                    return null; //this direction does not lead to a valid move
                }
            }
            catch (Exception e) //if we get an IndexOutOfRangeException we keep going as the current tile and direction does not lead to a valid move
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