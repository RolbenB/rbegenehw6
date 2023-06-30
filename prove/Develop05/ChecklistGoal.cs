using System;

class ChecklistGoal : Goal
{
    private int _targetCount;

    public ChecklistGoal(string name, string description, int points, int targetCount)
        : base(name, description, points)
    {
        _targetCount = targetCount;
    }

    public override string GetStringRepresentation()
    {
        return $"Checklist Goal:{_name},{_description},{_points},{_targetCount}";
    }

    public override void RecordEvent()
    {
        Console.WriteLine("Event recorded for Checklist Goal.");
    }
}
