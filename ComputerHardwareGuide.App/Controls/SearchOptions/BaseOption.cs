using ComputerHardwareGuide.Models.ViewModels;
using System.Collections.Generic;

namespace ComputerHardwareGuide.App.Controls.SearchOptions
{
    public interface IBaseOption
    {
        Unit Unit { get; set; }
        UnitType UnitType { get; }
        IEnumerable<(string, object)> Dictionary { get; }
    }
}
