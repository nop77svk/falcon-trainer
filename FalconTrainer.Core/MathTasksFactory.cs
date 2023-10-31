namespace NoP77svk.FalconTrainer.Core;

using System;
using System.Collections.Generic;
using System.Linq;

public static class MathTasksFactory
{
    private static readonly (int Level, Range Operands1, Range Operands2, Range Results, Func<int, int, int> Calculator, Func<int, int, int, AbstractMathTaskPart[]> TaskCreator)[] _twoOperandTemplates =
    {
        (0, new Range(0, 9), new Range(0, 9), new Range(0, 10), (a, b) => a + b,
            (a, b, r) => new AbstractMathTaskPart[] { new KnownMathTaskPart($"{a} + {b} ="), new ResultMathTaskPart($"{r}") }),
        (1, new Range(0, 10), new Range(0, 9), new Range(0, 9), (a, b) => a - b,
            (a, b, r) => new AbstractMathTaskPart[] { new KnownMathTaskPart($"{a} - {b} ="), new ResultMathTaskPart($"{r}") }),
        (2, new Range(0, 10), new Range(0, 10), new Range(10, 20), (a, b) => a + b,
            (a, b, r) => new AbstractMathTaskPart[] { new KnownMathTaskPart($"{a} + {b} ="), new ResultMathTaskPart($"{r}") }),
        (3, new Range(10, 20), new Range(0, 9), new Range(10, 20), (a, b) => a - b,
            (a, b, r) => new AbstractMathTaskPart[] { new KnownMathTaskPart($"{a} - {b} ="), new ResultMathTaskPart($"{r}") }),
        (4, new Range(10, 20), new Range(0, 20), new Range(0, 20), (a, b) => a - b,
            (a, b, r) => new AbstractMathTaskPart[] { new KnownMathTaskPart($"{a} - {b} ="), new ResultMathTaskPart($"{r}") }),
        (5, new Range(0, 100), new Range(0, 100), new Range(0, 100), (a, b) => a + b,
            (a, b, r) => new AbstractMathTaskPart[] { new KnownMathTaskPart($"{a} + {b} ="), new ResultMathTaskPart($"{r}") }),
        (6, new Range(0, 100), new Range(0, 100), new Range(0, 100), (a, b) => a - b,
            (a, b, r) => new AbstractMathTaskPart[] { new KnownMathTaskPart($"{a} - {b} ="), new ResultMathTaskPart($"{r}") })
    };

    public static int MaxAllowedLevel => _twoOperandTemplates.Select(x => x.Level).Max();

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
