using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioApp.CustomException
{
    public class ConfigurationException : Exception
    {
        public string? JsonKey { get; set; }


        public ConfigurationException()
        {
        }

        public ConfigurationException(string? message) : base(message)
        {
        }

        public ConfigurationException(string? jsonKey, string? message) : base(message)
        {
            JsonKey = jsonKey;
        }

        public ConfigurationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
