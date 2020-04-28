using ComputerHardwareGuide.App.Controls.Components;
using ComputerHardwareGuide.Models;
using ComputerHardwareGuide.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ComputerHardwareGuide.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchComponentList : ContentPage
    {
        public SearchComponentList(Assembly assembly, IEnumerable<BaseComponent> baseComponents)
        {
            InitializeComponent();

            if (baseComponents?.Count() > 0)
            {
                foreach (var component in baseComponents)
                {
                    var componentView = new ComponentView(assembly, component);
                    ComponentList.Children.Add(componentView);
                }
            }
        }
    }
}