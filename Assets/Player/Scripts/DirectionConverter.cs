using UnityEngine;
using FightingGame.Character;

namespace FightingGame.Inputs
{
    /// <summary>
    /// 入力デバイスの入力値（Vector2）を絶対方向（AbsoluteDirection）や、
    /// キャラクターの向きを考慮した相対方向（RelativeDirection）に変換するユーティリティクラス。
    /// </summary>
    public static class DirectionConverter
    {
        /// <summary>
        /// アナログスティックや方向キーの入力値（Vector2）を、画面基準の絶対的な入力方向（AbsoluteDirection）に変換します。
        /// </summary>
        /// <param name="input">生の入力ベクトル</param>
        /// <param name="threshold">入力を検知するデッドゾーンの閾値（デフォルトは 0.4f）</param>
        /// <returns>変換された絶対方向</returns>
        public static AbsoluteDirection ToAbsolute(Vector2 input, float threshold = 0.4f)
        {
            int horizontal = input.x switch
            {
                _ when input.x >= threshold => 1,
                _ when input.x <= -threshold => -1,
                _ => 0,
            };

            int vertical = input.y switch
            {
                _ when input.y >= threshold => 1,
                _ when input.y <= -threshold => -1,
                _ => 0,
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
        /// 画面基準の絶対方向（AbsoluteDirection）から、キャラクターの現在の向きを考慮した相対的な入力方向（RelativeDirection）に変換します。
        /// </summary>
        /// <param name="direction">画面基準の絶対方向</param>
        /// <param name="facing">キャラクターの向き（右向き / 左向き）</param>
        /// <returns>変換されたキャラクター基準の相対方向</returns>
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