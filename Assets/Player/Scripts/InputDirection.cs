namespace FightingGame.Inputs
{
    /// <summary>
    /// 画面基準の絶対的な入力方向を表す列挙体（テンキー記法に対応）。
    /// </summary>
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

    /// <summary>
    /// キャラクターの向きを考慮した相対的な入力方向を表す列挙体。
    /// 前方が Forward (6)、後方が Back (4) に対応し、コマンド判定に使用されます。
    /// </summary>
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