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
        Light = 1 << 0,
        Medium = 1 << 1,
        Heavy = 1 << 2,
        Special = 1 << 3,
    }
}
