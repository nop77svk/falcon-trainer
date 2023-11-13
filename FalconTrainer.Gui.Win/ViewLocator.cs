namespace NoP77svk.FalconTrainer.Gui.Win;
using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Win.ViewModels;

public class ViewLocator : IDataTemplate
{
    public Control Build(object? data)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));

        var name = data.GetType().FullName!.Replace("ViewModel", "View");
        var type = Type.GetType(name);

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }

        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}