using System;
using System.Collections.Generic;

public class ReflectionActivity : Activity
{
    private List<string> _questions;

    public ReflectionActivity(int duration, List<string> questions) : base("Reflection", "This activity will help you reflect on a past experience where you did something special. Take a moment to reflect on a past experience where you did something really special.", duration)
    {
        _questions = questions;
    }

    protected override void StartTimer()
    {
        Console.WriteLine("Think of something that holds significance to you.");
        Console.WriteLine("Now, let's explore it through some questions:");
        Console.WriteLine();

        foreach (string question in _questions)
        {
            Console.WriteLine(question);
            Console.ReadLine();
        }

        for (int i = Duration; i > 0; i--)
        {
            Console.Write("Time remaining: {0} seconds", i);
            Thread.Sleep(1000);
            Console.SetCursorPosition(0, Console.CursorTop);
        }
    }
}
