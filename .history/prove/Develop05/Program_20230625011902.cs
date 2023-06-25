using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public abstract class Goal
{
    private string _goalName;
    private string _description;
    private int _points;

    public string GoalName { get => _goalName; set => _goalName = value; }
    public string Description { get => _description; set => _description = value; }
    public int Points { get => _points; set => _points = value; }

    public Goal(string name, string description, int points)
    {
        GoalName = name;
        Description = description;
        Points = points;
    }

    public abstract void RecordEvent();
    public abstract bool IsComplete();
}

public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
    }

    public override void RecordEvent()
    {
        // No need to implement for simple goals
    }

    public override bool IsComplete()
    {
        return true; // Simple goals are always considered complete
    }
}

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points)
    {
    }

    public override void RecordEvent()
    {
        // No need to implement for eternal goals
    }

    public override bool IsComplete()
    {
        return false; // Eternal goals are never considered complete
    }
}

public class ChecklistGoal : Goal
{
    private int _bonusPoints;
    private int _nmbOfTime;

    public int NumberOfTimesCompleted => _nmbOfTime;

    public ChecklistGoal(string name, string description, int points, int bonusPoints) : base(name, description, points)
    {
        _bonusPoints = bonusPoints;
        _nmbOfTime = 0;
    }

    public override void RecordEvent()
    {
        _nmbOfTime++;
    }

    public override bool IsComplete()
    {
        return _nmbOfTime >= Points;
    }
}

public class GoalTracker
{
    private List<Goal> _goals;
    private int _score;

    public int Score => _score;

    public GoalTracker()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void RecordEvent(int goalIndex)
    {
        if (goalIndex >= 0 && goalIndex < _goals.Count)
        {
            Goal goal = _goals[goalIndex];
            goal.RecordEvent();
            _score += goal.IsComplete() ? goal.Points : 0;
        }
    }

    public void DisplayGoals()
    {
        Console.WriteLine("Current Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Goal goal = _goals[i];
            string completionStatus = goal.IsComplete() ? "[X]" : "[ ]";
            string goalInfo = $"{completionStatus} {goal.GoalName}";

            if (goal is ChecklistGoal checklistGoal)
            {
                string checklistInfo = $"Completed {checklistGoal.NumberOfTimesCompleted}/{checklistGoal.Points} times";
                goalInfo += $" ({checklistInfo})";
            }

            Console.WriteLine(goalInfo);
        }
        Console.WriteLine();
    }

    public void SaveGoals(string fileName)
    {
        try
        {
            string jsonString = JsonSerializer.Serialize(_goals);
            File.WriteAllText(fileName, jsonString);
            Console.WriteLine("Goals saved successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save goals: {ex.Message}");
        }
    }

    public void LoadGoals(string fileName)
    {
        try
        {
            string jsonString = File.ReadAllText(fileName);
            _goals = JsonSerializer.Deserialize<List<Goal>>(jsonString);
            Console.WriteLine("Goals loaded successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load goals: {ex.Message}");
        }
    }
}

public class Program
{
    static void Main()
    {
        GoalTracker tracker = new GoalTracker();

        SimpleGoal marathonGoal = new SimpleGoal("Marathon Goal", "Run a marathon", 1000);
        tracker.AddGoal(marathonGoal);

        EternalGoal scripturesGoal = new EternalGoal("Scriptures Goal", "Read scriptures", 100);
        tracker.AddGoal(scripturesGoal);

        ChecklistGoal templeGoal = new ChecklistGoal("Temple Goal", "Attend the temple", 50, 500);
        tracker.AddGoal(templeGoal);

        int choice = 0;

        while (choice != 5)
        {
            Console.WriteLine("You have " + tracker.Score + " points.");
            Console.WriteLine("Menu options:");
            Console.WriteLine("  1. Create New goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Load Goals");
            Console.WriteLine("  4. Record event");
            Console.WriteLine("  5. Quit");

            Console.Write("Select a choice from the menu: ");
            int.TryParse(Console.ReadLine(), out choice);

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Creating a New goal...");
                    Console.Write("Enter goal name: ");
                    string goalName = Console.ReadLine();
                    Console.Write("Enter goal description: ");
                    string goalDescription = Console.ReadLine();
                    Console.Write("Enter goal points: ");
                    int goalPoints;
                    int.TryParse(Console.ReadLine(), out goalPoints);
                    Goal newGoal = new SimpleGoal(goalName, goalDescription, goalPoints);
                    tracker.AddGoal(newGoal);
                    Console.WriteLine("Goal created successfully!");
                    break;
                case 2:
                    Console.WriteLine("Listing Goals...");
                    tracker.DisplayGoals();
                    break;
                case 3:
                    Console.WriteLine("Loading Goals...");
                    Console.Write("Enter the file name to load goals from: ");
                    string loadFileName = Console.ReadLine();
                    tracker.LoadGoals(loadFileName);
                    break;
                case 4:
                    Console.WriteLine("Recording Event...");
                    Console.Write("Select a goal to record event: ");
                    tracker.DisplayGoals();
                    Console.Write("Enter goal index: ");
                    int goalIndex;
                    int.TryParse(Console.ReadLine(), out goalIndex);
                    tracker.RecordEvent(goalIndex);
                    Console.WriteLine("Event recorded successfully!");
                    break;
                case 5:
                    Console.WriteLine("Quitting...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }
}
