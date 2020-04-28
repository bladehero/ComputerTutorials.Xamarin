using ComputerHardwareGuide.API;
using ComputerHardwareGuide.App.Controls.AssemblyComponents;
using ComputerHardwareGuide.Models;
using ComputerHardwareGuide.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI;
using XF.Material.Forms.UI.Dialogs;

namespace ComputerHardwareGuide.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssemblyComponentsPage : ContentPage
    {
        public Assembly Assembly { get; set; }

        public AssemblyComponentsPage(Assembly assembly, int total)
        {
            InitializeComponent();

            Assembly = assembly;
            AssemblyNameLabel.Text = assembly.Name;
            ToPriceLabel.Text = assembly.ToPrice.ToString();
            CurrentTotalLabel.Text = total.ToString();
            AssemblyPercentLabel.Text = GetReadyPercentage().ToString();
            AssemblyPercentProgress.Progress = GetReadyPercentage() / 100f;

            MenuButton.MenuSelected += MenuButton_MenuSelected;

            ComponentListControl.Content = new AssemblyComponentList(assembly);
        }

        private async void MenuButton_MenuSelected(object sender, MenuSelectedEventArgs e)
        {
            try
            {
                if (e.Result.Index == -1)
                {
                    return;
                }

                if (e.Result.Index == 0)
                {

                    using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Loading..."))
                    {
                        var result = await APIContext.Assemblies.Get(Assembly.Id);
                        if (result.Success)
                        {
                            var componentsView = new AssemblyComponentsPage(result.Data.Assembly,
                                (int)result.Data.Total);
                            await Navigation.PushModalAsync(componentsView);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "An error occured!",
                                               msDuration: MaterialSnackbar.DurationLong);
            }
        }

        private int GetReadyPercentage()
        {
            int count = 0;

            count += Assembly.AssemblyComponents
                .Any(x => x.ComponentType.ComponentTypeEnumeration == ComponentTypeEnumeration.CPU) ? 1 : 0;
            count += Assembly.AssemblyComponents
                .Any(x => x.ComponentType.ComponentTypeEnumeration == ComponentTypeEnumeration.RAM) ? 1 : 0;
            count += Assembly.AssemblyComponents
                .Any(x => x.ComponentType.ComponentTypeEnumeration == ComponentTypeEnumeration.GPU) ? 1 : 0;
            count += Assembly.AssemblyComponents
                .Any(x => x.ComponentType.ComponentTypeEnumeration == ComponentTypeEnumeration.Motherboard) ? 1 : 0;
            count += Assembly.AssemblyComponents
                .Any(x => x.ComponentType.ComponentTypeEnumeration == ComponentTypeEnumeration.PowerUnit) ? 1 : 0;
            count += Assembly.AssemblyComponents
                .Any(x => x.ComponentType.ComponentTypeEnumeration == ComponentTypeEnumeration.SSD ||
                x.ComponentType.ComponentTypeEnumeration == ComponentTypeEnumeration.HDD) ? 1 : 0;

            return (int)(((double)count / 6) * 100);
        }
    }
}