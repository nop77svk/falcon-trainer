namespace NoP77svk.FalconTrainer.Gui.Win.ViewModels;
using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NoP77svk.FalconTrainer.Gui.Win.Services;

internal partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        _mathTaskRepository = new MathTasksRepository(6);
        _mathTaskContent = new MathTaskControlViewModel(_mathTaskRepository.GetRandomTask());
    }

    private MathTasksRepository _mathTaskRepository;

    [ObservableProperty]
    private float _progressMax = 125;

    [ObservableProperty]
    private float _progressValue = 13;

    [ObservableProperty]
    private MathTaskControlViewModel _mathTaskContent;

    [RelayCommand]
    public void SubmitCommand()
    {
        ProgressValue += 1.0f;
    }
}