using Newtonsoft.Json;

using TicTacToe.API.Data;

namespace TicTacToe.API.Game
{
    public class GameModel
    {
        private const string DataFileName = "GameStatusData.json";

        private const string DataPath = "./Data/" + DataFileName;

        private GameBoard _gameBoard;

        private PlayerType _currentPlayer;

        private GameState _gameState;

        private PlayerMove _playerMove;

        private GameStatus _gameStatus;

        private string _status;

        public GameModel(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
        }

        public PlayerType Player => _currentPlayer;

        public GameStatus GameStatus => _gameStatus;

        public GameState GameState => _gameState;

        public void ChangePlayerTurn(PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.X:
                    _currentPlayer = PlayerType.O;
                    break;
                case PlayerType.O:
                    _currentPlayer = PlayerType.X;
                    break;
            }
        }

        public void NewGame()
        {
            SelectRandomPlayer();

            _gameBoard.Initialize();

            _status = "Game has been started";

            _gameStatus = new GameStatus();

            _gameStatus.BoardValues = _gameBoard.Board;

            _gameStatus.NextPlayer = _currentPlayer;

            _gameStatus.Status = _status;

            SaveGameData();
        }

        public void MakeMove(int x, int y)
        {
            LoadData();

            if (_gameState == GameState.WON || _gameState == GameState.DRAWN)
            {
                _status = "Game is over. Please, start new game";

                SaveGameData();

                return;
            }

            _playerMove = new PlayerMove(x, y, _currentPlayer);

            if (_gameBoard.TryChangeBoardCell(_playerMove))
            {
                if (_gameBoard.CheckGameDrawn())
                {
                    _gameState = GameState.DRAWN;

                    _status = "Game finished with drawn result.";

                    SaveGameData();

                    return;
                }
                else if (_gameBoard.CheckPlayerWon(_currentPlayer))
                {
                    _gameState = GameState.WON;

                    _status = $"Player {_currentPlayer} won!";

                    SaveGameData();

                    return;
                }

                _gameState = GameState.PLAYER_MOVE;

                _status = $"Player {_currentPlayer} made move";

                ChangePlayerTurn(_currentPlayer);

                SaveGameData();
            }
            else
            {
                _gameState = GameState.INVALID_MOVE;

                _gameStatus.Status = "Ivalid player move. Game board coordinates must be in range: [0;2] and be in empty game board cell";
            }
        }

        public void ContinueGame()
        {
            LoadData();
        }

        private void SelectRandomPlayer()
        {
            Random random = new Random();

            Array values = Enum.GetValues(typeof(PlayerType));

            _currentPlayer = (PlayerType)values.GetValue(random.Next(values.Length));
        }

        private void SaveGameData()
        {
            if (_gameStatus == null)
                throw new Exception("Game has not been started. Please, start new game");

            _gameStatus.BoardValues = _gameBoard.Board;

            _gameStatus.NextPlayer = _currentPlayer;

            _gameStatus.GameState = _gameState;

            _gameStatus.Status = _status;

            string json = JsonConvert.SerializeObject(_gameStatus);

            File.WriteAllText(DataPath, json);
        }

        private void LoadData()
        {
            string json = string.Empty;

            if (File.Exists(DataPath))
            {
                json = File.ReadAllText(DataPath);

                _gameStatus = JsonConvert.DeserializeObject<GameStatus>(json);

                _gameBoard.LoadBoardValues(_gameStatus.BoardValues);

                _gameState = _gameStatus.GameState;

                _currentPlayer = _gameStatus.NextPlayer;
            }
            else
            {
                throw new FileNotFoundException("Game data file not found");
            }
        }
    }
}
