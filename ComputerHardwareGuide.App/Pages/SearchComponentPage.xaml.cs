using ComputerHardwareGuide.API;
using ComputerHardwareGuide.App.Controls.SearchOptions;
using ComputerHardwareGuide.Models;
using ComputerHardwareGuide.Models.Components;
using ComputerHardwareGuide.Models.Helpers;
using ComputerHardwareGuide.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace ComputerHardwareGuide.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchComponentPage : ContentPage
    {
        public ComponentTypeEnumeration ComponentTypeEnumeration { get; set; }
        public Assembly Assembly { get; set; }

        public SearchComponentPage(Assembly assembly,
                                   ComponentTypeEnumeration type,
                                   IEnumerable<Firm> firms,
                                   IEnumerable<Lookup> lookups,
                                   GetUnitVM unitModel)
        {
            InitializeComponent();
            Assembly = assembly;

            ComponentTypeEnumeration = type;

            Search.Text = $"Search {type.GetDescription()}";
            Search.Clicked += Search_Clicked;

            foreach (var unit in unitModel.Units)
            {
                switch (unit.UnitType)
                {
                    case UnitType.CheckboxGroup:
                        var checkboxGroupOption = new CheckboxGroupOption(unit);
                        UnitList.Children.Add(checkboxGroupOption);
                        break;
                    case UnitType.RadiobuttonGroup:
                        var radioButtonOption = new RadiobuttonGroupOption(unit);
                        UnitList.Children.Add(radioButtonOption);
                        break;
                    case UnitType.Range:
                        var rangeOption = new RangeOption(unit);
                        UnitList.Children.Add(rangeOption);
                        break;
                    case UnitType.Text:
                        foreach (var option in unit.Options)
                        {
                            var textInput = new TextOption(unit, option);
                            UnitList.Children.Add(textInput);
                        }
                        break;
                    default:
                        break;
                }
            }

            var firmUnit = new Unit
            {
                Name = "Firms",
                Key = "firms",
                UnitType = UnitType.CheckboxGroup,
                Options = firms.Select(x => new Option
                {
                    Key = "firms",
                    Text = $"{x.Name} ({x.Country})",
                    Value = x.Id
                })
            };
            var firmsOption = new CheckboxGroupOption(firmUnit);
            UnitList.Children.Add(firmsOption);

            foreach (var lookup in lookups)
            {
                var lookupUnit = new Unit
                {
                    Name = lookup.Name,
                    Key = lookup.Key,
                    UnitType = UnitType.CheckboxGroup,
                    Options = lookup.LookupValues.Select(x => new Option
                    {
                        Key = lookup.Key,
                        Text = x.DisplayText,
                        Value = x.Id
                    })
                };
                var lookupOption = new CheckboxGroupOption(lookupUnit);
                UnitList.Children.Add(lookupOption);
            }
        }

        private async void Search_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Loading..."))
                {
                    var dictionary = new List<KeyValuePair<string, object>>();
                    foreach (var child in UnitList.Children)
                    {
                        if (child is IBaseOption option)
                        {
                            dictionary.AddRange(option.Dictionary
                                .Select(x => new KeyValuePair<string, object>(x.Item1, x.Item2)));
                        }
                    }

                    IEnumerable<BaseComponent> components = null;
                    switch (ComponentTypeEnumeration)
                    {
                        case ComponentTypeEnumeration.CPU:
                            var resultCPUs = await APIContext.CPUs.Get(dictionary);
                            components = resultCPUs.Data;
                            break;
                        case ComponentTypeEnumeration.RAM:
                            var resultRAMs = await APIContext.RAMs.Get(dictionary);
                            components = resultRAMs.Data;
                            break;
                        case ComponentTypeEnumeration.GPU:
                            var resultGPUs = await APIContext.GPUs.Get(dictionary);
                            components = resultGPUs.Data;
                            break;
                        case ComponentTypeEnumeration.PowerUnit:
                            var resultPowerUnits = await APIContext.PowerUnits.Get(dictionary);
                            components = resultPowerUnits.Data;
                            break;
                        case ComponentTypeEnumeration.Motherboard:
                            var resultMotherboards = await APIContext.Motherboards.Get(dictionary);
                            components = resultMotherboards.Data;
                            break;
                        case ComponentTypeEnumeration.HDD:
                            var resultHDDs = await APIContext.HDDs.Get(dictionary);
                            components = resultHDDs.Data;
                            break;
                        case ComponentTypeEnumeration.SSD:
                            var resultSSDs = await APIContext.SSDs.Get(dictionary);
                            components = resultSSDs.Data;
                            break;
                    }
                    var componentListPage = new SearchComponentList(Assembly, components);
                    await Navigation.PushModalAsync(componentListPage);
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