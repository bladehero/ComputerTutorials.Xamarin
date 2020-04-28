using ComputerHardwareGuide.App.Controls.Assemblies;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ComputerHardwareGuide.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssembliesPage : ContentPage
    {
        public AssembliesPage()
        {
            InitializeComponent();
            ContentGrid.Children.Add(new AssemblyList());
        }
    }
}