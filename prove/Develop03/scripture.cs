using System;
using System.Collections.Generic;
using System.Linq;

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
