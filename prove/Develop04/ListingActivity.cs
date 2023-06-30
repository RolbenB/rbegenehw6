using System;
using System.Threading;

public class ListingActivity : Activity
{
    public ListingActivity(int duration) : base("Listing", "Think broadly and list as many things as you can in a certain area of strength or positivity.", duration)
    {
    }

    protected override void StartTimer()
    {
        Console.WriteLine("Please list as many things as you can:");
        Console.WriteLine("(Press Enter after each item)");

        for (int i = Duration; i > 0; i--)
        {
            Console.Write("Time remaining: {0} seconds", i);
            Thread.Sleep(1000);
            Console.SetCursorPosition(0, Console.CursorTop);
        }
    }
}
