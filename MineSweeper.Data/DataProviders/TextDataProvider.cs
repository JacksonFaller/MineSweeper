using MineSweeper.Data.Models;
using MineSweeper.Data.Serializers;
using System;
using System.Threading.Tasks;

namespace MineSweeper.Data.DataProviders
{
    public class TextDataProvider : BaseSerializationDataProvider, IDataProvider
    {
        public TextDataProvider(ISerializer serializer) : base(serializer)
        {
        }

        public Task<GameSave> LoadGameAsync(string game)
        {
            if (string.IsNullOrEmpty(game))
                throw new ArgumentNullException(nameof(game));

            return Task.Run(() => Serializer.Deserialize<GameSave>(game));
        }

        public Task RemoveGameAsync(string _)
        {
            throw new NotSupportedException();
        }

        public Task<string> SaveGameAsync(GameSave game)
        {
            if (game == null)
                throw new ArgumentNullException(nameof(game));

            return Task.Run(() => Serializer.Serialize(game));
        }
    }
}
