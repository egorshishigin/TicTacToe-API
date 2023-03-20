using Newtonsoft.Json;

using Microsoft.AspNetCore.Mvc;

using TicTacToe.API.Game;

namespace TicTacToe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private static GameBoard _gameBoard = new GameBoard();

        private GameModel _gameModel = new GameModel(_gameBoard);

        [Route("/api/[controller]/newGame")]
        [HttpGet]
        public IActionResult NewGame()
        {
            _gameModel.NewGame();

            return Ok(JsonConvert.SerializeObject(_gameModel.GameStatus));
        }

        [Route("/api/[controller]/continueGame")]
        [HttpGet]
        public IActionResult ContinueGame()
        {
            try
            {
                _gameModel.ContinueGame();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(JsonConvert.SerializeObject(_gameModel.GameStatus));
        }

        [Route("/api/[controller]/makeMove")]
        [HttpPost]
        public IActionResult MakeMove(int x, int y)
        {
            try
            {
                _gameModel.MakeMove(x, y);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            switch (_gameModel.GameState)
            {
                case GameState.PLAYER_MOVE:
                    return Ok(JsonConvert.SerializeObject(_gameModel.GameStatus));
                case GameState.INVALID_MOVE:
                    return BadRequest(JsonConvert.SerializeObject(_gameModel.GameStatus));
                case GameState.DRAWN:
                    return Ok(JsonConvert.SerializeObject(_gameModel.GameStatus));
                case GameState.WON:
                    return Ok(JsonConvert.SerializeObject(_gameModel.GameStatus));
                default:
                    return BadRequest();
            }
        }
    }
}
