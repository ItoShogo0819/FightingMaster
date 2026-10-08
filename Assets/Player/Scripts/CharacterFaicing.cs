namespace FightingGame.Character
{
    /// <summary>
    /// キャラクターが向いている方向を定義する列挙体。
    /// </summary>
    public enum FacingDirection
    {
        // 左向き (1P基準で後ろ/2P基準で前)
        Left = -1,
        // 右向き (1P基準で前/2P基準で後ろ)
        Right = 1,
    }
}