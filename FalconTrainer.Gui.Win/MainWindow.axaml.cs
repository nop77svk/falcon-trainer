namespace FalconTrainer.Gui.Win;

using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using NoP77svk.FalconTrainer.Core;

public partial class MainWindow : Window
{
    private Dictionary<int, MathTask> _mathTasks = new Dictionary<int, MathTask>();

    public MainWindow()
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
    }
}