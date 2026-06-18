namespace FightingGame.Inputs
{
    public enum AbsoluteDirection : byte
    {
        DownLeft = 1,
        Down = 2,
        DownRight = 3,
        Left = 4,
        Neutral = 5,
        Right = 6,
        UpLeft = 7,
        Up = 8,
        UpRight = 9
    }

    public enum RelativeDirection : byte
    {
        DownBack = 1,
        Down = 2,
        DownForward = 3,
        Back = 4,
        Neutral = 5,
        Forward = 6,
        UpBack = 7,
        Up = 8,
        UpForward = 9
    }
}