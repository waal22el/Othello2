using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Othello.ViewModels;

namespace Othello.Views
{
    public partial class GameGrid : UserControl
    {
        private const int BoardSize = 8;

        public GameGrid()
        {
            InitializeComponent();
            InitializeGameBoard();
        }

        private void InitializeGameBoard()
        {
            GameBoardGrid.RowDefinitions.Clear();
            GameBoardGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < BoardSize; i++)
            {
                GameBoardGrid.RowDefinitions.Add(new RowDefinition());
                GameBoardGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    var cell = new Border
                    {
                        Background = Brushes.Green,
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1)
                    };
                    cell.MouseLeftButtonDown += (s, e) => OnTileClicked(row, col);

                    Grid.SetRow(cell, row);
                    Grid.SetColumn(cell, col);
                    GameBoardGrid.Children.Add(cell);
                }
            }
        }

        private void OnTileClicked(int row, int column)
        {
            if (DataContext is GameWindowViewModel viewModel)
            {
                viewModel.OnTileClicked(new TileClickEventArgs { Row = row, Column = column });
            }
        }

        public void UpdateGameBoard(Player.Disk[,] boardState)
        {
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    var cell = (Border)GameBoardGrid.Children[row * BoardSize + col];

                    if (cell.Child is Ellipse ellipse)
                    {
                        ellipse.Fill = boardState[row, col] switch
                        {
                            Player.Disk.Black => Brushes.Black,
                            Player.Disk.White => Brushes.White,
                            _ => Brushes.Transparent
                        };
                    }
                    else
                    {
                        cell.Child = new Ellipse
                        {
                            Width = 40,
                            Height = 40,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Fill = boardState[row, col] switch
                            {
                                Player.Disk.Black => Brushes.Black,
                                Player.Disk.White => Brushes.White,
                                _ => Brushes.Transparent
                            }
                        };
                    }
                }
            }
        }
    }
}

