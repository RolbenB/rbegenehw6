using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public abstract class Goal
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }
    public bool IsCompleted { get; set; }

    public Goal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
        IsCompleted = false;
    }

    public abstract void AccomplishGoal();

    public override string ToString()
    {
        return $"Name: {Name}\nDescription: {Description}\nPoints: {Points}\nStatus: {(IsCompleted ? "Completed" : "Not Completed")}";
    }
}

public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) : base(name, description, points)
    {
    }

    public override void AccomplishGoal()
    {
        IsCompleted = true;
    }
}

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points)
    {
    }

    public override void AccomplishGoal()
    {
        // No action needed for eternal goals
    }
}

public class ChecklistGoal : Goal
{
    public int NumberOfTimes { get; set; }
    public int TimesAccomplished { get; set; }

    public ChecklistGoal(string name, string description, int points, int numberOfTimes) : base(name, description, points)
    {
        NumberOfTimes = numberOfTimes;
        TimesAccomplished = 0;
    }

    public override void AccomplishGoal()
    {
        TimesAccomplished++;
        if (TimesAccomplished >= NumberOfTimes)
        {
            IsCompleted = true;
        }
    }

    public override string ToString()
    {
        return base.ToString() + $"\nTimes Accomplished: {TimesAccomplished}/{NumberOfTimes}";
    }
}

public class GoalTracker
{
    private List<Goal> _goals;
    public int Score { get; private set; }

    public GoalTracker()
    {
        _goals = new List<Goal>();
        Score = 0;
    }

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void DisplayGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals found.");
        }
        else
        {
            Console.WriteLine("Goals:");
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"Goal {i + 1}:\n{_goals[i]}\n");
            }
        }
    }

    public void RecordEvent(int goalIndex)
    {
        if (goalIndex >= 0 && goalIndex < _goals.Count)
        {
            Goal goal = _goals[goalIndex];
            if (!goal.IsCompleted)
            {
                goal.AccomplishGoal();
                Score += goal.Points;
                Console.WriteLine("Event recorded successfully!");
            }
            else
            {
                Console.WriteLine("Goal already completed.");
            }
        }
        else
        {
            Console.WriteLine("Invalid goal index.");
        }
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
            if (File.Exists(fileName))
            {
                string jsonString = File.ReadAllText(fileName);
                _goals = JsonSerializer.Deserialize<List<Goal>>(jsonString);
                Console.WriteLine("Goals loaded successfully!");
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load goals: {ex.Message}");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        GoalTracker tracker = new GoalTracker();

        int choice = 0;
        while (choice != 6)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Create a new goal");
            Console.WriteLine("2. Display goals");
            Console.WriteLine("3. Load goals from file");
            Console.WriteLine("4. Record an event");
            Console.WriteLine("5. Save goals to file");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid choice. Please try again.");
                Console.WriteLine();
                continue;
            }

            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Types of goals:");
                    Console.WriteLine("1. Simple goal");
                    Console.WriteLine("2. Eternal goal");
                    Console.WriteLine("3. Checklist goal");
                    Console.Write("Enter the type of goal you want to create: ");

                    int goalType;
                    if (int.TryParse(Console.ReadLine(), out goalType))
                    {
                        Console.Write("Enter the goal name: ");
                        string goalName = Console.ReadLine();

                        Console.Write("Enter the goal description: ");
                        string goalDescription = Console.ReadLine();

                        Console.Write("Enter the goal points: ");
                        int goalPoints;
                        if (int.TryParse(Console.ReadLine(), out goalPoints))
                        {
                            switch (goalType)
                            {
                                case 1:
                                    Goal simpleGoal = new SimpleGoal(goalName, goalDescription, goalPoints);
                                    tracker.AddGoal(simpleGoal);
                                    Console.WriteLine("Simple goal created successfully!");
                                    break;
                                case 2:
                                    Goal eternalGoal = new EternalGoal(goalName, goalDescription, goalPoints);
                                    tracker.AddGoal(eternalGoal);
                                    Console.WriteLine("Eternal goal created successfully!");
                                    break;
                                case 3:
                                    Console.Write("Enter the number of times the goal should be accomplished: ");
                                    int numberOfTimes;
                                    if (int.TryParse(Console.ReadLine(), out numberOfTimes))
                                    {
                                        Goal checklistGoal = new ChecklistGoal(goalName, goalDescription, goalPoints, numberOfTimes);
                                        tracker.AddGoal(checklistGoal);
                                        Console.WriteLine("Checklist goal created successfully!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid number of times. Checklist goal creation failed.");
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Invalid goal type.");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid goal points. Goal creation failed.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid goal type. Goal creation failed.");
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
                    Console.WriteLine("Exiting the program...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }
}
