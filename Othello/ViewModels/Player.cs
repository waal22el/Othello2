using Othello.Models;
using System;

namespace Othello.ViewModels
{
    public abstract class Player
    {
        public Models.Player ModelPlayer { get; }
        public enum Disk
        {
            Black,
            White,
            Empty
        }
        public string Name { get; }
        public Disk PlayerDisk { get; }
        public int Score { get; set; }

      
        protected Player(string name, Disk disk)
        {
            Name = name;
            PlayerDisk = disk;
            Score = 0;
        }

        public abstract int[] MakeMove(List<int[]> validMoves);
    }
}