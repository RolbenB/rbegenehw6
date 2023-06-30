using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<Goal> goals = new List<Goal>();

    static void Main()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Create a new goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("Select a choice from the menu: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateNewGoal();
                    break;
                case "2":
                    ListGoals();
                    break;
                case "3":
                    SaveGoals();
                    break;
                case "4":
                    LoadGoals();
                    break;
                case "5":
                    RecordEvent();
                    break;
                case "6":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select again.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void CreateNewGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? : ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                CreateSimpleGoal();
                break;
            case "2":
                CreateEternalGoal();
                break;
            case "3":
                CreateChecklistGoal();
                break;
            default:
                Console.WriteLine("Invalid choice. Goal creation cancelled.");
                break;
        }
    }

    static void CreateSimpleGoal()
    {
        Console.Write("What is the name of your goal? : ");
        string name = Console.ReadLine();
        Console.Write("What is a short description of your goal?: ");
        string description = Console.ReadLine();
        Console.Write("What is the amount of points associated with this gaol? : ");
        int points = int.Parse(Console.ReadLine());

        Goal goal = new SimpleGoal(name, description, points);
        goals.Add(goal);

        Console.WriteLine("Simple goal created successfully!");
    }

    static void CreateEternalGoal()
    {
        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();
        Console.Write("What is a short description of your goal?: ");
        string description = Console.ReadLine();
        Console.Write("What is the amount of points associated with this gaol? : ");
        int points = int.Parse(Console.ReadLine());
        Console.Write("How many time does this goal need to be accomplished for a bonus: ");
        string time = Console.ReadLine();

        Goal goal = new EternalGoal(name, description, points, time);
        goals.Add(goal);

        Console.WriteLine("Eternal goal created successfully!");
    }

    static void CreateChecklistGoal()
    {
        Console.Write("What is the name of your goal?: ");
        string name = Console.ReadLine();
        Console.Write("What is a short description of your goal?: ");
        string description = Console.ReadLine();
        Console.Write("What is the amount of points associated with this gaol?: ");
        int points = int.Parse(Console.ReadLine());
        Console.Write("How many times does this goal need to be accomplished for a bonus point?: ");
        int targetCount = int.Parse(Console.ReadLine());

        Goal goal = new ChecklistGoal(name, description, points, targetCount);
        goals.Add(goal);

        Console.WriteLine("Checklist goal created successfully!");
    }

    static void ListGoals()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals found.");
            return;
        }

        Console.WriteLine("List of Goals:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"Goal {i + 1}: {goals[i].ToString()}");
        }
    }

    static void SaveGoals()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals to save.");
            return;
        }

        Console.Write("Enter the filename to save goals: ");
        string filename = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Goal goal in goals)
            {
                writer.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine("Goals saved successfully!");
    }

    static void LoadGoals()
    {
        Console.Write("Please, enter the filename to load your goals: ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found. Unable to load goals.");
            return;
        }

        goals.Clear();

        using (StreamReader reader = new StreamReader(filename))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Goal goal = CreateGoalFromString(line);
                if (goal != null)
                {
                    goals.Add(goal);
                }
            }
        }

        Console.WriteLine("Goals loaded successfully!");

        // Display loaded goals
        ListGoals();
    }

    static Goal CreateGoalFromString(string goalString)
    {
        string[] parts = goalString.Split(':');

        if (parts.Length != 2)
        {
            Console.WriteLine($" {goalString}");
            return null;
        }

        string[] values = parts[1].Split(',');

        if (values.Length < 4)
        {
            Console.WriteLine($"{goalString}");
            return null;
        }

        string goalType = parts[0];
        string name = values[0];
        string description = values[1];
        int points = int.Parse(values[2]);

        switch (goalType)
        {
            case "Simple Goal":
                return new SimpleGoal(name, description, points);
            case "Eternal Goal":
                if (values.Length != 4)
                {
                    Console.WriteLine($"{goalString}");
                    return null;
                }
                string time = values[3];
                return new EternalGoal(name, description, points, time);
            case "Checklist Goal":
                if (values.Length != 4)
                {
                    Console.WriteLine($"{goalString}");
                    return null;
                }
                int targetCount = int.Parse(values[3]);
                return new ChecklistGoal(name, description, points, targetCount);
            default:
                Console.WriteLine($"Unknown goal type: {goalType}");
                return null;
        }
    }

    static void RecordEvent()
    {
        if (goals.Count == 0)
        {
            Console.WriteLine("No goals found. Unable to record event.");
            return;
        }

        Console.WriteLine("Select a goal to record an event:");

        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"Goal {i + 1}: {goals[i].ToString()}");
        }

        Console.Write("Enter the goal number: ");
        int goalNumber = int.Parse(Console.ReadLine()) - 1;

        if (goalNumber < 0 || goalNumber >= goals.Count)
        {
            Console.WriteLine("Invalid goal number. Unable to record event.");
            return;
        }

        Goal selectedGoal = goals[goalNumber];
        selectedGoal.RecordEvent();

        Console.WriteLine("Event recorded successfully!");
    }
}
