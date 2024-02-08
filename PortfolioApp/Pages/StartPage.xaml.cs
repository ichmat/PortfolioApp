using PortfolioApp.Tools;
using ShowcaseCore.ApiResponses;
using ShowcaseCore.Enums;

namespace PortfolioApp.Pages;

public partial class StartPage : ContentPage
{
	public StartPage()
	{
		InitializeComponent();
	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
		activity.IsRunning = true;
		Init();
    }

	private async void Init()
	{
        StatusResponse? statusResponse = null;
		bool retry = false;

        do
        {
            statusResponse = await ApiRequest.GetAsync<StatusResponse>("/api/status");
            if (statusResponse == null)
            {
                retry = await DisplayAlert("Impossible de se connecté",
                    "Veuillez vérifier votre connexion.",
                    "Réessayer", "Annulé");
            }
        } 
        while (retry);

        if(statusResponse == null)
        {
            Application.Current!.Quit();
            return;
        }

        if(App.ApiKey == null)
        {
            await App.TryGetApiKeyFromSecureStorage();
        }

        if(App.ApiKey == null)
        {
            _ = Shell.Current.GoToAsync("//ProfilePage", true);
        }
        else
        {
            _ = Shell.Current.GoToAsync("//StatusPage", true);
        }
    }
}