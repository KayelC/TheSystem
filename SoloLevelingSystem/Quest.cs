using System;
using System.Collections.Generic;
using System.Linq;

public class Quest
{
    public string Title { get; private set; }
    public List<Task> Tasks { get; private set; }
    public bool IsCompleted => Tasks.All(t => t.IsCompleted);

    private DateTime lastResetDate;

    public Quest(string title, List<Task> tasks)
    {
        Title = title;
        Tasks = tasks;
        lastResetDate = DateTime.Now.Date; // Initialize to today's date
    }

    public void AddTaskProgress(int taskIndex, int amount)
    {
        CheckForReset(); // Automatically reset if needed

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

    public void Reset()
    {
        foreach (var task in Tasks)
        {
            task.Reset();
        }
        lastResetDate = DateTime.Now.Date; // Update reset date
        Console.WriteLine($"Quest '{Title}' has been reset and is ready for a new day!");
    }

    private void CheckForReset()
    {
        if (DateTime.Now.Date > lastResetDate)
        {
            Reset();
        }
    }

    public override string ToString()
    {
        CheckForReset(); // Ensure up-to-date status
        string taskDetails = string.Join("\n", Tasks.Select((t, i) => $"{i + 1}. {t}"));
        return $"{Title}\n{taskDetails}\nStatus: {(IsCompleted ? "Completed" : "In Progress")}";
    }
}
