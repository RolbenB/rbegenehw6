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
        Console.WriteLine("<><><><> +++++++ <><><><><> ++++++ <><><><><><> +++++ <><><><><><><><><>");
        Console.WriteLine();

        foreach (Scripture scripture in scriptures)
        {
            while (!scripture.IsFullyHidden())
            {
                scripture.Display();

                Console.WriteLine();

                string input = Console.ReadLine();

                if (input.ToLower() == "quit")
                    return;


                scripture.HideRandomWord();
                Console.Clear();
            }
        }

        Console.WriteLine("Hi guess, we note that all words have been hidden; the program is now exit.");
    }
}

class Scripture
{
    private readonly string _reference;
    private readonly string _text;
    private List<Word> _words;
    private Random _random;

    public Scripture(string reference, string text)
    {
        _reference = reference;
        _text = text;
        _words = _text.Split(' ').Select(word => new Word(word)).ToList();
        _random = new Random();
    }

    public void Display()
    {
        Console.WriteLine(_reference);
        Console.WriteLine();

        foreach (Word word in _words)
        {
            Console.Write(word.IsHidden ? "_ _ _ _" : word.Value + " ");
        }

        Console.WriteLine();
    }

    public void HideRandomWord()
    {
        List<Word> visibleWords = _words.Where(word => !word.IsHidden).ToList();

        if (visibleWords.Count > 0)
        {
            int randomIndex = _random.Next(visibleWords.Count);
            visibleWords[randomIndex].Hide();
        }
    }

    public bool IsFullyHidden()
    {
        return _words.All(word => word.IsHidden);
    }
}

class Word
{
    public string Value { get; }
    public bool IsHidden { get; private set; }

    public Word(string value)
    {
        Value = value;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }
}
