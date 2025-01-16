using System;

public class Task
{
    public string Description { get; private set; }
    public int Target { get; private set; }
    public int Progress { get; private set; } // Progress remains private for general use
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

    public void Reset()
    {
        Progress = 0;
        Console.WriteLine($"Task '{Description}' progress reset.");
    }

    // New method to set progress directly (protected to prevent misuse)
    public void SetProgress(int progress)
    {
        Progress = progress;
    }

    public override string ToString()
    {
        return $"{Description} - {Progress}/{Target} ({(IsCompleted ? "Completed" : "In Progress")})";
    }
}
