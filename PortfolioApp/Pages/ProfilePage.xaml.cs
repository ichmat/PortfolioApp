namespace PortfolioApp.Pages;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
		E_ApiKey.Text = App.ApiKey;
	}

    private void ButtonValidate_Clicked(object sender, EventArgs e)
    {
		ChangeApiKey();
    }

	private async void ChangeApiKey()
	{
		ButtonValidate.IsEnabled = false;

		App.ApiKey = E_ApiKey.Text;

		ButtonValidate.IsEnabled = true;
    }
}