using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

public class Quest
{
    public string Title { get; private set; }
    public List<Task> Tasks { get; set; }
    public bool IsCompleted => Tasks.All(t => t.IsCompleted);

    private List<string> MessagePool { get; set; }
    private string CurrentMessage { get; set; }

    private DateTime lastResetDate;

    public bool HasGivenReward { get; private set; }
    private int AttributePointsReward { get; set; }

    public Quest(string title, List<Task> tasks, List<string> messagePool, int attributePointsReward)
    {
        Title = title;
        Tasks = tasks;
        MessagePool = messagePool;
        CurrentMessage = GetRandomMessage();
        lastResetDate = DateTime.Now.Date;
        AttributePointsReward = attributePointsReward;
    }

    private string GetRandomMessage()
    {
        if (MessagePool != null && MessagePool.Count > 0)
        {
            Random rand = new Random();
            return MessagePool[rand.Next(0, MessagePool.Count)];
        }
        return "No message available.";
    }
    public void AddTaskProgress(int taskIndex, int amount, Player player)
    {
        CheckForReset(); // Automatically reset if needed

        if (taskIndex < 0 || taskIndex >= Tasks.Count)
        {
            Console.WriteLine("Invalid task index!");
            return;
        }

        Tasks[taskIndex].AddProgress(amount);

        if (IsCompleted && !HasGivenReward)
        {
            HasGivenReward = true; // Mark reward as given
            Console.WriteLine($"Quest '{Title}' completed!");

            if (AttributePointsReward > 0)
            {
                Console.WriteLine($"You've earned {AttributePointsReward} attribute points!");
                player.AddAttributePoints(AttributePointsReward); // Corrected reference
            }

            player.LogQuest(Title, true, "Quest completed successfully!");
        }
    }

    public virtual void Reset()
    {
        foreach (var task in Tasks)
        {
            task.Reset();
        }
        lastResetDate = DateTime.Now.Date;
        HasGivenReward = false; // Reset reward status
        Console.WriteLine($"Quest '{Title}' has been reset and is ready for a new day!");
    }

    public void CheckForReset()
    {
        if (DateTime.Now.Date > lastResetDate)
        {
            Reset();
        }
    }

    public void SaveState(string filePath = "quest_save.json")
    {
        var questData = new
        {
            Title,
            Tasks = Tasks.Select(t => new { t.Description, t.Target, t.Progress }).ToList(),
            lastResetDate
        };

        string json = JsonConvert.SerializeObject(questData, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(filePath, json);
        Console.WriteLine("Quest progress saved!");
    }

    public void LoadState(string filePath = "quest_save.json")
    {
        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                dynamic questData = JsonConvert.DeserializeObject(json);

                Title = questData.Title;
                lastResetDate = questData.lastResetDate;

                Tasks = ((IEnumerable<dynamic>)questData.Tasks).Select(task =>
                {
                    var newTask = new Task((string)task.Description, (int)task.Target);
                    newTask.SetProgress((int)task.Progress); // Set progress using the new method
                    return newTask;
                }).ToList();

                Console.WriteLine("Quest progress loaded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load quest data: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("No quest save file found. Starting fresh.");
        }
    }


    public override string ToString()
    {
        CheckForReset(); // Ensure up-to-date status
        string taskDetails = string.Join("\n", Tasks.Select((t, i) => $"{i + 1}. {t}"));
        return $"{Title}\n{CurrentMessage}\n{taskDetails}\nStatus: {(IsCompleted ? "Completed" : "In Progress")}";
    }
}
