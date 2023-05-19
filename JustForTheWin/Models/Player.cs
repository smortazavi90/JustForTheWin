namespace JustForTheWin.Models;

public class Player
{
    public Credit Credit { get; }

    public Player()
    {
        Credit = new Credit();
    }
}