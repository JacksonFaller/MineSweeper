using MineSweeper.Data.Serializers;
using System;

namespace MineSweeper.Data.DataProviders
{
    public abstract class BaseSerializationDataProvider
    {
        protected readonly ISerializer Serializer;

        public BaseSerializationDataProvider(ISerializer serializer)
        {
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }
    }
}
