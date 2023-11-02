namespace NoP77svk.FalconTrainer.Gui.Win;

public class MainWindowModel
{
    public const string MathTaskResultPlaceholder = "?";

    public int MathTaskId { get; set; } = -1;
    public string MathTaskNaturalId { get; set; } = "a+b=?";
    public string MathTaskLeftKnownPart { get; set; } = "? + ? =";
    public string MathTaskRightKnownPart { get; set; } = string.Empty;
    public string MathTaskResultPart { get; set; } = MathTaskResultPlaceholder;

    public string MathTaskExpectedResult { get; set; } = string.Empty;
}
