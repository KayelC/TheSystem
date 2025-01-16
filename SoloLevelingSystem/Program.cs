using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Initialize the player
        Player player = new Player("Fitness Enthusiast");

        // Create quests
        List<Quest> availableQuests = new List<Quest>
    {
        new Quest("Daily Quest", new List<Task>
        {
            new Task("100 Push-Ups", 100),
            new Task("Stretching for 15 Minutes", 15),
            new Task("3 km Run", 3)
        }, "[Daily Quest : Strength Training Has Arrived.]"),
            
        new Quest("Penalty Quest", new List<Task>
        {
            new Task("Survive For The Alloted Time", 4),
        }, "[Penalty Quest : Survival]"),

        new Quest("Job Change Quest", new List<Task>
        {
            new Task("Spar", 10),
            new Task("Win a Bout", 1)
        }, "[Job Change Quest : Meet The Requirements To Change Your Job]")
    };

        Quest currentQuest = availableQuests[0]; // Default to the first quest

        // Main Loop
        while (true)
        {
            ResetAllQuests(availableQuests); // Ensure all quests are up-to-date


            Console.Clear();
            Console.WriteLine("===== Fitness Journal =====");
            Console.WriteLine($"Player: {player.Name}, Level: {player.Level}, XP: {player.CurrentXP}/{player.XPToNextLevel}");
            Console.WriteLine($"Stats: STR={player.Strength}, AGI={player.Agility}, VIT={player.Vitality}");
            Console.WriteLine("\n[1] Quests\n[2] Load Progress\n[3] Save and Exit");
            Console.Write("Choose an action: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    HandleQuestMenu(ref currentQuest, availableQuests, player);
                    break;

                case "2":
                    Console.WriteLine("Loading Progress...");
                    player.LoadProgress();
                    currentQuest.LoadState();
                    break;

                case "3":
                    Console.WriteLine("Saving Progress...");
                    player.SaveProgress();
                    currentQuest.SaveState();
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    static void HandleQuestMenu(ref Quest currentQuest, List<Quest> availableQuests, Player player)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== Quest Menu =====");
            Console.WriteLine("[1] Select A Quest\n[2] Current Quest Details\n[3] Add Progress to Task\n[4] Back to Main Menu");
            Console.Write("Choose an action: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    currentQuest = SelectQuest(availableQuests);
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("=== Current Quest Details ===");
                    Console.WriteLine(currentQuest);
                    break;

                case "3":
                    AddProgressToTask(currentQuest, player);
                    break;

                case "4":
                    return;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }


    static Quest SelectQuest(List<Quest> availableQuests)
    {
        Console.Clear();
        Console.WriteLine("=== Select A Quest ===");

        for (int i = 0; i < availableQuests.Count; i++)
        {
            Console.WriteLine($"[{i + 1}] {availableQuests[i].Title}");
        }

        Console.Write("Choose a quest: ");
        if (int.TryParse(Console.ReadLine(), out int questIndex) && questIndex > 0 && questIndex <= availableQuests.Count)
        {
            Console.WriteLine($"You selected: {availableQuests[questIndex - 1].Title}");
            return availableQuests[questIndex - 1];
        }

        Console.WriteLine("Invalid choice. Returning to quest menu.");
        return availableQuests[0]; // Default fallback
    }


    static void AddProgressToTask(Quest currentQuest, Player player)
    {
        Console.Clear();
        Console.WriteLine("=== Add Progress to Task ===");
        Console.WriteLine(currentQuest);

        Console.Write("Select a task to update progress: ");
        if (int.TryParse(Console.ReadLine(), out int taskIndex) && taskIndex > 0 && taskIndex <= currentQuest.Tasks.Count)
        {
            Console.Write("Enter progress amount: ");
            if (int.TryParse(Console.ReadLine(), out int progressAmount))
            {
                currentQuest.AddTaskProgress(taskIndex - 1, progressAmount);

                if (currentQuest.IsCompleted && !currentQuest.HasGivenReward)
                {
                    player.GainXP(100); // Example reward
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
    }


    static void ResetAllQuests(List<Quest> availableQuests)
    {
        foreach (var quest in availableQuests)
        {
            quest.CheckForReset();
        }
    }
}
