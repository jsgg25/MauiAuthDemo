using MauiAuthDemo.GoogleServices;

namespace MauiAuthDemo
{
    public partial class MainPage : ContentPage
    {

        private readonly IGoogleAuthService _googleAuthService;
        private readonly IAnalyticsService _analyticsService;
        private readonly ICrashlyticsService _crashlyticsService;
        public MainPage(IGoogleAuthService googleAuthService, IAnalyticsService analyticsService, ICrashlyticsService crashlyticsService)
        {

            _googleAuthService = googleAuthService;
            _analyticsService = analyticsService;
            _crashlyticsService = crashlyticsService;
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

        int zero = 0;
        private void crashlyticsLog_Clicked(object sender, EventArgs e)
        {
            try
            {
                var divisionByZero = 10 / zero;
            }
            catch
            {
                _crashlyticsService.Log(new Exception("User tried to divide by 0."));
            }

        }
    }

}
