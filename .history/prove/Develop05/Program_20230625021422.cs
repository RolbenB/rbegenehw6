using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public abstract class Goal
{
    public string GoalName { get; set; }
    public string GoalDescription { get; set; }
    public int GoalPoints { get; set; }

    public abstract bool IsComplete();
}

public class SimpleGoal : Goal
{
    public override bool IsComplete()
    {
        return true;
    }
}

public class EternalGoal : Goal
{
    public override bool IsComplete()
    {
        return false;
    }
}

public class ChecklistGoal : Goal
{
    private int _nmbOfTime;
    private int _awardedPoints;

    public ChecklistGoal(string goalName, string goalDescription, int goalPoints, int numberOfTimes)
    {
        GoalName = goalName;
        GoalDescription = goalDescription;
        GoalPoints = goalPoints;
        _nmbOfTime = numberOfTimes;
    }

    public override bool IsComplete()
    {
        return _nmbOfTime == 0;
    }

    public void RecordEvent()
    {
        if (_nmbOfTime > 0)
        {
            _nmbOfTime--;
            _awardedPoints += GoalPoints;
            Console.WriteLine("Event recorded!");
        }
        else
        {
            Console.WriteLine("Goal already completed the required number of times.");
        }
    }
}

public class GoalTracker
{
    private List<Goal> _goals;

    public GoalTracker()
    {
        _goals = new List<Goal>();
    }

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void DisplayGoals()
    {
        Console.WriteLine("Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Goal goal = _goals[i];
            string completionStatus = goal.IsComplete() ? "[X]" : "[ ]";
            string goalInfo = $"{completionStatus} {goal.GoalName}";
            Console.WriteLine(goalInfo);
        }
    }

    public void RecordEvent(int goalIndex)
    {
        if (goalIndex >= 0 && goalIndex < _goals.Count)
        {
            Goal goal = _goals[goalIndex];
            if (goal is ChecklistGoal checklistGoal)
            {
                checklistGoal.RecordEvent();
            }
            else
            {
                Console.WriteLine("Event recording not supported for this goal type.");
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
            Console.WriteLine("3. Record an event");
            Console.WriteLine("4. Load goals from file");
            Console.WriteLine("5. Save goals to file");
            Console.WriteLine("6. Exit");

            Console.Write("Enter your choice (1-6): ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Types of goals:");
                    Console.WriteLine("1. Simple goal");
                    Console.WriteLine("2. Eternal goal");
                    Console.WriteLine("3. Checklist goal");

                    Console.Write("Enter the type of goal you would like to create (1-3): ");
                    int goalType = int.Parse(Console.ReadLine());

                    Console.Write("Enter the goal name: ");
                    string goalName = Console.ReadLine();

                    Console.Write("Enter the goal description: ");
                    string goalDescription = Console.ReadLine();

                    Console.Write("Enter the goal points: ");
                    int goalPoints = int.Parse(Console.ReadLine());

                    if (goalType == 1)
                    {
                        Goal simpleGoal = new SimpleGoal
                        {
                            GoalName = goalName,
                            GoalDescription = goalDescription,
                            GoalPoints = goalPoints
                        };
                        tracker.AddGoal(simpleGoal);
                    }
                    else if (goalType == 2)
                    {
                        Goal eternalGoal = new EternalGoal
                        {
                            GoalName = goalName,
                            GoalDescription = goalDescription,
                            GoalPoints = goalPoints
                        };
                        tracker.AddGoal(eternalGoal);
                    }
                    else if (goalType == 3)
                    {
                        Console.Write("Enter the number of times the goal needs to be accomplished: ");
                        int numberOfTimes = int.Parse(Console.ReadLine());

                        Goal checklistGoal = new ChecklistGoal(goalName, goalDescription, goalPoints, numberOfTimes);
                        tracker.AddGoal(checklistGoal);
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
                    Console.Write("Enter the index of the goal you want to record an event for: ");
                    int eventGoalIndex = int.Parse(Console.ReadLine());
                    tracker.RecordEvent(eventGoalIndex);
                    break;
                case 4:
                    Console.Write("Enter the file name to load goals from: ");
                    string loadFileName = Console.ReadLine();
                    tracker.LoadGoals(loadFileName);
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
