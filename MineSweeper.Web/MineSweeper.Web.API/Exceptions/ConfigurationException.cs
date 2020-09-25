using System;

namespace MineSweeper.Web.API.Exceptions
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException(string configName, string message) : base($"Invalid configuration {configName}. {message}")
        {
        }
    }
}
