using ShowcaseCore.ApiResponses;
using ShowcaseCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioApp.Tools
{
    internal class RequestBuilder
    {
        private static string _apiUrl = string.Empty;

        private static IReadOnlyDictionary<Type, string> _modelBaseUrl = new Dictionary<Type, string>()
        {
            { typeof(Competences), "/api/skill" },
            { typeof(ParcoursResponse), "/api/experiences" },
            { typeof(TypeParcours), "/api/experiences" },
            { typeof(Projets), "/api/projects" },
        };

        internal static void Init()
        {
            _apiUrl = ConfigurationFiles.GetApiUrl();
        }
    }
}
