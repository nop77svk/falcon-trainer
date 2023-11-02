namespace NoP77svk.FalconTrainer.Gui.Win;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using NoP77svk.FalconTrainer.Core;

internal partial class MainWindow
    : Window
{
    private Dictionary<int, MathTask> _mathTasks = new Dictionary<int, MathTask>();
    private int _maxTaskId => _mathTasks.Last().Key; // 2do! unhandled exception!
    private MainWindowModel? _dc => (MainWindowModel?)DataContext;

    internal MainWindow()
    {
        InitializeComponent();

        try
        {
            // 2do! move to its own class MathTaskRepository
            _mathTasks = MathTasksFactory.GenerateAllPossibleTasks(6)
                .ToDictionary(task => task.Id);
        }
        catch (ArgumentOutOfRangeException e)
        {
            // 2do!
            throw new Exception("This should not have happened!", e);
        }

        ShowTask(GetRandomTask());
    }

    internal void btnSubmitClick(object? sender, RoutedEventArgs args)
    {
        if (sender is Button button && button.Name == "btnSubmit")
        {
            if (this.UnknownPart.Text?.CompareTo(this.UnknownPart.Tag) == 0)
            {
                // 2do! increase success counter
                ShowTask(GetRandomTask());
            }
            else
            {
                this.UnknownPart.Text = "?";
            }
        }
    }

    private MathTask GetRandomTask()
    {
        // 2do! move to own class MathTaskRepository
        int taskId = Random.Shared.Next(0, _maxTaskId + 1);
        return _mathTasks[taskId];
    }

    private void ShowTask(MathTask mathTask)
    {
        if (_dc == null)
            throw new NullReferenceException("DataContext not set");

        _dc.MathTaskResultPart = MainWindowModel.MathTaskResultPlaceholder;

        // 2do! rework to dynamically contstructing/deconstructing the known/unknown parts and their styles
        if (mathTask.Parts.Length == 2 && mathTask.Parts[0] is KnownMathTaskPart)
        {
            _dc.MathTaskLeftKnownPart = mathTask.Parts[0].Value;
            this.LeftKnownPart.IsVisible = true;

            _dc.MathTaskExpectedResult = mathTask.Parts[1].Value;

            _dc.MathTaskRightKnownPart = string.Empty;
            this.RightKnownPart.IsVisible = false;
        }
        else if (mathTask.Parts.Length == 2 && mathTask.Parts[0] is ResultMathTaskPart)
        {
            _dc.MathTaskLeftKnownPart = string.Empty;
            this.LeftKnownPart.IsVisible = false;

            _dc.MathTaskExpectedResult = mathTask.Parts[0].Value;

            _dc.MathTaskRightKnownPart = mathTask.Parts[1].Value;
            this.RightKnownPart.IsVisible = true;
        }
        else if (mathTask.Parts.Length == 3)
        {
            _dc.MathTaskLeftKnownPart = mathTask.Parts[0].Value;
            this.LeftKnownPart.IsVisible = false;

            _dc.MathTaskExpectedResult = mathTask.Parts[1].Value;

            _dc.MathTaskRightKnownPart = mathTask.Parts[2].Value;
            this.RightKnownPart.IsVisible = true;
        }

        this.UnknownPart.Focus();
    }
}