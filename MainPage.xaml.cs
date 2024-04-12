using MauiAuthDemo.GoogleServices;

namespace MauiAuthDemo
{
    public partial class MainPage : ContentPage
    {

        private readonly IGoogleAuthService _googleAuthService;
        private readonly IAnalyticsService _analyticsService;
        public MainPage(IGoogleAuthService googleAuthService, IAnalyticsService analyticsService)
        {

            _googleAuthService = googleAuthService;
            _analyticsService = analyticsService;
            InitializeComponent();
        }

        private async void loginBtn_Clicked(object sender, EventArgs e)
        {

            var loggedUser = await _googleAuthService.GetCurrentUserAsync();

            if (loggedUser == null)
            {
                loggedUser = await _googleAuthService.AuthenticateAsync();
            }

            await Application.Current.MainPage.DisplayAlert("Login Message", "Welcome " + loggedUser.FullName, "Ok");

        }

        private async void logoutBtn_Clicked(object sender, EventArgs e)
        {

            await _googleAuthService?.LogoutAsync();

            await Application.Current.MainPage.DisplayAlert("Login Message", "Goodbye", "Ok");

        }

        private void analyticsLog_Clicked(object sender, EventArgs e)
        {

            _analyticsService.Log("Event_AnaliticsLogClicked");


        }
    }

}
