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

    protected void StartTimer()
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
    private List<string> _reflectionPrompts = new List<string>()
    {
        "Reflect on a time when you faced an accident. What was your first reflection?",
        "Think about a moment, where you were about to leave your father's house to live with your wife/husband. Explain your emotions.",
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
        return "This activity will help you reflect on a past experience where you did something special.";
    }

    protected override void StartTimer()
    {
        string prompt = GetRandomPrompt(_reflectionPrompts);
        Console.WriteLine(prompt);

        base.StartTimer();
    }

    private string GetRandomPrompt(List<string> prompts)
    {
        Random random = new Random();
        int index = random.Next(prompts.Count);
        return prompts[index];
    }
}

class ListingActivity : Activity
{
    private List<string> _listingPrompts = new List<string>()
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
        string prompt = GetRandomPrompt(_listingPrompts);
        Console.WriteLine(prompt);

        base.StartTimer();
    }

    private string GetRandomPrompt(List<string> prompts)
    {
        Random random = new Random();
        int index = random.Next(prompts.Count);
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
                    PerformActivity(new BreathingActivity(GetActivityDuration()));
                    break;
                case "2":
                    PerformActivity(new ReflectionActivity(GetActivityDuration()));
                    break;
                case "3":
                    PerformActivity(new ListingActivity(GetActivityDuration()));
                    break;
                case "4":
                    Console.WriteLine("Thank you for using the Mindfulness App. Hope to see you soon. Goodbye!");
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

    static void PerformActivity(Activity activity)
    {
        activity.StartActivity();
    }
}
