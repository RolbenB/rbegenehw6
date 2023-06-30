using System;

class Entry
{
    public string _prompt; // Represents the prompt/question for the entry
    public string _response; // Represents the user's response to the prompt
    public string _date; // Represents the date of the entry

    public void DisplayEntryInfo()
    {
        Console.WriteLine($"Date: {_date}");
        Console.WriteLine($"Prompt: {_prompt}");
        Console.WriteLine($"Response: {_response}\n");
    }
}
