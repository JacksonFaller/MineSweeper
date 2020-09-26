using Microsoft.Extensions.Configuration;
using MineSweeper.Data.DataProviders;
using MineSweeper.Data.Serializers;
using MineSweeper.Web.API.DI.Options;
using MineSweeper.Web.API.Exceptions;

namespace MineSweeper.Web.API.DI
{
    public static class DataProviderResolver
    {
        public static IDataProvider Resolve(IConfiguration configuration)
        {
            var config = configuration.GetSection("DataProvider");
            if (!config.Exists())
                ConfigError("DataProvider section is missing from configuration");

            var providerType = config.GetValue<DataProviderType>("Type");

            var optionsSection = config.GetSection(BaseDataProviderOptions.Options);
            if (!optionsSection.Exists())
                throw NoOptions(providerType);

            switch (providerType)
            {
                case DataProviderType.File:
                {
                    var options = optionsSection.Get<FileDataProviderOptions>();
                    if (options.SavePath == null)
                        throw RequiredOption(nameof(options.SavePath));

                    return new FileDataProvider(options.SavePath, ResolveSerializer(options.Serializer));
                }
                case DataProviderType.Text:
                {
                    var options = optionsSection.Get<BaseDataProviderOptions>();
                    return new TextDataProvider(ResolveSerializer(options.Serializer));
                }
                default:
                {
                    throw ConfigError($"Data provider type {providerType} is not supported");
                }
            }
        }

        public static ISerializer ResolveSerializer(SerializerType serializerType)
        {
            return serializerType switch
            {
                SerializerType.Json => new JsonSerializer(),
                SerializerType.Base64 => new Base64Serializer(),
                _ => throw ConfigError($"Serializer type {serializerType} is not supported"),
            };
        }

        public static ConfigurationException ConfigError(string message)
        {
            return new ConfigurationException("DataProvider", message);
        }

        public static ConfigurationException RequiredOption(string optionName)
        {
            return ConfigError($"Option {optionName} is required");
        }

        public static ConfigurationException NoOptions(DataProviderType providerType)
        {
            return ConfigError($"No options for DataProvider {providerType} were provided");
        }
    }
}
