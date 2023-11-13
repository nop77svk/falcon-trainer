namespace NoP77svk.FalconTrainer.Gui.Win.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using NoP77svk.FalconTrainer.Core;
using NoP77svk.FalconTrainer.Gui.Win.Services;

internal partial class MathTaskControlViewModel : ViewModelBase
{
    public MathTaskControlViewModel(MathTask mathTask)
    {
        if (mathTask.Parts.Length == 2 && mathTask.Parts[0] is KnownMathTaskPart)
        {
            LeftKnownPart = mathTask.Parts[0].Value;
            // this.LeftKnownPart.IsVisible = true;
            UnknownPart = mathTask.Parts[1].Value;
            RightKnownPart = string.Empty;
            // this.RightKnownPart.IsVisible = false;
        }
        else if (mathTask.Parts.Length == 2 && mathTask.Parts[0] is ResultMathTaskPart)
        {
            LeftKnownPart = string.Empty;
            // this.LeftKnownPart.IsVisible = false;
            UnknownPart = mathTask.Parts[0].Value;
            RightKnownPart = mathTask.Parts[1].Value;
            // this.RightKnownPart.IsVisible = true;
        }
        else if (mathTask.Parts.Length == 3)
        {
            LeftKnownPart = mathTask.Parts[0].Value;
            // this.LeftKnownPart.IsVisible = false;
            UnknownPart = mathTask.Parts[1].Value;
            RightKnownPart = mathTask.Parts[2].Value;
            // this.RightKnownPart.IsVisible = true;
        }
    }

    [ObservableProperty]
    private string _leftKnownPart = "64 + 25 =";

    [ObservableProperty]
    private string _unknownPart = string.Empty;

    [ObservableProperty]
    private string _rightKnownPart = string.Empty;

    [ObservableProperty]
    private string _unknownPartWatermark = "?";
}
