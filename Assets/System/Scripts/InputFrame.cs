using UnityEngine;

namespace FightingGame.Inputs
{
    /// <summary>
    /// 入力フレームを表す構造体
    /// </summary>
    public readonly struct InputFrame
    {
        public readonly int Frame;
        public readonly Vector2 RawDirection;
        public readonly AbsoluteDirection AbsoluteDirection;
        public readonly RelativeDirection RelativeDirection;
        public readonly InputButton HeldButtons;
        public readonly InputButton PressedButtons;
        public readonly InputButton ReleasedButtons;

        /// <summary>
        /// 入力フレームのコンストラクタ
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="rawDirection"></param>
        /// <param name="absoluteDirection"></param>
        /// <param name="relativeDirection"></param>
        /// <param name="heldButtons"></param>
        /// <param name="pressedButtons"></param>
        /// <param name="releasedButtons"></param>
        public InputFrame(
            int frame,
            Vector2 rawDirection,
            AbsoluteDirection absoluteDirection,
            RelativeDirection relativeDirection,
            InputButton heldButtons,
            InputButton pressedButtons,
            InputButton releasedButtons)
        {
            Frame = frame;
            RawDirection = rawDirection;
            AbsoluteDirection = absoluteDirection;
            RelativeDirection = relativeDirection;
            HeldButtons = heldButtons;
            PressedButtons = pressedButtons;
            ReleasedButtons = releasedButtons;
        }
    }
}
