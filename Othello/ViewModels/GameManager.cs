using Othello.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using Othello.Views;



namespace Othello.ViewModels
{
    public class GameWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected GameManager _gameManager;

        public ICommand NewGameCommand { get; }

        public GameWindowViewModel()
        {
            _gameManager = new GameManager();

            GameGrid = new ObservableCollection<ObservableCollection<string>>();
            for (int i = 0; i < 8; i++)
            {
                var row = new ObservableCollection<string>();
                for (int j = 0; j < 8; j++)
                {
                    row.Add("");
                }
                GameGrid.Add(row);
            }
            NewGameCommand = new RelayCommand(NewGame);

        }
        private void InitializePlayers(string player1Name, string player2Name, string player1Type, string player2Type)
        {
            var player1Disk = Player.Disk.Black;
            var player2Disk = Player.Disk.White;

            _gameManager = new GameManager();
        }
        public void NewGame()
        {

            var setupDialog = new SetUpGameDialog();
            bool? dialogResult = setupDialog.ShowDialog();

            if (dialogResult == true)
            {
                string player1Name = setupDialog.Player1Name;
                string player2Name = setupDialog.Player2Name;
                string player1Type = setupDialog.Player1Type;
                string player2Type = setupDialog.Player2Type;

                InitializePlayers(player1Name, player2Name, player1Type, player2Type);

                _gameManager.StartNewGame();
                UpdateGameGrid();
                UpdateScores();
                CurrentPlayerName = _gameManager.CurrentPlayer.Name;
            }
        }

        public async void OnTileClicked(TileClickEventArgs e)
        {
            if (_gameManager.CurrentPlayer is HumanPlayer)
            {
                var validMove = _gameManager.RequestMove(e.Row, e.Column);

                if (validMove)
                {
                    UpdateGameGrid();
                    UpdateScores();
                    await HandleNextTurn(); // Switch turns and handle computer moves if applicable
                }
                else
                {
                    MessageBox.Show("Invalid move. Try again.");
                }
            }
        }
        public void NotifyGameGridUpdate(GameGrid gameGrid)
        {
            gameGrid.UpdateGameBoard(_gameManager.GetBoardState());
        }

        public class GameManager
        {
            private Player _player1; 
            private Player _player2; 
            private GameBoard _gameBoard;
            private Player _currentPlayer; 
            private Player.Disk[,] _boardState;
            private GameManager _gameManager;
            

            public Player Player1 => _player1;
            public Player Player2 => _player2;

            public GameManager()
            {
                _gameBoard = new GameBoard();
                _player1 = new HumanPlayer("Player 1", Player.Disk.Black);
                _player2 = new ComputerPlayer("Player 2", Player.Disk.White);
                _currentPlayer = _player1;
                _boardState = new Player.Disk[8, 8];
                InitializeBoardState();
            }
            private void InitializeBoardState()
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        _boardState[i, j] = Player.Disk.Empty;
                    }
                }
                _boardState[3, 3] = Player.Disk.White;
                _boardState[3, 4] = Player.Disk.Black;
                _boardState[4, 3] = Player.Disk.Black;
                _boardState[4, 4] = Player.Disk.White;
            }


            public void StartNewGame()
            {
                _currentPlayer = _player1;
                InitializeBoardState();
            }

            public bool RequestMove(int row, int column)
            {
                var diskValue = _currentPlayer.PlayerDisk == Player.Disk.Black ? 0 : 1;

                List<int[]> validMoves = _gameBoard.findValidMoves(diskValue);

                if (validMoves.Any(m => m[0] == row && m[1] == column))
                {
                    _boardState[row, column] = _currentPlayer.PlayerDisk; 
                    _gameBoard.DoMove(new int[] { row, column }, _currentPlayer.ModelPlayer);
                    SwitchPlayer();
                    return true;
                }
                MessageBox.Show($"Invalid move at ({row},{column})");
                return false;
            }

            public void SwitchPlayer()
            {
                _currentPlayer = _currentPlayer == _player1 ? _player2 : _player1;
            }
            public bool IsGameOver()
            {
                int player1Color = _gameManager.Player1.PlayerDisk == Player.Disk.Black ? 0 : 1;
                int player2Color = _gameManager.Player2.PlayerDisk == Player.Disk.Black ? 0 : 1;

                return !_gameManager.GameBoard.findValidMoves(player1Color).Any() &&
                       !_gameManager.GameBoard.findValidMoves(player2Color).Any();
            }
            public Player CurrentPlayer => _currentPlayer;
            public Player.Disk[,] GetBoardState()
            {
                return _boardState;
            }
            public GameBoard GameBoard => _gameBoard;
            public int GetDiskCount(Player.Disk disk)
            {
                int count = 0;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (_boardState[i, j] == disk)
                        {
                            count++;
                        }
                    }
                }
                return count;
            }
            public void MakeComputerMove()
            {
                int diskValue = _player2.PlayerDisk == Player.Disk.Black ? 0 : 1;

                List<int[]> validMoves = _gameBoard.findValidMoves(diskValue);

                if (validMoves.Count > 0)
                {
                    Random random = new Random();
                    int randomIndex = random.Next(validMoves.Count);

                    int[] move = validMoves[randomIndex];

                    int row = move[0];
                    int column = move[1];

                    RequestMove(row, column);
                }
            }
        }

        public ObservableCollection<ObservableCollection<string>> GameGrid { get; set; }
        private string _currentPlayerName = "Player 1";
        public string CurrentPlayerName
        {
            get => _currentPlayerName;
            set
            {
                _currentPlayerName = value;
                OnPropertyChanged(nameof(CurrentPlayerName));
            }
        }
        private int _blackScore = 0;
        public int BlackScore
        {
            get => _blackScore;
            set
            {
                _blackScore = value;
                OnPropertyChanged(nameof(BlackScore));
            }
        }
        private int _whiteScore = 0;
        public int WhiteScore
        {
            get => _whiteScore;
            set
            {
                _whiteScore = value;
                OnPropertyChanged(nameof(WhiteScore));
            }
        }

        private void UpdateGameGrid()
        {
            Player.Disk[,] boardState = _gameManager.GetBoardState();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    GameGrid[i][j] = boardState[i, j] switch
                    {
                        Player.Disk.Black => "B",
                        Player.Disk.White => "W",
                        _ => ""
                    };
                }
            }
        }

        private void UpdateScores()
        {
            BlackScore = _gameManager.GetDiskCount(Player.Disk.Black);
            WhiteScore = _gameManager.GetDiskCount(Player.Disk.White);
        }

        private async Task HandleNextTurn()
        {
            if (_gameManager.IsGameOver())
            {
                MessageBox.Show("The game is over!");
            }
            else
            {
                _gameManager.SwitchPlayer();
                CurrentPlayerName = _gameManager.CurrentPlayer.Name;

                if (_gameManager.CurrentPlayer is ComputerPlayer)
                {
                    await Task.Run(() => _gameManager.MakeComputerMove());
                    UpdateGameGrid();
                    UpdateScores();
                    await HandleNextTurn();
                }
            }
        }
        public bool IsGameOver()
        {
            return false;
        }

    }

    public class TileClickEventArgs : EventArgs
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;
        public void Execute(object? parameter) => _execute();
        public event EventHandler? CanExecuteChanged;
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T?, bool>? _canExecute;

        public RelayCommand(Action<T> execute, Func<T?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke((T?)parameter) ?? true;
        }

        public void Execute(object? parameter)
        {
            _execute((T)parameter!);
        }

        public event EventHandler? CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}