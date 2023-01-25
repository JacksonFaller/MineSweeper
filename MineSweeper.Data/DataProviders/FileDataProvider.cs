using MineSweeper.Data.Models;
using MineSweeper.Data.Serializers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MineSweeper.Data.DataProviders
{
    public class FileDataProvider : BaseSerializationDataProvider, IDataProvider
    {
        private readonly string BasePathToSavesDirectory;

        public FileDataProvider(string basePathToSavesDirectory, ISerializer serializer) : base(serializer)
        {
            BasePathToSavesDirectory = basePathToSavesDirectory ??
                throw new ArgumentNullException(nameof(basePathToSavesDirectory));
        }

        public async Task<GameSave> LoadGameAsync(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));

            string savePath = ComposeSavePath(key);

            if (!File.Exists(savePath)) 
                throw new ArgumentException($"Game with the key {key} was not found", nameof(key));

            using var fileStream = new FileStream(savePath, FileMode.Open, FileAccess.Read);
            var streamReader = new StreamReader(fileStream);
            string gameData = await streamReader.ReadToEndAsync();
            var gameSave = Serializer.Deserialize<GameSave>(gameData);
            return gameSave;
        }

        public async Task<string> SaveGameAsync(GameSave game)
        {
            if (game == null) throw new ArgumentNullException(nameof(game));

            game.Id = Guid.NewGuid().ToString();
            string savePath = ComposeSavePath(game.Id);

            using var fileStream = new FileStream(savePath, FileMode.CreateNew, FileAccess.Write);
            var streamWriter = new StreamWriter(fileStream);
            string gameSave = Serializer.Serialize(game);
            await streamWriter.WriteLineAsync(gameSave);
            return game.Id;
        }

        public Task RemoveGameAsync(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            string savePath = ComposeSavePath(key);
            if (!File.Exists(savePath))
                throw new ArgumentException($"Game with the key {key} was not found", nameof(key));

            return Task.Run(() => File.Delete(savePath));
        }

        private string ComposeSavePath(string key)
        {
            return Path.Join(BasePathToSavesDirectory, key);
        }
    }
}