using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Initialize the player
        Player player = new Player("Fitness Enthusiast");

        

        // Define weekly program for the daily quest
        var weeklyProgram = new Dictionary<DayOfWeek, List<Task>>
        {
            { DayOfWeek.Monday, new List<Task> { new Task("50 Push-Ups", 50), new Task("2 km Run", 2) } },
            { DayOfWeek.Tuesday, new List<Task> { new Task("100 Sit-Ups", 100), new Task("Stretching for 10 Minutes", 10) } },
            { DayOfWeek.Wednesday, new List<Task> { new Task("30 Burpees", 30), new Task("Yoga for 15 Minutes", 15) } },
            { DayOfWeek.Thursday, new List<Task> { new Task("Plank for 2 Minutes", 2), new Task("3 km Run", 3) } },
            { DayOfWeek.Friday, new List<Task> { new Task("50 Squats", 50), new Task("Stretching for 20 Minutes", 20) } },
            { DayOfWeek.Saturday, new List<Task> { new Task("5 km Run", 5), new Task("Cycling for 30 Minutes", 30) } },
            { DayOfWeek.Sunday, new List<Task> { new Task("Meditation for 15 Minutes", 15) } }
        };

        // Create quests
        List<Quest> availableQuests = new List<Quest>
        {
            new DailyQuest("Daily Quest", weeklyProgram,
                new List<string>
                {
                    "Strength Training Has Arrived",
                    "Train To Become a Formidable Combatant",
                    "Getting Ready To Become Powerful",
                    "Prepare To Break Your Limits",
                    "The Path To Greatness Has Been Set Before You",
                    "Only Through Hardship Will You Find True Strenght",
                    "Your Body is Your Weapnon- Sharpen It",
                    "Fate Favors the Bold- Seize Your Strength",
                    "The Strong Walk a Path of No Return- Are You Ready?",
                    "Power Will Be Yours, If You Dare Reach for it"
                },
                3), // Attribute points reward
            new Quest("Penalty Quest", new List<Task>
            {
                new Task("Survive For The Allotted Time", 4),
            },
            new List<string>
            {
                "You Have Failed- Now Face The Consequences",
                "The Price of Failure is Steep- Can You Pay It?"
            },
            3),
            new Quest("Job Change Quest", new List<Task>
            {
                new Task("Spar", 10),
                new Task("Win a Bout", 1)
            },
            new List<string>
            {
                "The Time Has Come To Prove Your Worth"
            },
            20)
        };

        Quest currentQuest = availableQuests[0]; // Default to the first quest

        // Main Loop
        while (true)
        {
            ResetAllQuests(availableQuests); // Ensure all quests are up-to-date

            Console.Clear();
            Console.WriteLine("===== Fitness Journal =====");
            Console.WriteLine($"Player: {player.Name}, Level: {player.Level}, XP: {player.CurrentXP}/{player.XPToNextLevel}");
            Console.WriteLine($"Stats: STR={player.Strength}, AGI={player.Agility}, VIT={player.Vitality}, INT={player.Intelligence}, PER={player.Perception}");
            Console.WriteLine($"Unallocated Points: {player.UnallocatedAttributePoints}");
            Console.WriteLine("\n[1] Quests\n[2] Distribute Attribute Points\n[3] Load Progress\n[4] Save and Exit\n[5] Log Combat\n[6] View Combat Logs\n[7] View Quest Logs");
            Console.Write("Choose an action: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    HandleQuestMenu(ref currentQuest, availableQuests, player);
                    break;

                case "2":
                    player.DistributeAttributePoints();
                    break;

                case "3":
                    Console.WriteLine("Loading Progress...");
                    player.LoadProgress();
                    currentQuest.LoadState();
                    player.LoadCombatLogs(); // Load combat logs
                    player.LoadQuestLogs(); // Load quest logs
                    break;

                case "4":
                    Console.WriteLine("Saving Progress...");
                    player.SaveProgress();
                    currentQuest.SaveState();
                    player.SaveCombatLogs(); // Save combat logs
                    player.SaveQuestLogs(); // Save quest logs
                    Console.WriteLine("Exiting...");
                    return;

                case "5":
                    LogCombat(player);
                    break;

                case "6":
                    player.ViewCombatLogs();
                    break;

                case "7":
                    player.ViewQuestLogs();
                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }


    static void LogCombat(Player player)
    {
        Console.Clear();
        Console.WriteLine("=== Log Combat ===");

        // Match Type Validation
        string matchType;
        while (true)
        {
            Console.Write("Enter Match Type (Sparring/Competition): ");
            matchType = Console.ReadLine()?.Trim().ToLower();
            if (matchType == "sparring" || matchType == "competition")
            {
                break;
            }
            Console.WriteLine("Invalid match type. Please enter 'Sparring' or 'Competition'.");
        }

        // Duration Validation
        int duration;
        while (true)
        {
            Console.Write("Enter Duration (in minutes): ");
            if (int.TryParse(Console.ReadLine(), out duration) && duration > 0)
            {
                break;
            }
            Console.WriteLine("Invalid duration. Please enter a positive integer.");
        }

        // Outcome Validation
        string outcome;
        while (true)
        {
            Console.Write("Enter Outcome (Win/Loss/Draw): ");
            outcome = Console.ReadLine()?.Trim().ToLower();
            if (outcome == "win" || outcome == "loss" || outcome == "draw")
            {
                break;
            }
            Console.WriteLine("Invalid outcome. Please enter 'Win', 'Loss', or 'Draw'.");
        }

        // Opponent Level Validation
        string opponentLevel;
        while (true)
        {
            Console.Write("Enter Opponent Level (Easy/Normal/Hard/Very Hard): ");
            opponentLevel = Console.ReadLine()?.Trim().ToLower();
            if (opponentLevel == "easy" || opponentLevel == "normal" || opponentLevel == "hard" || opponentLevel == "very hard")
            {
                break;
            }
            Console.WriteLine("Invalid level. Please enter 'Easy', 'Normal', 'Hard', or 'Very Hard'.");
        }

        // Calculate XP based on opponent level
        int opponentLevelBonus;
        switch (opponentLevel)
        {
            case "easy":
                opponentLevelBonus = 1;
                break;
            case "normal":
                opponentLevelBonus = 2;
                break;
            case "hard":
                opponentLevelBonus = 3;
                break;
            case "very hard":
                opponentLevelBonus = 4;
                break;
            default:
                opponentLevelBonus = 0; // Fallback, shouldn't be hit due to validation
                break;
        }

        // Log combat details
        int xpEarned = Player.CalculateCombatXP(matchType, duration, outcome, opponentLevelBonus);
        player.LogCombat(matchType, duration, outcome, opponentLevelBonus);
        Console.WriteLine($"Combat logged: {matchType}, Duration: {duration} min, Outcome: {outcome}, Opponent Level: {opponentLevel}, XP Earned: {xpEarned}");
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
                currentQuest.AddTaskProgress(taskIndex - 1, progressAmount, player); // Quest handles rewards internally
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
