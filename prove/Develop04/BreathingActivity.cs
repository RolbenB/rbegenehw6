using System;

public class BreathingActivity : Activity
{
    public BreathingActivity(int duration) : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Please clear your mind and focus on your breathing.", duration)
    {
    }

    protected override void StartTimer()
    {
        for (int i = Duration; i > 0; i--)
        {
            Console.Write("Time remaining: {0} seconds", i);
            Thread.Sleep(1000);
            Console.SetCursorPosition(0, Console.CursorTop);
        }
    }
}
