using PortfolioApp.Tools;

namespace PortfolioApp
{
    public partial class App : Application
    {
        private const string API_KEY = "x-api-key";

        private static string? _apiKey = null;
        public static string? ApiKey
        {
            get => _apiKey;
            set
            {
                if(value == _apiKey) return;
                _apiKey = value;
                if(value != null)
                {
                    SecureStorage.Default.SetAsync(API_KEY, value);
                }
                else
                {
                    SecureStorage.Default.Remove(API_KEY);
                }
                ApiRequest.ApiKeyChanged();
            }
        }

        public App()
        {
            InitializeComponent();

            ApiRequest.Init();

            if(ConfigurationFiles.TryGetApiKey(out string apiKey))
            {
                ApiKey = apiKey;
            }

            MainPage = new AppShell();
        }

        public static async Task TryGetApiKeyFromSecureStorage()
        {
            string? value = await SecureStorage.Default.GetAsync(API_KEY);
            if(value != null)
            {
                _apiKey = value;
                ApiRequest.ApiKeyChanged();
            }
        }
    }
}
