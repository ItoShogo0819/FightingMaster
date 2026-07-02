using FightingGame.Inputs;
using FightingGame.Character;
using UnityEngine;
using UnityEngine.InputSystem;

public class FighterInputReader : MonoBehaviour
{
    [SerializeField]
    private InputActionReference _moveAction;

    [SerializeField]
    private FighterFacing _facing;

    private InputBuffer _inputBuffer;
    public InputBuffer Buffer => _inputBuffer;

    private int _currentFrame = 0;

    private void Awake()
    {
        _inputBuffer = new InputBuffer();
    }

    private void OnEnable()
    {
        _moveAction.action.Enable();
    }

    private void OnDisable()
    {
        _moveAction.action.Disable();
    }

    private void Update()
    {
        // 1. 方向入力(Vector2)の読み取り
        Vector2 rawInput = _moveAction.action.ReadValue<Vector2>();

        // 2. 画面基準の絶対方向（AbsoluteDirection）に変換
        AbsoluteDirection absDir = DirectionConverter.ToAbsolute(rawInput);

        // 3. キャラ基準の相対方向（RelativeDirection）に変換
        FacingDirection currentFacing = _facing != null ? _facing.Current : FacingDirection.Right;
        RelativeDirection relDir = DirectionConverter.ToRelative(absDir, currentFacing);

        // 4. ボタン入力の取得（※現時点ではボタン用のアクションが未設定のためNoneで初期化）
        InputButton held = InputButton.None;
        InputButton pressed = InputButton.None;
        InputButton released = InputButton.None;

        // TODO: 攻撃ボタンなどのInputActionがバインドされたら、ここでフラグを判定して代入する

        // 5. 入力フレーム（InputFrame）を作成
        InputFrame frame = new InputFrame(
            _currentFrame,
            rawInput,
            absDir,
            relDir,
            held,
            pressed,
            released
        );

        // 6. バッファにフレームを追加
        _inputBuffer.Add(frame);

        _currentFrame++;

        // デバッグログを出力（絶対方向と相対方向、現在のキャラクターの向き）
        Debug.Log($"Frame: {frame.Frame} | Abs: {frame.AbsoluteDirection} | Rel: {frame.RelativeDirection} (Facing: {currentFacing})");
    }
}
