using System;

namespace FightingGame.Inputs
{
    /// <summary>
    /// 入力ボタンを表すフラグ列挙型
    /// </summary>
    [Flags]
    public enum InputButton : ushort
    {
        None = 0,
        LightPunch = 1 << 0,
        MediumPunch = 1 << 1,
        HeavyPunch = 1 << 2,
        LightKick = 1 << 3,
        MediumKick = 1 << 4,
        HeavyKick = 1 << 5,
        Special = 1 << 6,
    }
}
