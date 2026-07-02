using UnityEngine;
using FightingGame.Character;

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
            int horizontal = input.x switch
            {
                >= 0.4f => 1,
                <= -0.4f => -1,
                _ => -0,
            };

            int vertical = input.y switch
            {
                >= 0.4f => 1,
                <= -0.4f => -1,
                _ => -0,
            };

            return (horizontal, vertical) switch
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

        /// <summary>
        /// 画面基準の絶対方向（AbsoluteDirection）から、キャラの向きを考慮した相対方向（RelativeDirection）に変換する
        /// </summary>
        public static RelativeDirection ToRelative(AbsoluteDirection direction, FacingDirection facing)
        {
            if (facing == FacingDirection.Right)
            {
                return (RelativeDirection)direction;
            }

            return direction switch
            {
                AbsoluteDirection.DownLeft  => RelativeDirection.DownForward,
                AbsoluteDirection.Down      => RelativeDirection.Down,
                AbsoluteDirection.DownRight => RelativeDirection.DownBack,

                AbsoluteDirection.Left      => RelativeDirection.Forward,
                AbsoluteDirection.Neutral   => RelativeDirection.Neutral,
                AbsoluteDirection.Right     => RelativeDirection.Back,

                AbsoluteDirection.UpLeft    => RelativeDirection.UpForward,
                AbsoluteDirection.Up        => RelativeDirection.Up,
                AbsoluteDirection.UpRight   => RelativeDirection.UpBack,

                _ => RelativeDirection.Neutral
            };
        }
    }
}