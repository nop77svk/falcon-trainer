namespace NoP77svk.FalconTrainer.Gui.Win.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoP77svk.FalconTrainer.Core;

internal class MathTasksRepository
{
    private Dictionary<int, MathTask> _mathTasks = new Dictionary<int, MathTask>();
    private int _maxTaskId => _mathTasks.Last().Key; // 2do! unhandled exception!

    public MathTasksRepository(int maxLevel)
    {
        try
        {
            // 2do! move to its own class MathTaskRepository
            _mathTasks = MathTasksFactory.GenerateAllPossibleTasks(maxLevel)
                .ToDictionary(task => task.Id);
        }
        catch (ArgumentOutOfRangeException e)
        {
            throw new Exception("This should not have happened!", e); // 2do!
        }
    }

    public MathTask GetRandomTask()
    {
        int taskId = Random.Shared.Next(0, _maxTaskId + 1);
        return _mathTasks[taskId];
    }
}
