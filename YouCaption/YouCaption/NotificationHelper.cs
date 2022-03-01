using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.App;

namespace YouCaptionBase.Droid
{
    internal class NotificationHelper
    {

        static readonly int PROGRESS_NOTIFICATION_ID = 1000;
        static readonly int ONESHOT_NOTIFICATION_ID = 1001;

        static readonly string PROGRESS_CHANNEL_ID = "progress_notification";
        static readonly string ONESHOT_CHANNEL_ID = "oneshot_notification";

        Context context;
        NotificationManager notificationManager;
        public NotificationCompat.Builder ProgressBuilder;
        public NotificationCompat.Builder OneshotBuilder;

        public NotificationManagerCompat Manager;
        public PendingIntent PendingIntent;

        private Notification progressNotification, oneshotNotification;
        public NotificationHelper(Context ctx, NotificationManager manager)
        {
            context = ctx;
            notificationManager = manager;
            CreateNotificationChannels();
            Manager = NotificationManagerCompat.From(context);
            Intent intent = new Intent(context, context.Class);
            PendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Mutable);

            ProgressBuilder = new NotificationCompat.Builder(context, PROGRESS_CHANNEL_ID);
            OneshotBuilder = new NotificationCompat.Builder(context, ONESHOT_CHANNEL_ID);
        }
        void CreateNotificationChannels()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                return;
            }

            var progressNtfChannel = new NotificationChannel(PROGRESS_CHANNEL_ID, "Progress Notifications", NotificationImportance.Default)
            {
                Description = "Used to show download progress",
            };

            var oneshotNtfChannel = new NotificationChannel(ONESHOT_CHANNEL_ID, "Oneshot Notifications", NotificationImportance.Max)
            {
                Description = "Used to show oneshot notifcations",
            };

            notificationManager.CreateNotificationChannel(progressNtfChannel);
            notificationManager.CreateNotificationChannel(oneshotNtfChannel);
        }
        public void NotifyProgress()
        {
            Manager.Notify(PROGRESS_NOTIFICATION_ID, ProgressBuilder.Build());
        }
        public void NotifyOneshot()
        {
            Manager.Notify(ONESHOT_NOTIFICATION_ID, OneshotBuilder.Build());
        }
    }
}
