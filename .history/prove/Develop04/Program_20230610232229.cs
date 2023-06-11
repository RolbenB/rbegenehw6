using System;
using System.Collections.Generic;
using System.Threading;

class Activity
{
    private int _duration;

    public Activity(int duration)
    {
        _duration = duration;
    }

    public void StartActivity()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the {0} Activity!", GetActivityName());
        Console.WriteLine("--------------------");
        Console.WriteLine(GetIntroduction());
        Console.WriteLine();

        Console.WriteLine("Get ready...");
        Thread.Sleep(3000);
        Console.WriteLine();

        Console.WriteLine("Start activity...");
        StartTimer();
        Console.WriteLine();

        Console.WriteLine("Well done! You have completed the {0} Activity for {1} seconds.", GetActivityName(), _duration);
        Thread.Sleep(3000);
    }

    protected virtual string GetActivityName()
    {
        return "Base";
    }

    protected virtual string GetIntroduction()
    {
        return "This is a base activity.";
    }

    protected virtual void StartTimer()
    {
        for (int i = _duration; i > 0; i--)
        {
            Console.Write("Time remaining: {0} seconds", i);
            Thread.Sleep(1000);
            Console.SetCursorPosition(0, Console.CursorTop);
        }
    }
}

class BreathingActivity : Activity
{
    public BreathingActivity(int duration) : base(duration)
    {
    }

    protected override string GetActivityName()
    {
        return "Breathing";
    }

    protected override string GetIntroduction()
    {
        return "This activity will help you relax by walking you through breathing in and out slowly. Please clear your mind and focus on your breathing.";
    }
}

class ReflectionActivity : Activity
{
    private static Random _random = new Random();
    private static List<string> _reflectionPrompts = new List<string>()
    {
        "Reflect on a time when you faced an accident. What was your first reflection?",
        "Think about a moment where you were about to leave your father's house to live with your spouse. Explain your emotions.",
        "Explain how you usually overcome obstacles in your life.",
        "Explain your first day in a boat or airplane."
    };

    public ReflectionActivity(int duration) : base(duration)
    {
    }

    protected override string GetActivityName()
    {
        return "Reflection";
    }

    protected override string GetIntroduction()
    {
        return "This activity will help you reflect on a past experience where you did something special. Take a moment to reflect on a past experience where you did something really special.";
    }

    protected override void StartTimer()
    {
        Console.WriteLine(GetRandomPrompt(_reflectionPrompts));
        base.StartTimer();
    }

    private string GetRandomPrompt(List<string> prompts)
    {
        int index = _random.Next(prompts.Count);
        return prompts[index];
    }
}

class ListingActivity : Activity
{
    private static Random _random = new Random();
    private static List<string> _listingPrompts = new List<string>()
    {
        "List as many things as you can that bring you happiness.",
        "Enumerate your strengths and skills.",
        "Think of positive experiences you've had in the past week and write them down."
    };

    public ListingActivity(int duration) : base(duration)
    {
    }

    protected override string GetActivityName()
    {
        return "Listing";
    }

    protected override string GetIntroduction()
    {
        return "Think broadly and list as many things as you can in a certain area of strength or positivity.";
    }

    protected override void StartTimer()
    {
        Console.WriteLine(GetRandomPrompt(_listingPrompts));
        base.StartTimer();
    }

    private string GetRandomPrompt(List<string> prompts)
    {
        int index = _random.Next(prompts.Count);
        return prompts[index];
    }
}

class MindfulnessApp
{
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
                    StartBreathingActivity();
                    break;
                case "2":
                    StartReflectionActivity();
                    break;
                case "3":
                    StartListingActivity();
                    break;
                case "4":
                    Console.WriteLine("Thank you for using the Mindfulness App. Hope to see you soon. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please take a number from 1 to 3 to choose an activity or 4 to exit.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Please, press any key to continue...");
            Console.ReadKey();
        }
    }

    static void StartBreathingActivity()
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

        Console.WriteLine("Well done! You have completed the Breathing Activity for {0} seconds. Congratulations!", duration);
        Thread.Sleep(3000);
    }

    static void StartReflectionActivity()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Reflection Activity!");
        Console.WriteLine("----------------------------------");
        Console.WriteLine("This activity will help you reflect on a past experience where you did something special. Take a moment to reflect on a past experience where you did something really special.");
        Console.WriteLine();

        int duration = GetActivityDuration();
        Console.WriteLine();

        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(3000);
        Console.WriteLine();

        Console.WriteLine("Start your reflection...");

        ReflectionActivity reflectionActivity = new ReflectionActivity(duration);
        reflectionActivity.StartActivity();

        Console.WriteLine();
        Console.WriteLine("Well done! You have completed the Reflection Activity for {0} seconds. Congratulations!", duration);
        Thread.Sleep(3000);
    }

    static void StartListingActivity()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Listing Activity!");
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Think broadly and list as many things as you can in a certain area of strength or positivity.");
        Console.WriteLine();

        int duration = GetActivityDuration();
        Console.WriteLine();

        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(3000);
        Console.WriteLine();

        Console.WriteLine("Start listing...");

        ListingActivity listingActivity = new ListingActivity(duration);
        listingActivity.StartActivity();

        Console.WriteLine();
        Console.WriteLine("Fantastic! You have completed the Listing Activity for {0} seconds. Congratulations!", duration);
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
                Console.WriteLine("Invalid duration. Please, enter a positive integer.");
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
}
