namespace FalconTrainer.Gui.Win;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using NoP77svk.FalconTrainer.Core;

internal partial class MainWindow : Window
{
    private Dictionary<int, MathTask> _mathTasks = new Dictionary<int, MathTask>();
    private int _maxTaskId => _mathTasks.Last().Key; // 2do! unhandled exception!

    internal MainWindow()
    {
        InitializeComponent();

        try
        {
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
        int taskId = Random.Shared.Next(0, _maxTaskId + 1);
        return _mathTasks[taskId];
    }

    private void ShowTask(MathTask mathTask)
    {
        this.UnknownPart.Text = "?";

        // 2do! rework to dynamically contstructing/deconstructing the known/unknown parts and their styles
        if (mathTask.Parts.Length == 2 && mathTask.Parts[0] is KnownMathTaskPart)
        {
            this.LeftKnownPart.Content = mathTask.Parts[0].Value;
            this.LeftKnownPart.IsVisible = true;
            this.UnknownPart.Tag = mathTask.Parts[1].Value;
            this.RightKnownPart.Content = string.Empty;
            this.RightKnownPart.IsVisible = false;
        }
        else if (mathTask.Parts.Length == 2 && mathTask.Parts[0] is ResultMathTaskPart)
        {
            this.LeftKnownPart.Content = string.Empty;
            this.LeftKnownPart.IsVisible = false;
            this.UnknownPart.Tag = mathTask.Parts[0].Value;
            this.RightKnownPart.Content = mathTask.Parts[1].Value;
            this.RightKnownPart.IsVisible = true;
        }
        else if (mathTask.Parts.Length == 3)
        {
            this.LeftKnownPart.Content = mathTask.Parts[0].Value;
            this.LeftKnownPart.IsVisible = false;
            this.UnknownPart.Tag = mathTask.Parts[1].Value;
            this.RightKnownPart.Content = mathTask.Parts[2].Value;
            this.RightKnownPart.IsVisible = true;
        }

        this.UnknownPart.Focus();
    }
}