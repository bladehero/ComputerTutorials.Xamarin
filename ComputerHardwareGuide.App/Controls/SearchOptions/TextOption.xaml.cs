using ComputerHardwareGuide.Models.ViewModels;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ComputerHardwareGuide.App.Controls.SearchOptions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextOption : ContentView, IBaseOption
    {
        public Unit Unit { get; set; }
        public Option Option { get; set; }

        public UnitType UnitType => UnitType.Text;

        public IEnumerable<(string, object)> Dictionary
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ValueInput.Text))
                {
                    return new (string, object)[] { };
                }
                return new (string, object)[] { (Unit.Key, ValueInput.Text) };
            }
        }

        public TextOption(Unit unit, Option option)
        {
            InitializeComponent();
            Unit = unit;
            Option = option;

            ValueInput.Placeholder = option.Text;
            ValueInput.Text = option.Value.ToString();
        }
    }
}