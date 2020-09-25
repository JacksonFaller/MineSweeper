using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MineSweeper.Data.DataProviders;
using MineSweeper.Data.Models;
using MineSweeper.Enums;
using MineSweeper.Generators;
using MineSweeper.Generators.Interfaces;
using MineSweeper.Models;
using MineSweeper.Web.API.DTO;
using Serilog;
using System;
using System.Threading.Tasks;

namespace MineSweeper.Web.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameController : BaseController
    {
        private readonly IGameStorage GameStorage;
        private readonly IFieldGeneratorFactory FieldGeneratorFactory;
        private readonly ISeedGenerator SeedGenerator;
        private readonly GameManager GameManager;

        public GameController(IGameStorage gameStorage, IFieldGeneratorFactory fieldGeneratorFactory,
             ISeedGenerator seedGenerator, IDataProvider dataProvider)
        {
            GameStorage = gameStorage;
            FieldGeneratorFactory = fieldGeneratorFactory;
            SeedGenerator = seedGenerator;
            GameManager = new GameManager(dataProvider, FieldGeneratorFactory);
        }

        /// <summary>
        /// Start a new game
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
                IFieldGenerator fieldGenerator = FieldGeneratorFactory.Create(generatorParams);
                var game = new Game(fieldGenerator, model.Width, model.Height);
                Guid gameKey = Guid.NewGuid();
                GameStorage.AddGame(gameKey, game);
                return Ok(new GameModelBase { GameKey = gameKey });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Couldn't create the game with params {@Model}", model);
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

                Game game = GameStorage[model.GameKey];
                MoveResult result = game.MakeMove(model.PlayerMove);

                if (result.ResultType == MoveResultType.GameOver)
                    GameStorage.RemoveGame(model.GameKey);

                return Ok(new MakeMoveResponse { MoveResult = result });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Couldn't make the move with params {@Model}", model);
                return Error("An error occured while making the move");
            }
        }

        /// <summary>
        /// Save a game
        /// </summary>
        /// <param name="model">Active game's key</param>
        /// <returns>Game's storage key</returns>
        [HttpPost]
        [ProducesResponseType(typeof(GameManagementBaseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveGame(GameModelBase model)
        {
            try
            {
                if (!GameStorage.HasGame(model.GameKey))
                    return NotFound($"Game with key {model.GameKey} doesn't exist. It may've finished");

                Game game = GameStorage[model.GameKey];
                string key = await GameManager.SaveGameAsync(game);
                return Ok(new GameManagementBaseModel { GameKey = key });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Couldn't save the game with params {@Model}", model);
                return Error("An error occured while saving the game");
            }
        }

        /// <summary>
        /// Load a game
        /// </summary>
        /// <param name="model">Game's storage key</param>
        /// <returns>Active game's key</returns>
        [HttpPost]
        [ProducesResponseType(typeof(GameModelBase), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoadGame(GameManagementBaseModel model)
        {
            try
            {
                Game game = await GameManager.LoadGameAsync(model.GameKey);
                Guid gameKey = Guid.NewGuid();
                GameStorage.AddGame(gameKey, game);
                return Ok(new GameModelBase { GameKey = gameKey });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Couldn't load the game with params {@Model}", model);
                return Error("An error occured while loading the game");
            }
        }
    }
}
