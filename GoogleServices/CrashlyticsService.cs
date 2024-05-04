
using Firebase.Crashlytics;
#if IOS
using Foundation;
#endif

namespace MauiAuthDemo.GoogleServices
{
    public class CrashlyticsService : ICrashlyticsService
    {
        public void Log(Exception ex)
        {

#if IOS

            var errorInfo = new Dictionary<object, object>
            {
                {NSError.LocalizedDescriptionKey, ex.Message },
                {NSError.LocalizedFailureReasonErrorKey, "Managed failure" }
            };

            var error = new NSError(new NSString("NonFatalError"), -1001,
                NSDictionary.FromObjectsAndKeys(errorInfo.Values.ToArray(), errorInfo.Keys.ToArray(), errorInfo.Keys.Count));

            Crashlytics.SharedInstance.RecordError(error);

#else

            FirebaseCrashlytics.Instance.RecordException(Java.Lang.Throwable.FromException(ex));

#endif


        }
    }
}
