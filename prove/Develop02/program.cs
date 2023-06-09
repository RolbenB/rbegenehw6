using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Journal journal = new Journal(); // Creates a new instance of the Journal class

        string[] prompts = {
            "What is the most interesting game played in your life?",
            "What is the name of your best friend?",
            "How did you see the blessing of the Lord in your life?",
            "Explain a short story of your life?",
            "What country would you like to visit next year?"
        };

        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("Journal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Please, choose a number from 1-5: ");

            string choice = Console.ReadLine(); // Reads the user's choice
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    Entry newEntry = new Entry(); // Creates a new instance of the Entry class
                    newEntry._prompt = prompts[journal.Entries.Count % prompts.Length]; // Selects a prompt based on the number of entries
                    Console.WriteLine($"Prompt: {newEntry._prompt}");
                    Console.Write("Response: ");
                    newEntry._response = Console.ReadLine(); // Reads the user's response
                    Console.Write("Date: ");
                    newEntry._date = Console.ReadLine(); // Reads the date of the entry
                    journal.AddEntry(newEntry); // Adds the new entry to the journal
                    Console.WriteLine("Entry added successfully.\n");
                    break;
                case "2":
                    if (journal.Entries.Count > 0)
                        journal.DisplayEntries(); // Displays the journal entries
                    else
                        Console.WriteLine("No entries found.\n");
                    break;
                case "3":
                    Console.Write("Enter filename to save the journal: ");
                    string saveFilename = Console.ReadLine(); // Reads the filename from the user
                    journal.SaveToFile(saveFilename); // Saves the journal to the specified file
                    Console.WriteLine();
                    break;
                case "4":
                    Console.Write("Enter filename to load the journal: ");
                    string loadFilename = Console.ReadLine(); // Reads the filename from the user
                    journal.LoadFromFile(loadFilename); // Loads the journal from the specified file
                    Console.WriteLine();
                    break;
                case "5":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.\n");
                    break;
            }
        }

        Console.WriteLine("Thank you, have a good day! Goodbye!");
    }
}
