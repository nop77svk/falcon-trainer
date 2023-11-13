namespace NoP77svk.FalconTrainer.Gui.Win.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

internal partial class MathTaskControlViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _leftKnownPart = "64 + 25 =";

    [ObservableProperty]
    private string _unknownPart = string.Empty;

    [ObservableProperty]
    private string _rightKnownPart = string.Empty;

    [ObservableProperty]
    private string _unknownPartWatermark = "?";
}
