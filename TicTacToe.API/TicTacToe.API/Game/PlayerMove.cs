namespace TicTacToe.API.Game
{
    public class PlayerMove
    {
        public PlayerMove(int x, int y, PlayerType playerType)
        {
            X = x;

            Y = y;

            PlayerType = playerType;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public PlayerType PlayerType { get; set; }
    }
}
