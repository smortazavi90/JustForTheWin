using JustForTheWin.Enums;

namespace JustForTheWin.Models;

public class GameEvent
{
    public GameEvent(uint round, BallType ballType)
    {
        Round = round;
        BallType = ballType;
    }

    public uint Round { get; }
    public BallType BallType { get; }
}
