using System;

class EternalGoal : Goal
{
    private string _time;

    public EternalGoal(string name, string description, int points, string time)
        : base(name, description, points)
    {
        _time = time;
    }

    public override string GetStringRepresentation()
    {
        return $"Eternal Goal:{_name},{_description},{_points},{_time}";
    }

    public override void RecordEvent()
    {
        Console.WriteLine("Event recorded for Eternal Goal.");
    }
}
