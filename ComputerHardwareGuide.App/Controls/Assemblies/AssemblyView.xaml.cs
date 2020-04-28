using ComputerHardwareGuide.API;
using ComputerHardwareGuide.App.Pages;
using ComputerHardwareGuide.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Models;
using XF.Material.Forms.UI;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace ComputerHardwareGuide.App.Controls.Assemblies
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssemblyView : ContentView
    {
        public Assembly Assembly { get; set; }

        public event EventHandler Deleted;

        public AssemblyView(Assembly assembly)
        {
            Assembly = assembly;
            InitializeComponent();

            AssemblyNameLabel.Text = assembly.Name;
            var nameGesture = new TapGestureRecognizer();
            nameGesture.Tapped += NameGesture_Tapped;
            AssemblyNameLabel.GestureRecognizers.Add(nameGesture);

            AssemblyPriceLabel.Text = assembly.ToPrice.ToString();
            var priceGesture = new TapGestureRecognizer();
            priceGesture.Tapped += PriceGesture_Tapped;
            AssemblyPriceLabel.GestureRecognizers.Add(priceGesture);

            MenuButton.MenuSelected += MenuButton_MenuSelected;
        }

        private async void MenuButton_MenuSelected(object sender, MenuSelectedEventArgs e)
        {
            try
            {
                if (e.Result.Index == -1)
                {
                    return;
                }

                if (e.Result.Index == 1)
                {
                    Deleted?.Invoke(this, e);
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
            catch (Exception)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "An error occured!",
                                               msDuration: MaterialSnackbar.DurationLong);
            }
        }

        private async void PriceGesture_Tapped(object sender, EventArgs e)
        {
            try
            {
                var input = await MaterialDialog.Instance.InputAsync(
                    "Update assembly",
                    "Change price:",
                    inputText: AssemblyPriceLabel.Text, 
                    configuration: new MaterialInputDialogConfiguration
                    {
                        InputType = MaterialTextFieldInputType.Numeric
                    });
                if (string.IsNullOrWhiteSpace(input))
                {
                    return;
                }

                using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Record is being updated..."))
                {
                    var result = await APIContext.Assemblies.Put(new Models.ViewModels.UpdateAssemblyVM
                    {
                        AssemblyId = Assembly.Id,
                        ToPrice = Convert.ToInt32(input)
                    });
                    if (result.Success)
                    {
                        Application.Current.MainPage = new AssembliesPage();
                        await MaterialDialog.Instance.SnackbarAsync(message: "Assembly was updated!",
                                                       msDuration: MaterialSnackbar.DurationLong);
                    }
                }
            }
            catch (Exception)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "An error occured!",
                                               msDuration: MaterialSnackbar.DurationLong);
            }
        }

        private async void NameGesture_Tapped(object sender, EventArgs e)
        {
            try
            {
                var input = await MaterialDialog.Instance.InputAsync(
                    "Update assembly",
                    "Change name:",
                    inputText: AssemblyNameLabel.Text,
                    configuration: new MaterialInputDialogConfiguration
                    {
                        InputType = MaterialTextFieldInputType.Text
                    });
                if (string.IsNullOrWhiteSpace(input))
                {
                    return;
                }
                using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Record is being updated..."))
                {
                    var result = await APIContext.Assemblies.Put(new Models.ViewModels.UpdateAssemblyVM
                    {
                        AssemblyId = Assembly.Id,
                        Name = input
                    });
                    if (result.Success)
                    {
                        Application.Current.MainPage = new AssembliesPage();
                        await MaterialDialog.Instance.SnackbarAsync(message: "Assembly was updated!",
                                                       msDuration: MaterialSnackbar.DurationLong);
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