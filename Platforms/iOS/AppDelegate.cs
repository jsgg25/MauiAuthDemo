using Foundation;
using Google.SignIn;
using UIKit;

namespace MauiAuthDemo
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public override bool OpenUrl(UIApplication application, NSUrl url, NSDictionary options)
        {
            SignIn.SharedInstance.HandleUrl(url);
            return true;
        }
    }
}
