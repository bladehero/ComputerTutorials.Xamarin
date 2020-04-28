using ComputerHardwareGuide.API;
using ComputerHardwareGuide.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI;
using XF.Material.Forms.UI.Dialogs;

namespace ComputerHardwareGuide.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAssembly : ContentPage
    {
        public AddAssembly()
        {
            InitializeComponent();

            AssemblyNameInput.TextChanged += AssemblyInput_TextChanged;
            AssemblyPriceInput.TextChanged += AssemblyInput_TextChanged;

            AssemblyCancelButton.Clicked += AssemblyCancelButton_Clicked;
            AssemblySaveButton.Clicked += AssemblySaveButton_Clicked;
        }

        private void AssemblyInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            var field = sender as MaterialTextField;
            if (field != null)
            {
                field.HasError = string.IsNullOrWhiteSpace(field.Text);
            }
        }

        private async void AssemblyCancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }

        private async void AssemblySaveButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(AssemblyNameInput.Text))
                {
                    AssemblyNameInput.HasError = true;
                    return;
                }
                if (string.IsNullOrWhiteSpace(AssemblyPriceInput.Text))
                {
                    AssemblyPriceInput.HasError = true;
                    return;
                }

                using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Record is being created..."))
                {
                    var assembly = new Assembly
                    {
                        Name = AssemblyNameInput.Text,
                        ToPrice = Convert.ToInt32(AssemblyPriceInput.Text)
                    };

                    var result = await APIContext.Assemblies.Post(assembly);
                    if (result.Success)
                    {
                        Application.Current.MainPage = new AssembliesPage();
                        await MaterialDialog.Instance.SnackbarAsync(message: "Assembly was created!",
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