namespace NoP77svk.FalconTrainer.Gui.Win;

public class MainWindowModel
    : ViewModelBase
{
    public int MathTaskId { get; set; }
    public string MathTaskNaturalId { get; set; }
    public string MathTaskLeftKnownPart { get; set; }
    public string MathTaskRightKnownPart { get; set; }
    public const string MathTaskResultPlaceholder = "?";
    public string MathTaskExpectedResult { get; set; }
}
