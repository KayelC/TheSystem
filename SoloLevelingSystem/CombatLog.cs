using System;

public class CombatLog
{
    public DateTime Date { get; private set; }
    public string MatchType { get; private set; } // e.g., Sparring, Competition
    public int Duration { get; private set; } // In minutes
    public string Outcome { get; private set; } // Win, Loss, Draw
    public int OpponentLevel { get; private set; } // Level of the opponent
    public int XPEarned { get; private set; }

    public CombatLog(string matchType, int duration, string outcome, int opponentLevel, int xpEarned)
    {
        Date = DateTime.Now;
        MatchType = matchType;
        Duration = duration;
        Outcome = outcome;
        OpponentLevel = opponentLevel;
        XPEarned = xpEarned;
    }

    public override string ToString()
    {
        return $"{Date.ToShortDateString()} | Match: {MatchType} | Duration: {Duration} min | Outcome: {Outcome} | Opponent Level: {OpponentLevel} | XP Earned: {XPEarned}";
    }
}
