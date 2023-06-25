using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public abstract class Goal
{
    public string GoalName { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }

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
    private int _awardedPoints;

    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
        _awardedPoints = 0;
    }

    public override void RecordEvent()
    {
        _awardedPoints += Points;
    }

    public override bool IsComplete()
    {
        return _awardedPoints >= Points;
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

public class ChecklistGoal : EternalGoal
{
    private int _bonusPoints;
    private int _nmbOfTime;

    public ChecklistGoal(string name, string description, int points, int bonusPoints, int nmbOfTime) : base(name, description, points)
    {
        _bonusPoints = bonusPoints;
        _nmbOfTime = nmbOfTime;
    }

    public override void RecordEvent()
    {
        _nmbOfTime++;
        if (_nmbOfTime <= Points)
            _awardedPoints += _bonusPoints;
        if (_nmbOfTime == Points)
            _awardedPoints += 500; // Bonus points on completing checklist goal
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
                string checklistInfo = $"Completed {checklistGoal.NmbOfTime}/{checklistGoal.Points} times";
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
            Console.WriteLine($"Failed to save goals. Error: {ex.Message}");
        }
    }

    public void LoadGoals(string fileName)
    {
        try
        {
            if (File.Exists(fileName))
            {
                string jsonString = File.ReadAllText(fileName);
                _goals = JsonSerializer.Deserialize<List<Goal>>(jsonString);
                Console.WriteLine("Goals loaded successfully!");
            }
            else
            {
                Console.WriteLine("File not found. No goals loaded.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load goals. Error: {ex.Message}");
        }
    }
}

public class Program
{
    static void Main()
    {
        GoalTracker tracker = new GoalTracker();

        int choice = 0;

        while (choice != 6)
        {
            Console.WriteLine("You have " + tracker.Score + " points.");
            Console.WriteLine("Menu options:");
            Console.WriteLine("  1. Create New goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Load Goals");
            Console.WriteLine("  4. Record event");
            Console.WriteLine("  5. Save Goals");
            Console.WriteLine("  6. Quit");

            Console.Write("Select a choice from the menu: ");
            int.TryParse(Console.ReadLine(), out choice);

            switch (choice)
            {
                case 1:
                    Console.WriteLine("The types of goals are:");
                    Console.WriteLine("1. Simple Goal");
                    Console.WriteLine("2. Eternal Goal");
                    Console.WriteLine("3. Checklist Goal");
                    Console.Write("Enter the type of goal you would like to create: ");
                    int goalType;
                    if (int.TryParse(Console.ReadLine(), out goalType))
                    {
                        switch (goalType)
                        {
                            case 1:
                                Console.Write("Enter goal name: ");
                                string simpleGoalName = Console.ReadLine();
                                Console.Write("Enter goal description: ");
                                string simpleGoalDescription = Console.ReadLine();
                                Console.Write("Enter goal points: ");
                                int simpleGoalPoints;
                                if (int.TryParse(Console.ReadLine(), out simpleGoalPoints))
                                {
                                    SimpleGoal simpleGoal = new SimpleGoal(simpleGoalName, simpleGoalDescription, simpleGoalPoints);
                                    tracker.AddGoal(simpleGoal);
                                    Console.WriteLine("Simple goal created successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid goal points. Simple goal creation failed.");
                                }
                                break;
                            case 2:
                                Console.Write("Enter goal name: ");
                                string eternalGoalName = Console.ReadLine();
                                Console.Write("Enter goal description: ");
                                string eternalGoalDescription = Console.ReadLine();
                                Console.Write("Enter goal points: ");
                                int eternalGoalPoints;
                                if (int.TryParse(Console.ReadLine(), out eternalGoalPoints))
                                {
                                    EternalGoal eternalGoal = new EternalGoal(eternalGoalName, eternalGoalDescription, eternalGoalPoints);
                                    tracker.AddGoal(eternalGoal);
                                    Console.WriteLine("Eternal goal created successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid goal points. Eternal goal creation failed.");
                                }
                                break;
                            case 3:
                                Console.Write("Enter goal name: ");
                                string checklistGoalName = Console.ReadLine();
                                Console.Write("Enter goal description: ");
                                string checklistGoalDescription = Console.ReadLine();
                                Console.Write("Enter goal points: ");
                                int checklistGoalPoints;
                                if (int.TryParse(Console.ReadLine(), out checklistGoalPoints))
                                {
                                    Console.Write("Enter bonus points: ");
                                    int bonusPoints;
                                    if (int.TryParse(Console.ReadLine(), out bonusPoints))
                                    {
                                        Console.Write("Enter the number of times the goal must be accomplished: ");
                                        int numberOfTimes;
                                        if (int.TryParse(Console.ReadLine(), out numberOfTimes))
                                        {
                                            ChecklistGoal checklistGoal = new ChecklistGoal(checklistGoalName, checklistGoalDescription, checklistGoalPoints, bonusPoints, numberOfTimes);
                                            tracker.AddGoal(checklistGoal);
                                            Console.WriteLine("Checklist goal created successfully!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid number of times. Checklist goal creation failed.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid bonus points. Checklist goal creation failed.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid goal points. Checklist goal creation failed.");
                                }
                                break;
                            default:
                                Console.WriteLine("Invalid goal type.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid goal type.");
                    }
                    break;
                case 2:
                    tracker.DisplayGoals();
                    break;
                case 3:
                    Console.Write("Enter the file name to load goals from: ");
                    string loadFileName = Console.ReadLine();
                    tracker.LoadGoals(loadFileName);
                    break;
                case 4:
                    Console.Write("Enter the goal index to record an event: ");
                    int goalIndex;
                    if (int.TryParse(Console.ReadLine(), out goalIndex))
                    {
                        tracker.RecordEvent(goalIndex);
                        Console.WriteLine("Event recorded successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid goal index. Event recording failed.");
                    }
                    break;
                case 5:
                    Console.Write("Enter the file name to save goals to: ");
                    string saveFileName = Console.ReadLine();
                    tracker.SaveGoals(saveFileName);
                    break;
                case 6:
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }
}
