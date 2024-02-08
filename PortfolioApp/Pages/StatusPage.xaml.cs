using PortfolioApp.Tools;
using ShowcaseCore.ApiResponses;
using ShowcaseCore.Enums;

namespace PortfolioApp.Pages;

public partial class StatusPage : ContentPage
{
	public StatusPage()
	{
		InitializeComponent();
	}

	private async void Refresh()
	{
        ButtonRefresh.IsEnabled = false;
        activity.IsVisible = true;
        activity.IsRunning = true;

        StatusResponse? statusResponse = await this.GetAsyncOrDisplayError<StatusResponse>("/api/status");

        bool apiKeyValid = await ApiRequest.PostAsync("/api/status");

        if(statusResponse != null)
        {
            L_APIStatus.Text = Enum.GetName(typeof(ApiStatus), statusResponse.ApiStatus);
            L_BDDStatus.Text = Enum.GetName(typeof(ApiStatus), statusResponse.DatabaseStatus);

            L_APIStatus.TextColor = GetColorByStatus(statusResponse.ApiStatus);
            L_BDDStatus.TextColor = GetColorByStatus(statusResponse.DatabaseStatus);

            if(apiKeyValid)
            {
                L_APIKeyStatus.Text = "Valide";
                L_APIKeyStatus.TextColor = Colors.Green;
            }
            else
            {
                L_APIKeyStatus.Text = "Invalide";
                L_APIKeyStatus.TextColor = Colors.Red;
            }
        }
        else
        {
            L_APIStatus.Text = "<Pas d'info>";
            L_BDDStatus.Text = "<Pas d'info>";
            L_APIKeyStatus.Text = "<Pas d'info>";
            L_APIStatus.TextColor = 
                L_BDDStatus.TextColor =
                L_APIKeyStatus.TextColor = Colors.Black;
        }

        activity.IsVisible = false;
        activity.IsRunning = false;
        ButtonRefresh.IsEnabled = true;
    }

    private Color GetColorByStatus(ApiStatus status)
    {
        switch (status)
        {
            case ApiStatus.Error:
                return Colors.Red;
            case ApiStatus.Success:
                return Colors.Green;
            case ApiStatus.Warning:
                return Colors.Yellow;

            default:
                return Colors.Black;
        }
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        Refresh();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Refresh();
    }
}