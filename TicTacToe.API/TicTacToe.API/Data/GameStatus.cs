using TicTacToe.API.Game;

namespace TicTacToe.API.Data
{
    [Serializable]
    public class GameStatus
    {
        public int[,] BoardValues { get; set; }

        public PlayerType NextPlayer { get; set; }

        public GameState GameState { get; set; }

        public string Status { get; set; }
    }
}
