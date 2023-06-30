using System;
using System.Collections.Generic;
using System.IO;

class Journal
{
    public List<Entry> Entries { get; private set; } // List to store journal entries

    public Journal()
    {
        Entries = new List<Entry>(); // Initializes the list of entries in the constructor
    }

    public void AddEntry(Entry entry)
    {
        Entries.Add(entry); // Adds a new entry to the list
    }

    public void DisplayEntries()
    {
        foreach (var entry in Entries)
        {
            Console.WriteLine($"Date: {entry._date}");
            Console.WriteLine($"Prompt: {entry._prompt}");
            Console.WriteLine($"Response: {entry._response}\n");
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in Entries)
            {
                writer.WriteLine(entry._date); // Write the date to the file
                writer.WriteLine(entry._prompt); // Writes the prompt to the file
                writer.WriteLine(entry._response); // Writes the response to the file
                writer.WriteLine(); // Write an empty line to separate entries
            }
        }

        Console.WriteLine("Journal saved successfully.");
    }

    public void LoadFromFile(string filename)
    {
        Entries.Clear(); // Clears the existing entries before loading from the file

        using (StreamReader reader = new StreamReader(filename))
        {
            while (!reader.EndOfStream)
            {
                string date = reader.ReadLine(); // Reads the date from the file
                string prompt = reader.ReadLine(); // Reads the prompt from the file
                string response = reader.ReadLine(); // Reads the response from the file

                reader.ReadLine(); // Skip the empty line

                Entries.Add(new Entry
                {
                    _date = date,
                    _prompt = prompt,
                    _response = response
                });
            }
        }

        Console.WriteLine("Journal loaded successfully.");
    }
}
