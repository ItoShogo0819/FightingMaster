using UnityEngine;

namespace FightingGame.Inputs
{

    ///<summary>
    ///入力されたVector2をAbsoluteDirectionに変換するクラス
    ///</summary>
    public static class DirectionConverter
    {
        /// <summary>
        /// 入力値から方向を判定してAbsoluteDirectionを返す
        /// </summary>
        public static AbsoluteDirection ToAbsolute(Vector2 input, float threshold = 0.4f)
        {
            int holizontal = input.x switch
            {
                >= 0.4f => 1,
                <= -0.4f => -1,
                _ => -0,
            };

            int vertical = input.y switch
            {
                >= 0.4f => 1,
                <= -0.4f => 1,
                _ => -0,
            };

            return (holizontal, vertical) switch
            {
                (-1,-1) => AbsoluteDirection.DownLeft,
                (0,-1) => AbsoluteDirection.Down,
                (1,-1) => AbsoluteDirection.DownRight,

                (-1,0) => AbsoluteDirection.Left,
                (0,0) => AbsoluteDirection.Neutral,
                (1,0) => AbsoluteDirection.Right,

                (-1,1) => AbsoluteDirection.UpLeft,
                (0,1) => AbsoluteDirection.Up,
                (1,1) => AbsoluteDirection.UpRight,

                _ => AbsoluteDirection.Neutral
            };
        }
    }
}