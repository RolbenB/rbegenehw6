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
