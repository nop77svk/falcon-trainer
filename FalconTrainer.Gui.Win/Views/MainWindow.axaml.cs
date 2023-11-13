namespace NoP77svk.FalconTrainer.Gui.Win.Views;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using NoP77svk.FalconTrainer.Core;
using NoP77svk.FalconTrainer.Gui.Win.Services;
using NoP77svk.FalconTrainer.Gui.Win.ViewModels;

internal partial class MainWindow : Window
{
    internal MainWindow()
    {
        InitializeComponent();
    }

/* 2do!
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
*/
}