using FightingGame.Inputs;
using FightingGame.Character;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Unity Input Systemから生の入力を受け取り、
/// 絶対方向・相対方向・ボタン状態を解析して毎フレーム入力バッファに記録するクラス。
/// </summary>
public class FighterInputReader : MonoBehaviour
{
    /// <summary>
    /// 現在蓄積されている入力フレームの履歴バッファを取得します。
    /// </summary>
    public InputBuffer Buffer => _inputBuffer;
    
    [Header("Movement Action")]
    [SerializeField] private InputActionReference _moveAction;

    [Header("Punch Actions")]
    [SerializeField] private InputActionReference _lightPunchAction;
    [SerializeField] private InputActionReference _mediumPunchAction;
    [SerializeField] private InputActionReference _heavyPunchAction;

    [Header("Kick Actions")]
    [SerializeField] private InputActionReference _lightKickAction;
    [SerializeField] private InputActionReference _mediumKickAction;
    [SerializeField] private InputActionReference _heavyKickAction;

    [Header("System Actions")]
    [SerializeField] private InputActionReference _specialAttackAction;

    [Header("Character Facing")]
    [SerializeField] private FighterFacing _facing;

    private InputBuffer _inputBuffer;

    private int _currentFrame = 0;

    private void Awake()
    {
        // 入力バッファを初期化（デフォルトの120フレーム＝2秒分）
        _inputBuffer = new InputBuffer();
    }

    private void OnEnable()
    {
        // 各アクションの入力を有効化
        if (_moveAction != null) _moveAction.action.Enable();

        // 弱・中・強Pの入力判定
        if (_lightPunchAction != null) _lightPunchAction.action.Enable();
        if (_mediumPunchAction != null) _mediumPunchAction.action.Enable();
        if (_heavyPunchAction != null) _heavyPunchAction.action.Enable();

        //　弱・中・強Kの入力判定
        if (_lightKickAction != null) _lightKickAction.action.Enable();
        if (_mediumKickAction != null) _mediumKickAction.action.Enable();
        if (_heavyKickAction != null) _heavyKickAction.action.Enable();

        if (_specialAttackAction != null) _specialAttackAction.action.Enable();
    }

    private void OnDisable()
    {
        // 各アクションの入力を無効化
        if (_moveAction != null) _moveAction.action.Disable();

        if (_lightPunchAction != null) _lightPunchAction.action.Disable();
        if (_mediumPunchAction != null) _mediumPunchAction.action.Disable();
        if (_heavyPunchAction != null) _heavyPunchAction.action.Disable();

        if (_lightKickAction != null) _lightKickAction.action.Disable();
        if (_mediumKickAction != null) _mediumKickAction.action.Disable();
        if (_heavyKickAction != null) _heavyKickAction.action.Disable();

        if (_specialAttackAction != null) _specialAttackAction.action.Disable();
    }

    private void Update()
    {
        // 1. 方向入力(Vector2)の読み取り
        Vector2 rawInput = _moveAction != null ? _moveAction.action.ReadValue<Vector2>() : Vector2.zero;

        // 2. 画面基準の絶対方向（AbsoluteDirection）に変換
        AbsoluteDirection absDir = DirectionConverter.ToAbsolute(rawInput);

        // 3. キャラ基準の相対方向（RelativeDirection）に変換
        FacingDirection currentFacing = _facing != null ? _facing.Current : FacingDirection.Right;
        RelativeDirection relDir = DirectionConverter.ToRelative(absDir, currentFacing);

        // 4. ボタン入力の初期化
        InputButton held = InputButton.None;
        InputButton pressed = InputButton.None;
        InputButton released = InputButton.None;

        // 5. パンチボタン入力判定 (|= で同時押しに対応)
        // 弱パンチ (LightPunch)
        if (_lightPunchAction != null)
        {
            var action = _lightPunchAction.action;
            if (action.IsPressed()) held |= InputButton.LightPunch;
            if (action.WasPressedThisFrame()) pressed |= InputButton.LightPunch;
            if (action.WasReleasedThisFrame()) released |= InputButton.LightPunch;
        }

        // 中パンチ (MediumPunch)
        if (_mediumPunchAction != null)
        {
            var action = _mediumPunchAction.action;
            if (action.IsPressed()) held |= InputButton.MediumPunch;
            if (action.WasPressedThisFrame()) pressed |= InputButton.MediumPunch;
            if (action.WasReleasedThisFrame()) released |= InputButton.MediumPunch;
        }

        // 強パンチ (HeavyPunch)
        if (_heavyPunchAction != null)
        {
            var action = _heavyPunchAction.action;
            if (action.IsPressed()) held |= InputButton.HeavyPunch;
            if (action.WasPressedThisFrame()) pressed |= InputButton.HeavyPunch;
            if (action.WasReleasedThisFrame()) released |= InputButton.HeavyPunch;
        }

        // 6. キックボタン入力判定
        // 弱キック (LightKick)
        if (_lightKickAction != null)
        {
            var action = _lightKickAction.action;
            if (action.IsPressed()) held |= InputButton.LightKick;
            if (action.WasPressedThisFrame()) pressed |= InputButton.LightKick;
            if (action.WasReleasedThisFrame()) released |= InputButton.LightKick;
        }

        // 中キック (MediumKick)
        if (_mediumKickAction != null)
        {
            var action = _mediumKickAction.action;
            if (action.IsPressed()) held |= InputButton.MediumKick;
            if (action.WasPressedThisFrame()) pressed |= InputButton.MediumKick;
            if (action.WasReleasedThisFrame()) released |= InputButton.MediumKick;
        }

        // 強キック (HeavyKick)
        if (_heavyKickAction != null)
        {
            var action = _heavyKickAction.action;
            if (action.IsPressed()) held |= InputButton.HeavyKick;
            if (action.WasPressedThisFrame()) pressed |= InputButton.HeavyKick;
            if (action.WasReleasedThisFrame()) released |= InputButton.HeavyKick;
        }

        // 7. システム・スペシャルボタン入力判定
        if (_specialAttackAction != null)
        {
            var action = _specialAttackAction.action;
            if (action.IsPressed()) held |= InputButton.Special;
            if (action.WasPressedThisFrame()) pressed |= InputButton.Special;
            if (action.WasReleasedThisFrame()) released |= InputButton.Special;
        }

        // 8. 入力フレーム（InputFrame）を作成
        InputFrame frame = new InputFrame(
            _currentFrame,
            rawInput,
            absDir,
            relDir,
            held,
            pressed,
            released
        );

        // 9. バッファにフレームを追加
        _inputBuffer.Add(frame);

        _currentFrame++;

        // デバッグログ出力 (※テスト時はコメント解除して使用)
        // Debug.Log($"Frame: {frame.Frame} | Abs: {frame.AbsoluteDirection} | Rel: {frame.RelativeDirection} | Held: {frame.HeldButtons} | Pressed: {frame.PressedButtons}");
    }
}