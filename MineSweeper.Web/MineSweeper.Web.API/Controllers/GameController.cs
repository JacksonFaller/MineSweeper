using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MineSweeper.Data;
using MineSweeper.Enums;
using MineSweeper.Generators.Interfaces;
using MineSweeper.Models;
using MineSweeper.Web.API.DTO;
using MineSweeper.Web.API.Models;
using Serilog;
using System;

namespace MineSweeper.Web.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameController : BaseController
    {
        private readonly IGameStorage GameStorage;
        private readonly IFieldGeneratorFactory FieldGeneratorFactory;
        private readonly ISeedGenerator SeedGenerator;


        public GameController(IGameStorage gameStorage,
            IFieldGeneratorFactory fieldGeneratorFactory, ISeedGenerator seedGenerator)
        {
            GameStorage = gameStorage;
            FieldGeneratorFactory = fieldGeneratorFactory;
            SeedGenerator = seedGenerator;
        }

        /// <summary>
        /// Start new game
        /// </summary>
        /// <param name="model">New game parameters</param>
        /// <returns>Model with new game's key</returns>
        [HttpPost]
        [ProducesResponseType(typeof(GameModelBase), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult Start(StartGameModel model)
        {
            try
            {
                var generatorParams = new FieldGeneratorParams(SeedGenerator.GenerateSeed(), model.MinesCount);
                var fieldGenerator = FieldGeneratorFactory.Create(generatorParams);
                var game = new GameModel(fieldGenerator, model.Width, model.Height, DateTime.UtcNow);
                Guid gameKey = Guid.NewGuid();
                GameStorage.AddGame(gameKey, game);
                return Ok(new GameModelBase { GameKey = gameKey });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Couldn't create a game with params {@Model}", model);
                return Error("An error occured while creating the game");
            }
        }

        /// <summary>
        /// Make a move
        /// </summary>
        /// <param name="model">Player's move parameters</param>
        /// <returns>Move result</returns>
        [HttpPost]
        [ProducesResponseType(typeof(MakeMoveResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult MakeMove(MakeMoveModel model)
        {
            try
            {
                if (!GameStorage.HasGame(model.GameKey))
                    return NotFound($"Game with key {model.GameKey} doesn't exist. It may've finished");

                GameModel game = GameStorage[model.GameKey];
                MoveResult result = game.MakeMove(model.PlayerMove);

                if (result.ResultType == MoveResultType.GameOver)
                    GameStorage.RemoveGame(model.GameKey);

                return Ok(new MakeMoveResponse { MoveResult = result });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Couldn't create a game with params {@Model}", model);
                return Error("An error occured while making the move");
            }
        }

        [HttpGet]
        public IActionResult SaveGame()
        {
            return NotFound();
        }

        [HttpPost]
        public IActionResult LoadGame()
        {
            return NotFound();
        }
    }
}
