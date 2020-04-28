using ComputerHardwareGuide.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI;

namespace ComputerHardwareGuide.App.Controls.SearchOptions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadiobuttonGroupOption : ContentView, IBaseOption
    {
        public Unit Unit { get; set; }

        public UnitType UnitType => UnitType.RadiobuttonGroup;

        public IEnumerable<(string, object)> Dictionary
        {
            get
            {
                if (RadiobuttonGroup.SelectedIndex == -1)
                {
                    return new (string, object)[] { };
                }
                return new (string, object)[] { (Unit.Key, Unit.Options.ToList()[RadiobuttonGroup.SelectedIndex].Value) };
            }
        }

        public RadiobuttonGroupOption(Unit unit)
        {
            InitializeComponent();
            Unit = unit;

            Title.Text = unit.Name;
            RadiobuttonGroup.Choices = unit.Options.Select(x => x.Text).ToList();
            RadiobuttonGroup.SelectedIndexChanged += RadiobuttonGroup_SelectedIndexChanged;
        }

        private void RadiobuttonGroup_SelectedIndexChanged(object sender, SelectedIndexChangedEventArgs e)
        {
            var children = ((RadiobuttonGroup as ContentView).Content as StackLayout).Children;
            foreach (var child in children)
            {
                if (child is MaterialRadioButton radioButton)
                {
                    var index = children.IndexOf(radioButton);
                    radioButton.IsSelected = index == e.Index;
                }
            }
        }
    }
}