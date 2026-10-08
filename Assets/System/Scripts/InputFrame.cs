using UnityEngine;

namespace FightingGame.Inputs
{
    /// <summary>
    /// 1フレーム分の入力状態を保持する読み取り専用の構造体。
    /// 方向入力（生データ、絶対、相対）と各ボタンの状態（ホールド、プレス、リリース）を保持します。
    /// </summary>
    public readonly struct InputFrame
    {
        /// <summary>ゲーム起動またはラウンド開始からの経過フレーム数</summary>
        public readonly int Frame;

        /// <summary>コントローラーまたはキーボードから入力された生の2Dベクトル</summary>
        public readonly Vector2 RawDirection;

        /// <summary>画面基準（1P/2Pに関わらず固定）の絶対的な入力方向</summary>
        public readonly AbsoluteDirection AbsoluteDirection;

        /// <summary>キャラクターの向きを考慮した相対的な入力方向（格闘ゲームのコマンド判定に使用）</summary>
        public readonly RelativeDirection RelativeDirection;

        /// <summary>現在長押しされている（押しっぱなしの）ボタンのフラグ</summary>
        public readonly InputButton HeldButtons;

        /// <summary>このフレームで新しく押された瞬間のボタンのフラグ</summary>
        public readonly InputButton PressedButtons;

        /// <summary>このフレームで離された瞬間のボタンのフラグ</summary>
        public readonly InputButton ReleasedButtons;

        /// <summary>
        /// 新しい入力フレームのインスタンスを初期化します。
        /// </summary>
        /// <param name="frame">経過フレーム数</param>
        /// <param name="rawDirection">生の入力ベクトル</param>
        /// <param name="absoluteDirection">画面基準 of 絶対方向</param>
        /// <param name="relativeDirection">キャラ基準 of 相対方向</param>
        /// <param name="heldButtons">押しっぱなしのボタン</param>
        /// <param name="pressedButtons">押された瞬間のボタン</param>
        /// <param name="releasedButtons">離された瞬間のボタン</param>
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