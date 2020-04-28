using ComputerHardwareGuide.API;
using ComputerHardwareGuide.App.Controls.Components;
using ComputerHardwareGuide.App.Pages;
using ComputerHardwareGuide.Models;
using ComputerHardwareGuide.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace ComputerHardwareGuide.App.Controls.AssemblyComponents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssemblyComponentList : ContentView
    {
        public Assembly Assembly { get; set; }

        public AssemblyComponentList(Assembly assembly)
        {
            InitializeComponent();
            Assembly = assembly;
            SetVisibility();

            //BaseApiResponse<IEnumerable<Assembly>> assemblies = null;
            //Task.Run(async () =>
            //{
            //    assemblies = await APIContext.Assemblies.Get();
            //}).Wait();
            //foreach (var assembly in assemblies.Data)
            //{
            //    var assemblyView = new AssemblyView(assembly);
            //    assemblyView.Deleted += AssemblyView_Deleted;
            //    AssemblyListControl.Children.Add(assemblyView);
            //}

            CPUButton.Clicked += CPUButton_Clicked;
            RAMButton.Clicked += RAMButton_Clicked;
            GPUButton.Clicked += GPUButton_Clicked;
            PowerUnitButton.Clicked += PowerUnitButton_Clicked;
            MotherboardButton.Clicked += MotherboardButton_Clicked;
            HDDButton.Clicked += HDDButton_Clicked;
            SSDButton.Clicked += SSDButton_Clicked;

            foreach (var assemblyComponent in assembly.AssemblyComponents)
            {
                var componentView = new ComponentView(assembly, assemblyComponent.BaseComponent, assemblyComponent.Quantity);
                ComponentList.Children.Add(componentView);
            }
        }

        private void SSDButton_Clicked(object sender, EventArgs e)
        {
            GoToSearchPage(ComponentTypeEnumeration.SSD);
        }

        private void HDDButton_Clicked(object sender, EventArgs e)
        {
            GoToSearchPage(ComponentTypeEnumeration.HDD);
        }

        private void MotherboardButton_Clicked(object sender, EventArgs e)
        {
            GoToSearchPage(ComponentTypeEnumeration.Motherboard);
        }

        private void PowerUnitButton_Clicked(object sender, EventArgs e)
        {
            GoToSearchPage(ComponentTypeEnumeration.PowerUnit);
        }

        private void GPUButton_Clicked(object sender, EventArgs e)
        {
            GoToSearchPage(ComponentTypeEnumeration.GPU);
        }

        private void RAMButton_Clicked(object sender, EventArgs e)
        {
            GoToSearchPage(ComponentTypeEnumeration.RAM);
        }

        private void CPUButton_Clicked(object sender, EventArgs e)
        {
            GoToSearchPage(ComponentTypeEnumeration.CPU);
        }

        private async void GoToSearchPage(ComponentTypeEnumeration type)
        {
            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Loading..."))
            {
                var resultFirms = await APIContext.Options.GetFirms(type);
                var resultLookups = await APIContext.Options.GetLookups(type);
                var resultUnits = await APIContext.Options.GetUnits(type);
                if (resultFirms.Success && resultLookups.Success && resultUnits.Success)
                {
                    var searchPage = new SearchComponentPage(Assembly,
                                                             type,
                                                             resultFirms.Data,
                                                             resultLookups.Data,
                                                             resultUnits.Data);
                    await Navigation.PushModalAsync(searchPage);
                }
            }
        }

        private void SetVisibility()
        {
            if (!Assembly.AssemblyComponents
                .Any(x => x.ComponentType.ComponentTypeEnumeration == ComponentTypeEnumeration.CPU))
            {
                CPUButton.IsVisible = true;
                return;
            }
            if (!Assembly.AssemblyComponents
                .Any(x => x.ComponentType.ComponentTypeEnumeration == ComponentTypeEnumeration.RAM))
            {
                RAMButton.IsVisible = true;
                return;
            }
            if (!Assembly.AssemblyComponents
                .Any(x => x.ComponentType.ComponentTypeEnumeration == ComponentTypeEnumeration.GPU))
            {
                GPUButton.IsVisible = true;
                return;
            }
            if (!Assembly.AssemblyComponents
                .Any(x => x.ComponentType.ComponentTypeEnumeration == ComponentTypeEnumeration.PowerUnit))
            {
                PowerUnitButton.IsVisible = true;
                return;
            }
            if (!Assembly.AssemblyComponents
                .Any(x => x.ComponentType.ComponentTypeEnumeration == ComponentTypeEnumeration.Motherboard))
            {
                MotherboardButton.IsVisible = true;
                return;
            }

            if (!Assembly.AssemblyComponents
                .Any(x => x.ComponentType.ComponentTypeEnumeration == ComponentTypeEnumeration.HDD))
            {
                HDDButton.IsVisible = true;
            }
            if (!Assembly.AssemblyComponents
                .Any(x => x.ComponentType.ComponentTypeEnumeration == ComponentTypeEnumeration.SSD))
            {
                SSDButton.IsVisible = true;
            }
        }
    }
}