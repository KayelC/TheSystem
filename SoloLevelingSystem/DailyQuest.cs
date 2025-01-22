using System.Collections.Generic;
using System;

public class DailyQuest : Quest
{
    private Dictionary<DayOfWeek, List<Task>> WeeklyProgram { get; set; }

    public DailyQuest(string title, Dictionary<DayOfWeek, List<Task>> weeklyProgram, List<string> messagePool, int attributePointsReward)
        : base(title, new List<Task>(), messagePool, attributePointsReward)
    {
        WeeklyProgram = weeklyProgram;
        AssignTasksForToday();
    }

    private void AssignTasksForToday()
    {
        DayOfWeek today = DateTime.Now.DayOfWeek;

        if (WeeklyProgram.ContainsKey(today))
        {
            Tasks = WeeklyProgram[today];
            Console.WriteLine($"Tasks for {today} have been assigned!");
        }
        else
        {
            Console.WriteLine($"No tasks defined for {today}. Quest is empty.");
        }
    }

    public override void Reset()
    {
        base.Reset();
        AssignTasksForToday(); // Reassign tasks for the current day
    }
}
