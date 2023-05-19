namespace JustForTheWin.Models;

public class Credit
{
    private int Value { get; set; }

    public Credit()
    {
        Value = 0;
    }

    public Credit Decrease(int amount)
    {
        Value -= amount;
        return this;
    }

    public Credit Increase(int amount)
    {
        Value += amount;
        return this;
    }

    public void Print()
    {
        Console.WriteLine($"Current credit: {Value}");
    }
    
    public int GetValue()
    {
        return Value;
    }
}