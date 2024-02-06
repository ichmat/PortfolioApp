using Microsoft.Extensions.Configuration;
using PortfolioApp.CustomException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioApp.Tools
{
    internal static class ConfigurationFiles
    {
        private const string CONFIG_JSON = "config.json";

        private const string JSKEY_API_KEY = "autorizationKey";
        private const string JSKEY_API_URL = "apiUrl";

        private static IConfiguration? _configuration;

        private static void InitIfNull()
        {
            if (_configuration == null)
            {
                _configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(CONFIG_JSON, optional: false, reloadOnChange: false)
                    .Build();
            }
        }

        internal static string GetApiUrl()
        {
            InitIfNull();
            return _configuration![JSKEY_API_URL] ?? 
                throw new ConfigurationException(JSKEY_API_URL, $"la clé d'api n'est pas configuré dans le fichier {CONFIG_JSON}");
        }

        internal static bool TryGetApiKey(out string apiKey)
        {
            InitIfNull();
            apiKey = _configuration![JSKEY_API_KEY] ?? string.Empty;
            return apiKey != string.Empty;
        }
    }
}
