using ComputerHardwareGuide.App.Pages;
using Xamarin.Forms;

namespace ComputerHardwareGuide.App
{
    public partial class App : Application
    {
        const string appUrl = "http://computerhardware-001-site1.etempurl.com/";
        //const string appUrl = "https://localhost:44312/";

        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);
            API.APIContext.SetApiUrl(appUrl);

            MainPage = new NavigationPage(new AssembliesPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
