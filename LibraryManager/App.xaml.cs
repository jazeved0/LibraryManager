using LibraryManager.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App Instance;
        public Configuration Config;

        public const String SCHOOL_NAME = "East Hamilton High School";

        public App()
        {
            Instance = this;
            Config = new Configuration();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            // Cancel background tasks
            MainWindowViewModel.Instance?.CancelBackgroundTasks();
        }

        // The `onTick` method will be called periodically unless cancelled.
        /// <summary>
        /// Credit to SO User Erik
        /// </summary>
        /// <param name="onTick"></param>
        /// <param name="dueTime"></param>
        /// <param name="interval"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task RunPeriodicAsync(Action onTick,
                                                   TimeSpan dueTime,
                                                   TimeSpan interval,
                                                   CancellationToken token)
        {
            // Initial wait time before we begin the periodic loop.
            if (dueTime > TimeSpan.Zero)
                await Task.Delay(dueTime, token);

            // Repeat this loop until cancelled.
            while (!token.IsCancellationRequested)
            {
                // Call our onTick function.
                onTick?.Invoke();

                // Wait to repeat again.
                if (interval > TimeSpan.Zero)
                    await Task.Delay(interval, token);
            }
        }
    }
}
