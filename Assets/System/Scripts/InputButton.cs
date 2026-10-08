using System;

namespace FightingGame.Inputs
{
    /// <summary>
    /// プレイヤーの入力ボタンを表すビットフラグ列挙体。
    /// 同時押し（EX技や投げなど）を判定するために Flags 属性を指定しています。
    /// </summary>
    [Flags]
    public enum InputButton : ushort
    {
        /// <summary>無入力</summary>
        None        = 0,
        /// <summary>弱パンチ (LP)</summary>
        LightPunch  = 1 << 0,
        /// <summary>中パンチ (MP)</summary>
        MediumPunch = 1 << 1,
        /// <summary>強パンチ (HP)</summary>
        HeavyPunch  = 1 << 2,
        /// <summary>弱キック (LK)</summary>
        LightKick   = 1 << 3,
        /// <summary>中キック (MK)</summary>
        MediumKick  = 1 << 4,
        /// <summary>強キック (HK)</summary>
        HeavyKick   = 1 << 5,
        /// <summary>スペシャルボタン (システムボタン/ワンボタン必殺技用)</summary>
        Special     = 1 << 6,
    }
    
    public static class InputButtonExtensions
    {
        /// <summary>
        /// 押されているボタンの個数を取得
        /// </summary>
        public static int GetPressedCount(this InputButton btn)
        {
            int count = 0;
            ushort val = (ushort)btn;
            while (val > 0)
            {
                count += val & 1;
                val >>= 1;
            }
            return count;
        }
    }
}