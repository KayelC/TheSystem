using System;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using System.Collections.Generic;

public class Player
{
    public string Name { get; private set; }
    public int Level { get; private set; }
    public int CurrentXP { get; private set; }
    public int XPToNextLevel { get; private set; }

    // Stats
    public int Strength { get; private set; }
    public int Agility { get; private set; }
    public int Intelligence { get; private set; }
    public int Vitality { get; private set; }
    public int Perception { get; private set; }
    public int UnallocatedAttributePoints { get; private set; }

    public List<CombatLog> CombatLogs { get; private set; }

    public Player(string name)
    {
        Name = name;
        Level = 1;
        CurrentXP = 0;
        XPToNextLevel = 100;

        // Initial Stats
        Strength = 10;
        Agility = 10;
        Intelligence = 10;
        Vitality = 10;
        Perception = 10;
        UnallocatedAttributePoints = 0;

        CombatLogs = new List<CombatLog>();
    }

    public void LogCombat(string matchType, int duration, string outcome, int opponentLevel)
    {
        int xpEarned = CalculateCombatXP(duration, outcome, opponentLevel);
        GainXP(xpEarned);

        CombatLogs.Add(new CombatLog(matchType, duration, outcome, opponentLevel, xpEarned));
        Console.WriteLine($"Combat logged: {matchType}, Duration: {duration} min, Outcome: {outcome}, Opponent Level: {opponentLevel}, XP Earned: {xpEarned}");
    }

    private int CalculateCombatXP(int duration, string outcome, int opponentLevel)
    {
        int baseXP = 10 * duration; // Base XP based on duration
        int outcomeMultiplier = outcome == "Win" ? 2 : outcome == "Draw" ? 1 : 0; // Multiplier for outcomes
        int levelDifference = opponentLevel - Level;
        int levelBonus = levelDifference > 0 ? levelDifference * 5 : 0; // Bonus for fighting stronger opponents

        return baseXP * outcomeMultiplier + levelBonus;
    }

    public void ViewCombatLogs()
    {
        Console.Clear();
        Console.WriteLine("=== Combat History ===");
        foreach (var log in CombatLogs)
        {
            Console.WriteLine(log);
        }
    }

    public void AddAttributePoints(int points)
    {
        UnallocatedAttributePoints += points;
        Console.WriteLine($"You currently have {UnallocatedAttributePoints} unallocated points.");
    }

    public void DistributeAttributePoints()
    {
        while (UnallocatedAttributePoints > 0)
        {
            Console.Clear();
            Console.WriteLine("Distribute your attribute points:");
            Console.WriteLine($"1. Strength: {Strength}");
            Console.WriteLine($"2. Agility: {Agility}");
            Console.WriteLine($"3. Intelligence: {Intelligence}");
            Console.WriteLine($"4. Vitality: {Vitality}");
            Console.WriteLine($"5. Perception: {Perception}");
            Console.WriteLine($"Unallocated Points: {UnallocatedAttributePoints}");
            Console.Write("Choose a stat to increase (1-4): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Strength++;
                    break;
                case "2":
                    Agility++;
                    break;
                case "3":
                    Intelligence++;
                    break;
                case "4":
                    Vitality++;
                    break;
                case "5":
                    Perception++;
                    break;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    continue;
            }

            UnallocatedAttributePoints--;
            Console.WriteLine("Point allocated!");
        }

        Console.WriteLine("All points have been distributed!");
    }
    public void GainXP(int amount)
    {
        Console.WriteLine($"{Name} gained {amount} XP!");
        CurrentXP += amount;

        if (CurrentXP >= XPToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        Level++;
        CurrentXP -= XPToNextLevel; // Carry over excess XP
        XPToNextLevel += 50; // Increase XP threshold

        // Increase Stats
        Strength += 1;
        Agility += 1;
        Intelligence += 1;
        Vitality += 1;
        Perception += 1;

        Console.WriteLine("=== LEVEL UP! ===");
        Console.WriteLine($"{Name} is now Level {Level}!");
        Console.WriteLine("Stats increased!");
    }


    // Save Player Progress
    public void SaveProgress(string filePath = "player_save.json")
    {
        var playerData = new
        {
            Name,
            Level,
            CurrentXP,
            XPToNextLevel,
            Strength,
            Agility,
            Intelligence,
            Vitality,
            Perception,
            UnallocatedAttributePoints
        };
        string json = JsonConvert.SerializeObject(playerData, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(filePath, json);
        Console.WriteLine("Player progress saved!");
    }

    // Load Player Progress
    public void LoadProgress(string filePath = "player_save.json")
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            dynamic playerData = JsonConvert.DeserializeObject(json);
            Name = playerData.Name;
            Level = playerData.Level;
            CurrentXP = playerData.CurrentXP;
            XPToNextLevel = playerData.XPToNextLevel;
            Strength = playerData.Strength;
            Agility = playerData.Agility;
            Intelligence = playerData.Intelligence;
            Vitality = playerData.Vitality;
            Perception = playerData.Perception;
            UnallocatedAttributePoints = playerData.UnallocatedAttributePoints;

            Console.WriteLine("Player progress loaded!");
        }
        else
        {
            Console.WriteLine("No save file found. Starting fresh.");
        }
    }
}
