namespace ProcessKiller
{
    using System.Windows;
    using System.Windows.Forms;

    using ProcessKiller.Logic;

    public partial class App
    {
        private readonly NotifyIcon notifyIcon;

        private readonly ProcessKiller processKiller;

        public App()
        {
            this.notifyIcon = new NotifyIcon();
            this.processKiller = new ProcessKiller();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.notifyIcon.Text = global::ProcessKiller.Properties.Resources.TrayIconText;
            this.notifyIcon.Icon = global::ProcessKiller.Properties.Resources.Axe;
            this.notifyIcon.DoubleClick += this.CloseApp;

            this.notifyIcon.Visible = true;
            this.processKiller.Run();
        }

        private void CloseApp(object sender, System.EventArgs e)
        {
            Current.Shutdown();
        }
    }
}
