namespace TicTacToe.API.Game
{
    public class GameBoard
    {
        private const int BoardSize = 3;

        private int[,] _board = new int[BoardSize, BoardSize];

        public int[,] Board => _board;

        public void Initialize()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    _board[i, j] = (int)GameBoardCell.EMPTY;
                }
            }
        }

        public void LoadBoardValues(int[,] values)
        {
            _board = values;
        }

        public bool TryChangeBoardCell(PlayerMove playerMove)
        {
            bool moveMade;

            if (playerMove.X < BoardSize && playerMove.Y < BoardSize && playerMove.X >= 0 && playerMove.Y >= 0)
            {
                moveMade = true;

                int boardCell = _board[playerMove.X, playerMove.Y];

                if (boardCell == (int)GameBoardCell.EMPTY)
                {
                    _board[playerMove.X, playerMove.Y] = (int)playerMove.PlayerType;
                }
                else moveMade = false;
            }
            else
            {
                moveMade = false;
            }

            return moveMade;
        }

        public bool CheckPlayerWon(PlayerType playerType)
        {
            if (_board[0, 0] == _board[1, 1] && _board[1, 1] == _board[2, 2] && _board[0, 0] == (int)playerType)
            {
                return true;
            }

            if (_board[2, 0] == _board[1, 1] && _board[1, 1] == _board[0, 2] && _board[1, 1] == (int)playerType)
            {
                return true;
            }

            for (int i = 0; i < BoardSize; i++)
            {
                if (_board[i, 0] == _board[i, 1] && _board[i, 1] == _board[i, 2] && _board[i, 0] == (int)playerType)
                {
                    return true;
                }
            }

            for (int i = 0; i < BoardSize; i++)
            {
                if (_board[0, i] == _board[1, i] && _board[1, i] == _board[2, i] && _board[0, i] == (int)playerType)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckGameDrawn()
        {
            int usedCellsCount = 0;

            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (_board[i, j] != (int)GameBoardCell.EMPTY)
                    {
                        usedCellsCount++;
                    }
                }
            }

            return usedCellsCount == _board.Length;
        }
    }
}
