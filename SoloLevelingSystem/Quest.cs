using System;
using System.Collections.Generic;
using System.Linq;

public class Quest
{
    public string Title { get; private set; }
    public List<Task> Tasks { get; private set; }
    public bool IsCompleted => Tasks.All(t => t.IsCompleted);

    public Quest(string title, List<Task> tasks)
    {
        Title = title;
        Tasks = tasks;
    }

    public void AddTaskProgress(int taskIndex, int amount)
    {
        if (taskIndex < 0 || taskIndex >= Tasks.Count)
        {
            Console.WriteLine("Invalid task index!");
            return;
        }

        Tasks[taskIndex].AddProgress(amount);

        if (IsCompleted)
        {
            Console.WriteLine($"Quest '{Title}' completed!");
        }
    }

    public override string ToString()
    {
        string taskDetails = string.Join("\n", Tasks.Select((t, i) => $"{i + 1}. {t}"));
        return $"{Title}\n{taskDetails}\nStatus: {(IsCompleted ? "Completed" : "In Progress")}";
    }
}
