using Microsoft.Extensions.Configuration;
using MineSweeper.Data.DataProviders;
using MineSweeper.Web.API.Exceptions;

namespace MineSweeper.Web.API.DI
{
    public static class DataProviderResolver
    {
        public static IDataProvider Resolve(IConfiguration configuration)
        {
            var config = configuration.GetSection("DataProvider");
            if (!config.Exists())
                Throw("DataProvider section is missing from configuration");

            var providerType = config.GetValue<DataProviderType>("Type");

            switch (providerType)
            {
                case DataProviderType.File:
                {
                    var optionsSection = config.GetSection("Options");
                    if (!optionsSection.Exists())
                        ThrowNoOptions(providerType);

                    var options = optionsSection.Get<FileDataProviderOptions>();
                    if (options.SavePath == null)
                        RequiredOption(nameof(options.SavePath));

                    return new FileDataProvider(options.SavePath);
                }
                default:
                    throw new ConfigurationException("DataProvider", $"Data provider type {providerType} is not supported");
            }
        }

        public static void Throw(string message)
        {
            throw new ConfigurationException("DataProvider", message);
        }

        public static void RequiredOption(string optionName)
        {
            Throw($"Option {optionName} is required");
        }

        public static void ThrowNoOptions(DataProviderType providerType)
        {
            Throw($"No options for DataProvider {providerType} were provided");
        }
    }
}
