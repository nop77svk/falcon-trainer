namespace NoP77svk.FalconTrainer.Core;

using System;
using System.Collections.Generic;
using System.Linq;

public static class MathTasksFactory
{
    public static readonly (int Level, string Title, Range Operands1, Range Operands2, Range Results, Func<int, int, int> Calculator, Func<int, int, int, AbstractMathTaskPart[]> TaskCreator)[] _twoOperandTemplates =
    {
        (0, "sčítanie jednociferných čísel bez prechodu cez desiatku",
            new Range(0, 9), new Range(0, 9), new Range(0, 10), (a, b) => a + b,
            (a, b, r) => new AbstractMathTaskPart[] { new KnownMathTaskPart($"{a} + {b} ="), new ResultMathTaskPart($"{r}") }),
        (1, "odčítanie jednociferných čísel bez prechodu cez desiatku",
            new Range(0, 10), new Range(0, 9), new Range(0, 9), (a, b) => a - b,
            (a, b, r) => new AbstractMathTaskPart[] { new KnownMathTaskPart($"{a} - {b} ="), new ResultMathTaskPart($"{r}") }),
        (2, "sčítanie jednociferných čísel bez prechodu cez desiatku - doplňte sčítance",
            new Range(0, 9), new Range(0, 9), new Range(0, 10), (a, b) => a + b,
            (a, b, r) => new AbstractMathTaskPart[] { new KnownMathTaskPart($"{a} +"), new ResultMathTaskPart($"{b}"), new KnownMathTaskPart($"= {r}") }),
        (2, "sčítanie jednociferných čísel bez prechodu cez desiatku - doplňte sčítance",
            new Range(0, 9), new Range(0, 9), new Range(0, 10), (a, b) => a + b,
            (a, b, r) => new AbstractMathTaskPart[] { new ResultMathTaskPart($"{a}"), new KnownMathTaskPart($"+ {b} = {r}") }),
        (3, "sčítanie jednociferných čísel s prechodom cez desiatku",
            new Range(0, 10), new Range(0, 10), new Range(10, 20), (a, b) => a + b,
            (a, b, r) => new AbstractMathTaskPart[] { new KnownMathTaskPart($"{a} + {b} ="), new ResultMathTaskPart($"{r}") }),
        (4, "odčítanie od max 20 bez prechodu cez desiatku",
            new Range(10, 20), new Range(0, 9), new Range(10, 20), (a, b) => a - b,
            (a, b, r) => new AbstractMathTaskPart[] { new KnownMathTaskPart($"{a} - {b} ="), new ResultMathTaskPart($"{r}") }),
        (5, "odčítanie od max 20 s prechodom cez desiatku",
            new Range(10, 20), new Range(0, 20), new Range(0, 20), (a, b) => a - b,
            (a, b, r) => new AbstractMathTaskPart[] { new KnownMathTaskPart($"{a} - {b} ="), new ResultMathTaskPart($"{r}") }),
        (6, "sčítanie ľubovoľných čísel od 0 do 100 bez prechodu cez 100",
            new Range(0, 100), new Range(0, 100), new Range(0, 100), (a, b) => a + b,
            (a, b, r) => new AbstractMathTaskPart[] { new KnownMathTaskPart($"{a} + {b} ="), new ResultMathTaskPart($"{r}") }),
        (7, "odčítanie ľubovoľných čísel od 0 do 100 bez prechodu do záporných výsledkov",
            new Range(0, 100), new Range(0, 100), new Range(0, 100), (a, b) => a - b,
            (a, b, r) => new AbstractMathTaskPart[] { new KnownMathTaskPart($"{a} - {b} ="), new ResultMathTaskPart($"{r}") })
    };

    public static int MaxAllowedLevel => _twoOperandTemplates.Select((_, level) => level).Max();

    public static IEnumerable<MathTask> GenerateAllPossibleTasks(int maxLevel)
    {
        if (maxLevel < 0)
            throw new ArgumentOutOfRangeException(nameof(maxLevel), maxLevel, "maxLevel supplied below 0");
        if (maxLevel > MaxAllowedLevel)
            throw new ArgumentOutOfRangeException(nameof(maxLevel), maxLevel, $"maxLevel supplied above {MaxAllowedLevel}");

        var templates = _twoOperandTemplates
            .Where(template => template.Level < maxLevel + 1)
            .OrderBy(template => template.Level);

        int taskId = 0;
        foreach (var template in templates)
        {
            foreach (AbstractMathTaskPart[] mathTaskParts in GenerateTwoOperandTaskParts(template.Operands1, template.Operands2, template.Results, template.Calculator, template.TaskCreator))
                yield return new MathTask(taskId++, mathTaskParts);
        }
    }

    internal static IEnumerable<AbstractMathTaskPart[]> GenerateTwoOperandTaskParts(Range a, Range b, Range validResults, Func<int, int, int> calculateResult, Func<int, int, int, AbstractMathTaskPart[]> createMathTask)
    {
        for (int o1 = a.Start.Value; o1 <= a.End.Value; o1++)
        {
            for (int o2 = b.Start.Value; o2 <= b.End.Value; o2++)
            {
                int result = calculateResult(o1, o2);
                if (result >= validResults.Start.Value && result <= validResults.End.Value)
                    yield return createMathTask(o1, o2, result);
            }
        }
    }
}
