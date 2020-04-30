using ComputerHardwareGuide.API;
using ComputerHardwareGuide.App.Pages;
using ComputerHardwareGuide.Models;
using ComputerHardwareGuide.Models.Components;
using ComputerHardwareGuide.Models.ViewModels;
using System;
using System.IO;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace ComputerHardwareGuide.App.Controls.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComponentView : ContentView
    {
        public Assembly Assembly { get; set; }
        public BaseComponent Component { get; set; }

        public ComponentView(Assembly assembly, BaseComponent baseComponent, int? quantity = null)
        {
            InitializeComponent();
            Assembly = assembly;
            Component = baseComponent;

            ComponentPriceLabel.Text = baseComponent.Price.ToString();
            ComponentNameLabel.Text = baseComponent.Name;
            if (quantity.HasValue)
            {
                ComponentCountLabel.Text = $"{quantity}pcs.";
                UpdateMenuButton.IsVisible = true;
            }
            else
            {
                AddMenuButton.IsVisible = true;
            }
            var bytes = baseComponent.ComponentPictures.FirstOrDefault()?.FileStream;
            if (bytes != null)
            {
                ComponentImage.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
            }

            AddMenuButton.MenuSelected += MenuButton_MenuSelected;
            UpdateMenuButton.MenuSelected += UpdateMenuButton_MenuSelected; ;
        }

        private async void UpdateMenuButton_MenuSelected(object sender, XF.Material.Forms.UI.MenuSelectedEventArgs e)
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
                        var result = await APIContext.Assemblies.Delete(Assembly.Id, Component.Id, (int)Component.Type);
                        if (result.Success)
                        {
                            var getAssembly = await APIContext.Assemblies.Get(Assembly.Id);
                            if (getAssembly.Success)
                            {
                                var componentsView = new AssemblyComponentsPage(getAssembly.Data.Assembly,
                                    (int)getAssembly.Data.Total);
                                await Navigation.PushModalAsync(componentsView);
                            }
                        }
                    }
                }
                if (e.Result.Index == 1)
                {
                    // TODO: View action
                }
            }
            catch (Exception ex)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "An error occured!",
                                               msDuration: MaterialSnackbar.DurationLong);
            }
        }

        private async void MenuButton_MenuSelected(object sender, XF.Material.Forms.UI.MenuSelectedEventArgs e)
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
                        var result = await APIContext.Assemblies.Post(new AddAssemblyComponentVM
                        {
                            AssemblyId = Assembly.Id,
                            ComponentId = Component.Id,
                            Quantity = 1,
                            Type = Component.Type
                        });
                        if (result.Success)
                        {
                            var getAssembly = await APIContext.Assemblies.Get(Assembly.Id);
                            if (getAssembly.Success)
                            {
                                var componentsView = new AssemblyComponentsPage(getAssembly.Data.Assembly,
                                    (int)getAssembly.Data.Total);
                                await Navigation.PushModalAsync(componentsView);
                            }
                        }
                    }
                }
                if (e.Result.Index == 1)
                {
                    // TODO: View action
                }
            }
            catch (Exception ex)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: "An error occured!",
                                               msDuration: MaterialSnackbar.DurationLong);
            }
        }
    }
}