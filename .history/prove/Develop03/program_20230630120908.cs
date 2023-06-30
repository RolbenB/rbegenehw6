using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Create a list of Scripture objects
        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture("Let us go for the first text found on: John 3:16, using NLT version", "For this is how God loved the world: He gave his one and only Son, so that everyone who believes in him will not perish but have eternal life.."),
            new Scripture("Let us go for the second text found on :Proverbs 3:5-6, using NLT version", "Trust in the LORD with all your heart; do not depend on your own understanding. Seek his will in all you do, and he will show you which path to take.")
        };

        Console.WriteLine("Hello, welcome to scripture Hiding Program");
        Console.WriteLine();
        Console.WriteLine("Please, press Enter to hide more words or type 'quit' to exit.");
        Console.WriteLine("--------------------------------------------------------------");
        Console.WriteLine();

        foreach (Scripture scripture in scriptures)
        {
            while (!scripture.IsFullyHidden())
            {
                scripture.Display();

                Console.WriteLine();

                string input = Console.ReadLine();

                if (input.ToLower() == "quit")
                {
                    Console.WriteLine("Hi guess, we note that you decide to exit the program. Bye, have a good day and we invite you to read more and more about the scripture.");
                    return;
                }

                scripture.HideRandomWord();
                Console.Clear();
            }
        }

        Console.WriteLine("Hi guess, we note that all words have been hidden; the program is now exit.");
    }
}
