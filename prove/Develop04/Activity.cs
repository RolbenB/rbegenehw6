using System;
using System.Collections.Generic;
using System.Threading;

public abstract class Activity
{
    private int _duration;
    private string _name;
    private string _description;

    public int Duration { get => _duration; }
    public string Name { get => _name; }
    public string Description { get => _description; }

    public Activity(string name, string description, int duration)
    {
        _name = name;
        _description = description;
        _duration = duration;
    }

    public void StartActivity()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the {0} Activity!", _name);
        Console.WriteLine("--------------------");
        Console.WriteLine(_description);
        Console.WriteLine();

        Console.WriteLine("Get ready...");
        Thread.Sleep(3000);
        Console.WriteLine();

        Console.WriteLine("Start activity...");
        StartTimer();
        Console.WriteLine();

        Console.WriteLine("Well done! You have completed the {0} Activity for {1} seconds.", _name, _duration);
        Thread.Sleep(3000);
    }

    protected abstract void StartTimer();
}
