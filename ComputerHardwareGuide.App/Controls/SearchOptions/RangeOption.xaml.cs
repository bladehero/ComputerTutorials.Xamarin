using ComputerHardwareGuide.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ComputerHardwareGuide.App.Controls.SearchOptions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RangeOption : ContentView, IBaseOption
    {
        public RangeOption(Unit unit)
        {
            InitializeComponent();
            Unit = unit;

            Title.Text = unit.Name;
            var first = unit.Options.First();
            var last = unit.Options.Last();
            RangeSlider.MinimumValue = Convert.ToSingle(first.Value);
            RangeSlider.MaximumValue = Convert.ToSingle(last.Value);
            RangeSlider.LowerValue = Convert.ToSingle(first.Value);
            RangeSlider.UpperValue = Convert.ToSingle(last.Value);
            RangeSlider.FormatLabel = (_, v) => $"{v:F1}";
        }

        public Unit Unit { get; set; }
        public UnitType UnitType => UnitType.Range;
        public IEnumerable<(string, object)> Dictionary
        {
            get
            {
                return new (string, object)[]
                {
                    ($"{Unit.Options.First().Key}", RangeSlider.LowerValue),
                    ($"{Unit.Options.Last().Key}", RangeSlider.UpperValue)
                };
            }
        }
    }
}