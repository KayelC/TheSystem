using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Initialize the player
        Player player = new Player("Fitness Enthusiast");

        // Create a daily fitness quest
        List<Task> dailyTasks = new List<Task>
        {
            new Task("100 Push-Ups", 100),
            new Task("Stretching for 15 Minutes", 15),
            new Task("3 km Run", 3)
        };
        Quest dailyQuest = new Quest("Daily Fitness Quest", dailyTasks);

        // Main Loop
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== Fitness Journal =====");
            Console.WriteLine($"Player: {player.Name}, Level: {player.Level}, XP: {player.CurrentXP}/{player.XPToNextLevel}");
            Console.WriteLine($"Stats: STR={player.Strength}, AGI={player.Agility}, VIT={player.Vitality}");
            Console.WriteLine("\n1. View Daily Quest\n2. Add Progress to Task\n3. Reset Quest\n4. Exit");
            Console.Write("Choose an action: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine(dailyQuest);
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("Select a task to update progress:");
                    for (int i = 0; i < dailyQuest.Tasks.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {dailyQuest.Tasks[i].Description}");
                    }

                    Console.Write("Task number: ");
                    if (int.TryParse(Console.ReadLine(), out int taskIndex) && taskIndex > 0 && taskIndex <= dailyQuest.Tasks.Count)
                    {
                        Console.Write("Progress amount: ");
                        if (int.TryParse(Console.ReadLine(), out int progressAmount))
                        {
                            dailyQuest.AddTaskProgress(taskIndex - 1, progressAmount);

                            if (dailyQuest.IsCompleted)
                            {
                                player.GainXP(100); // Example reward for completing daily quest
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid progress amount!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid task number!");
                    }
                    break;

                case "3":
                    Console.Clear();
                    dailyQuest.Reset();
                    break;

                case "4":
                    Console.WriteLine("Exiting the Fitness Journal...");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
