using ComputerHardwareGuide.API;
using ComputerHardwareGuide.App.Pages;
using ComputerHardwareGuide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace ComputerHardwareGuide.App.Controls.Assemblies
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssemblyList : ContentView
    {
        public AssemblyList()
        {
            InitializeComponent();

            AddAssemblyButton.Clicked += AddAssemblyButton_Clicked;

            BaseApiResponse<IEnumerable<Assembly>> assemblies = null;
            Task.Run(async () =>
            {
                assemblies = await APIContext.Assemblies.Get();
            }).Wait();
            foreach (var assembly in assemblies.Data)
            {
                var assemblyView = new AssemblyView(assembly);
                assemblyView.Deleted += AssemblyView_Deleted;
                AssemblyListControl.Children.Add(assemblyView);
            }
        }

        private async void AddAssemblyButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddAssembly(), true);
        }

        private async void AssemblyView_Deleted(object sender, EventArgs e)
        {
            try
            {
                using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Record is being deleted..."))
                {
                    var assemblyView = sender as AssemblyView;
                    var id = assemblyView.Assembly.Id;
                    var result = await APIContext.Assemblies.Delete(id);
                    if (result.Success)
                    {
                        var toDelete = AssemblyListControl.Children
                            .FirstOrDefault(x => (x as AssemblyView)?.Assembly?.Id == id);
                        AssemblyListControl.Children.Remove(toDelete);
                    }
                }
            }
            catch (Exception)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "An error occured!",
                                               msDuration: MaterialSnackbar.DurationLong);
            }
        }
    }
}