using System;
using System.Collections.Generic;
using System.Threading;

class MindfulnessApp
{
    static Random random = new Random();

    static List<string> reflectionPrompts = new List<string>()
    {
        "Reflect on a time when you faced an accident. What was your first reflexion?",
        "Think about a moment, where you were about to leave your father's house to live with your wife/ houband. Explain your emotion",
        "Explain How usually you overcome obstacles in your life",
        "Explain your first day in a boat or airplain ",

    };

    static List<string> listingPrompts = new List<string>()
    {
        "List as many things as you can that bring you happy.",
        "Enumerate your strengths and skills.",
        "Think of positive experiences you've had in the past week and write them down.",

    };

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Menu Options: ");
            Console.WriteLine("----------------");
            Console.WriteLine("1. Start Breathing Activity");
            Console.WriteLine("2. Start Reflection Activity");
            Console.WriteLine("3. Start Listing Activity");
            Console.WriteLine("4. Quit");
            Console.WriteLine();

            Console.Write("Please, select a choice from the Menu: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PerformBreathingActivity();
                    break;
                case "2":
                    PerformReflectionActivity();
                    break;
                case "3":
                    PerformListingActivity();
                    break;
                case "4":
                    Console.WriteLine("Thank you for using the Mindfulness App. Hope to see you soon, Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please choose a number from 1 to 3 to choose an activity or 4 to exit.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Please, press any key to continue...");
            Console.ReadKey();
        }
    }

    static void PerformBreathingActivity()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Breathing Activity!");
        Console.WriteLine("----------------------------------");
        Console.WriteLine("This activity will help you relax by walking you through breathing in and out slowly. Please clear your mind and focus on your breathing.");
        Console.WriteLine();

        int duration = GetActivityDuration();
        Console.WriteLine();

        Console.WriteLine("Get ready...");
        Thread.Sleep(3000);
        Console.WriteLine();

        Console.WriteLine("Start breathing deeply...");
        StartTimer(duration);
        Console.WriteLine();

        Console.WriteLine("Well done! You have completed the Breathing Activity for {0} seconds.", duration, "congratulations!");
        Thread.Sleep(3000);
    }

    static void PerformReflectionActivity()
    {
        Console.Clear();
        Console.WriteLine("Welcome to Reflection Activity!");
        Console.WriteLine();
        Console.WriteLine("This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.");
        Console.WriteLine("===================");
        Console.WriteLine("Take a moment to reflect on a past experience where you did something really difficult.");
        Console.WriteLine("Answer the following questions to explore the details of that experience.");
        Console.WriteLine();

        int duration = GetActivityDuration();
        Console.WriteLine();

        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(3000);
        Console.WriteLine();

        Console.WriteLine("Start your reflection...");

        string prompt = GetRandomPrompt(reflectionPrompts);
        Console.WriteLine(prompt);

        StartTimer(duration);
        Console.WriteLine();

        Console.WriteLine("Well done! You have completed the Reflection Activity for {0} seconds.", duration, "congratulations!");
        Thread.Sleep(3000);
    }

    static void PerformListingActivity()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Listing Activity");
        Console.WriteLine("================");
        Console.WriteLine("Think broadly and list as many things as you can in a certain area of strength or positivity.");
        Console.WriteLine();

        int duration = GetActivityDuration();
        Console.WriteLine();

        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(3000);
        Console.WriteLine();

        Console.WriteLine("Start listing...");

        string prompt = GetRandomPrompt(listingPrompts);
        Console.WriteLine(prompt);

        StartTimer(duration);
        Console.WriteLine();

        Console.WriteLine("Fantastic! You have completed the Listing Activity for {0} seconds.", duration, "congratulations!");
        Thread.Sleep(3000);
    }

    static int GetActivityDuration()
    {
        int duration;

        while (true)
        {
            Console.Write("How long, in seconds, would you like for your session? : ");
            if (int.TryParse(Console.ReadLine(), out duration) && duration > 0)
                break;
            else
                Console.WriteLine("Invalid duration. Please enter a positive integer.");
        }

        return duration;
    }

    static void StartTimer(int duration)
    {
        for (int i = duration; i > 0; i--)
        {
            Console.Write("Time remaining: {0} seconds", i);
            Thread.Sleep(1000);
            Console.SetCursorPosition(0, Console.CursorTop);
        }
    }

    static string GetRandomPrompt(List<string> prompts)
    {
        int index = random.Next(prompts.Count);
        return prompts[index];
    }
}
