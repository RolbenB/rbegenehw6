using System;
using System.Collections.Generic;

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
                    StartActivity(new BreathingActivity(GetActivityDuration()));
                    break;
                case "2":
                    StartReflectionActivity();
                    break;
                case "3":
                    StartActivity(new ListingActivity(GetActivityDuration()));
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

    static void StartActivity(Activity activity)
    {
        Console.Clear();
        Console.WriteLine("Welcome to the {0} Activity!", activity.Name);
        Console.WriteLine("--------------------");
        Console.WriteLine(activity.Description);
        Console.WriteLine();

        Console.WriteLine("Get ready...");
        Thread.Sleep(3000);
        Console.WriteLine();

        Console.WriteLine("Start activity...");
        activity.StartActivity();
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

        List<string> questions = new List<string>()
        {
            "How did it make you feel?",
            "What were the challenges you faced?",
            "What did you learn from this experience?",
            "How did it impact your life?"
            "How did it impact your life?"
        };

        ReflectionActivity reflectionActivity = new ReflectionActivity(duration, questions);
        StartActivity(reflectionActivity);

        Console.WriteLine();
        Console.WriteLine("Well done! You have completed the Reflection Activity for {0} seconds. Congratulations!", duration);
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
}
