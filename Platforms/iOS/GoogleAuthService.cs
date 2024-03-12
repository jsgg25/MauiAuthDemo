using Foundation;
using Google.SignIn;
using UIKit;

namespace MauiAuthDemo.GoogleServices
{
    public partial class GoogleAuthService
    {
        public GoogleAuthService()
        {
            Google.SignIn.SignIn.SharedInstance.Scopes = new string[] { "https://www.googleapis.com/auth/userinfo.email" };
            Google.SignIn.SignIn.SharedInstance.ClientId = WebApiKey;
        }


        public Task<GoogleUserDTO> AuthenticateAsync()
        {
            Google.SignIn.SignIn.SharedInstance.SignedIn += SharedInstance_SignedIn;

            // Prepare the presentation view controller for the authentication browser
            PreparePresentedViewController();

            // Create a TaskCompletionSource to represent the asynchronous operation
            var tcs = new TaskCompletionSource<GoogleUserDTO>();

            Google.SignIn.SignIn.SharedInstance.SignInUser();

            // Return the Task from the TaskCompletionSource
            return tcs.Task;
        }

        private void PreparePresentedViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;

            var viewController = window.RootViewController;

            while (viewController.PresentingViewController != null)
                viewController = viewController.PresentingViewController; ;

            SignIn.SharedInstance.PresentingViewController = viewController;
        }


        private void SharedInstance_SignedIn(object sender, Google.SignIn.SignInDelegateEventArgs arg)
        {
            // Check for errors after login
            if (arg.Error != null)
                throw new Exception($"Error - {arg.Error.LocalizedDescription} - {Convert.ToInt32(arg.Error.Code)}");

            var token = "";

            // Retrieve token from authentication
            SignIn.SharedInstance.CurrentUser.Authentication.GetTokens((Authentication auth, NSError error) =>
            {
                if (error == null)
                    token = auth.IdToken;
                else
                {
                    throw new Exception($"Unable to retrieve token id - ERR: {error.Code} - {error.LocalizedDescription}");
                }
            });

            var user = new GoogleUserDTO
            {
                TokenId = token,
                Email = arg.User.Profile.Email,
                FullName = arg.User.Profile.Name,
                UserName = arg.User.Profile.Email,
            };
        }


        public async Task<GoogleUserDTO> GetCurrentUserAsync()
        {
            // Check if there is a previous sign-in
            if (SignIn.SharedInstance.HasPreviousSignIn)
                SignIn.SharedInstance.RestorePreviousSignIn();

            // Get the current user
            var currentUser = SignIn.SharedInstance.CurrentUser;

            if (currentUser == null)
                throw new Exception("User not found");

            // Return details of the current user
            return await Task.FromResult(new GoogleUserDTO
            {
                Email = currentUser.Profile.Email,
                FullName = currentUser.Profile.Name,
                UserName = currentUser.Profile.Name
            });
        }



        public Task LogoutAsync() => Task.FromResult(() => SignIn.SharedInstance.SignOutUser());
    }
}
