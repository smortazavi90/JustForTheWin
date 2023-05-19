using JustForTheWin.Enums;

namespace JustForTheWin.Models;

public class Game
{
    private readonly Player _player;
    private readonly Random _random;
    private readonly List<GameEvent> _events;
    private uint _roundsPlayed;

    public Game(Player player)
    {
        _player = player;
        _random = new Random();
        _events = new List<GameEvent>();
        _roundsPlayed = 0;
    }

    /// <summary>
    /// Controls the entire game flow
    /// </summary>
    public void Play()
    {
        Console.WriteLine("Welcome to the game!");
        Console.WriteLine($"Each round costs {GameConfiguration.PickCost} credits.");

        var totalRounds = SetTotalRounds();

        _events.AddRange(PlayRounds(totalRounds));

        var rtp = GetRtp(_events, _roundsPlayed);

        Console.WriteLine("Game is finished.");

        _player.Credit.Print();

        Console.WriteLine("Thank you for playing. Press any key to exit.");
        Console.ReadKey();
    }

    /// <summary>
    /// Gets the number of all rounds from the player
    /// </summary>
    private static int SetTotalRounds()
    {
        Console.WriteLine("How many rounds do you want to play?");

        var rounds = 0;
        while (rounds <= 0)
        {
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input) || int.TryParse(input, out rounds) is false || rounds <= 0)
            {
                Console.WriteLine("Please enter an integer: ");
            }
        }

        Console.WriteLine($"Let's play {rounds} rounds.{Environment.NewLine}");

        return rounds;
    }

    /// <summary>
    /// Runs all the rounds
    /// </summary>
    /// <param name="totalRounds"></param>
    private IEnumerable<GameEvent> PlayRounds(int totalRounds)
    {
        List<GameEvent> events = new();
        for (var i = 0; i < totalRounds; i++)
        {
            Console.WriteLine($"Round {_roundsPlayed + 1}: ");

            events.AddRange(PlaySingleRound());

            _roundsPlayed++;

            Console.WriteLine($"Round {_roundsPlayed} completed.{Environment.NewLine}");
        }

        return events;
    }

    /// <summary>
    /// Runs one single round
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private IEnumerable<GameEvent> PlaySingleRound()
    {
        List<GameEvent> events = new();
        var isExtraPick = false;
        while (true)
        {
            GetPickOrder(isExtraPick);

            var ballType = PickBall(_random);

            UpdatePlayerBalance(_player, ballType);

            events.Add(new GameEvent(_roundsPlayed, ballType));

            if (ballType is not BallType.ExtraPick)
            {
                break;
            }

            isExtraPick = true;
        }

        return events;
    }

    /// <summary>
    /// Updates player balance based on the picked ball
    /// </summary>
    /// <param name="player"></param>
    /// <param name="ballType"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static void UpdatePlayerBalance(Player player, BallType ballType)
    {
        switch (ballType)
        {
            case BallType.Win:
                player.Credit.Decrease(GameConfiguration.PickCost).Increase(GameConfiguration.WinCredits);
                Console.WriteLine($"You won {GameConfiguration.WinCredits} credits!");
                player.Credit.Print();
                break;
            case BallType.ExtraPick:
                Console.WriteLine("Extra pick! You can pick another ball for free.");
                break;
            case BallType.NoWin:
                player.Credit.Decrease(GameConfiguration.PickCost);
                Console.WriteLine("No win.");
                player.Credit.Print();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void GetPickOrder(bool isExtraPick)
    {
        Console.WriteLine(
            $"Pick a ball {(isExtraPick ? "for free" : $"by paying {GameConfiguration.PickCost} credits")} (Enter 'p'):");

        var input = Console.ReadLine()?.ToLower();

        while (input is not "p")
        {
            Console.WriteLine("Invalid input. Please enter 'p' to pick a ball.");
            input = Console.ReadLine()?.ToLower();
        }
    }

    /// <summary>
    /// Picks up a random ball from the basket
    /// </summary>
    /// <returns></returns>
    public static BallType PickBall(Random random)
    {
        var randomValue = random.Next(1, GameConfiguration.WinBallCount + GameConfiguration.ExtraPickBallCount + GameConfiguration.NoWinBallCount);

        return randomValue switch
        {
            <= GameConfiguration.WinBallCount => BallType.Win,
            <= GameConfiguration.WinBallCount + GameConfiguration.ExtraPickBallCount => BallType.ExtraPick,
            _ => BallType.NoWin
        };
    }

    /// <summary>
    /// Calculate return to player
    /// </summary>
    /// <returns></returns>
    private static double GetRtp(IEnumerable<GameEvent> events, uint roundsPlayed)
    {
        var wonCredits = events.Count(e => e.BallType is BallType.Win) * GameConfiguration.WinCredits;
        var allRoundsCost = roundsPlayed * GameConfiguration.PickCost;
        var rtp = (double)wonCredits / allRoundsCost * 100;
        Console.WriteLine($"RTP (Return to Player): {rtp:0.00}%{Environment.NewLine}");
        return rtp;
    }
    
    /// <summary>
    /// Returns number of played rounds
    /// </summary>
    /// <returns></returns>
    public uint GetTotalPlayedRounds()
    {
        return _roundsPlayed;
    }
}