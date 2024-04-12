using Firebase.Analytics;

namespace MauiAuthDemo.GoogleServices
{
    public class AnalyticsService : IAnalyticsService
    {
        public void Log(string eventName)
        {
#if IOS
            Analytics.LogEvent(eventName, (Dictionary<object, object>)null);
#else
            var firebaseAnalytics = FirebaseAnalytics.GetInstance(Platform.CurrentActivity);
            firebaseAnalytics.LogEvent(eventName, null);
#endif
        }
    }
}
