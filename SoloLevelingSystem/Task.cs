using System;

public class Task
{
    public string Description { get; private set; }
    public int Target { get; private set; }
    public int Progress { get; private set; }
    public bool IsCompleted => Progress >= Target;

    public Task(string description, int target)
    {
        Description = description;
        Target = target;
        Progress = 0;
    }

    public void AddProgress(int amount)
    {
        if (IsCompleted)
        {
            Console.WriteLine($"Task '{Description}' is already completed!");
            return;
        }

        Progress += amount;
        if (Progress > Target) Progress = Target;

        Console.WriteLine($"Task '{Description}' progress updated: {Progress}/{Target}");

        if (IsCompleted)
        {
            Console.WriteLine($"Task '{Description}' completed!");
        }
    }

    public override string ToString()
    {
        return $"{Description} - {Progress}/{Target} ({(IsCompleted ? "Completed" : "In Progress")})";
    }
}
