using System;

[Serializable]
public class QuestLog
{
    public DateTime Date { get; private set; }
    public string QuestTitle { get; private set; }
    public bool WasCompleted { get; private set; }
    public string Notes { get; private set; }

    public QuestLog(string questTitle, bool wasCompleted, string notes = "")
    {
        Date = DateTime.Now;
        QuestTitle = questTitle;
        WasCompleted = wasCompleted;
        Notes = notes;
    }

    public override string ToString()
    {
        return $"{Date.ToShortDateString()} | Quest: {QuestTitle} | Status: {(WasCompleted ? "Completed" : "Failed")} | Notes: {Notes}";
    }
}
