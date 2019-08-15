using System;
using System.IO;
using System.Threading.Tasks;
using MineSweeper.Interfaces;
using Newtonsoft.Json;
using Serilog;

namespace MineSweeper.Data
{
    public class FileDataProvider : IDataProvider<string>
    {
        private const string BasePathToSavesDirectory = "/Saves/";

        public async Task<GameSave<string>> GetGameAsync(string key)
        {
            if(key == null) throw new ArgumentNullException(nameof(key));
            StreamReader streamReader = null;
            try
            {
                string savePath = ComposeSavePath(key);
                var fileStream = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                streamReader = new StreamReader(fileStream);
                string gameData = await streamReader.ReadToEndAsync();
                var gameSave = JsonConvert.DeserializeObject<GameSave<string>>(gameData);
                return gameSave;
            }
            catch (Exception ex)
            {
                Log.Error(ex,$"Failed to load game with key: {key}");
                throw;
            }
            finally
            {
                streamReader?.Close();
            }
        }

        public async Task<string> SaveGameAsync(GameSave<string> game)
        {
            if(game == null) throw new ArgumentNullException(nameof(game));
            StreamWriter streamWriter = null;
            try
            {
                game.Id = Guid.NewGuid().ToString();
                string savePath = ComposeSavePath(game.Id);
                var fileStream = new FileStream(savePath, FileMode.CreateNew, FileAccess.Write);
                streamWriter = new StreamWriter(fileStream);
                string gameSave = JsonConvert.SerializeObject(game);
                await streamWriter.WriteLineAsync(gameSave);
                return game.Id;
            }
            catch (Exception ex)
            {
                Log.Error(ex,$"Failed to save game");
                throw;
            }
            finally
            {
                streamWriter?.Close();
            }
        }

        public void RemoveGame(string key)
        {
            if(key == null) throw new ArgumentNullException(nameof(key));
            string savePath = ComposeSavePath(key);
            if(!File.Exists(savePath)) 
                throw new ArgumentException($"Game with key {key} is not found", nameof(key));
            
            File.Delete(savePath);
        }

        private string ComposeSavePath(string key)
        {
            return $"{BasePathToSavesDirectory}{key}";
        }
    }
}