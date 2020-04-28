using ComputerHardwareGuide.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ComputerHardwareGuide.App.Controls.SearchOptions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckboxGroupOption : ContentView, IBaseOption
    {
        public Unit Unit { get; set; }

        public UnitType UnitType => UnitType.CheckboxGroup;

        public IEnumerable<(string, object)> Dictionary
        {
            get
            {
                var selectedIndeces = SelectedIndeces();
                if (selectedIndeces?.Count == 0)
                {
                    return new (string, object)[] { };
                }
                var selectedOptions = new List<Option>();
                var options = Unit.Options.ToList();
                for (int i = 0; i < options.Count; i++)
                {
                    if (selectedIndeces.Any(x => x == i))
                    {
                        selectedOptions.Add(options[i]);
                    }
                }
                return selectedOptions.Select(x => (x.Key, x.Value));
            }
        }

        public CheckboxGroupOption(Unit unit)
        {
            InitializeComponent();
            Unit = unit;

            Title.Text = unit.Name;
            CheckBoxGroup.Choices = unit.Options.Select(x => x.Text).ToList();
            CheckBoxGroup.SelectedIndices.Clear();
        }

        private List<int> SelectedIndeces()
        {
            var children = (CheckBoxGroup.Content as StackLayout).Children;
            var indeces = new List<int>();
            foreach (var child in children)
            {
                if (child is XF.Material.Forms.UI.MaterialCheckbox checkbox)
                {
                    if (checkbox.IsSelected)
                    {
                        var index = children.IndexOf(checkbox);
                        if (index != -1)
                        {
                            indeces.Add(index);
                        }
                    }
                }
            }
            return indeces;
        }
    }
}