using MaddsPet.Settings;

namespace MaddsPet
{
    internal static class Program
    {
        public static Settingsmanager Settingss { get; private set; }
        public static PetWidget ActiveWidget;
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Settingss = Settingsmanager.Load();

            ActiveWidget = new PetWidget();
            Application.Run(ActiveWidget);
        }
    }
}