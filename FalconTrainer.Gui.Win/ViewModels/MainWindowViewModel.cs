namespace NoP77svk.FalconTrainer.Gui.Win.ViewModels;
using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

internal partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private float _progressMax = 125;

    [ObservableProperty]
    private float _progressValue = 13;

    [RelayCommand]
    public void SubmitCommand()
    {
        ProgressValue += 1.0f;
    }
}