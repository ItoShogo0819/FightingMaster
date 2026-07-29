using FightingGame.Inputs;
using FightingGame.Character;
using UnityEngine;
using UnityEngine.InputSystem;

public class FighterInputReader : MonoBehaviour
{
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

    [SerializeField] private FighterFacing _facing;

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

        _lightPunchAction.action.Enable();
        _mediumPunchAction.action.Enable();
        _heavyPunchAction.action.Enable();

        _lightKickAction.action.Enable();
        _mediumKickAction.action.Enable();
        _heavyKickAction.action.Enable();

        _specialAttackAction.action.Enable();
    }

    private void OnDisable()
    {
        _moveAction.action.Disable();

        _lightPunchAction.action.Disable();
        _mediumPunchAction.action.Disable();
        _heavyPunchAction.action.Disable();

        _lightKickAction.action.Disable();
        _mediumKickAction.action.Disable();
        _heavyKickAction.action.Disable();

        _specialAttackAction.action.Disable();
    }

    private void Update()
    {
        // 方向入力(Vector2)の読み取り
        Vector2 rawInput = _moveAction.action.ReadValue<Vector2>();

        // 画面基準の絶対方向（AbsoluteDirection）に変換
        AbsoluteDirection absDir = DirectionConverter.ToAbsolute(rawInput);

        // キャラ基準の相対方向（RelativeDirection）に変換
        FacingDirection currentFacing = _facing != null ? _facing.Current : FacingDirection.Right;
        RelativeDirection relDir = DirectionConverter.ToRelative(absDir, currentFacing);

        // ボタン入力の取得（※現時点ではボタン用のアクションが未設定のためNoneで初期化）
        InputButton held = InputButton.None;
        InputButton pressed = InputButton.None;
        InputButton released = InputButton.None;

        // TODO: 攻撃ボタンなどのInputActionがバインドされたら、ここでフラグを判定して代入する

        // 弱P判定
        if (_lightPunchAction.action.IsPressed()) held |= InputButton.LightPunch;
        if (_lightPunchAction.action.WasPressedThisFrame()) pressed |= InputButton.LightPunch;
        if (_lightPunchAction.action.WasReleasedThisFrame()) released |= InputButton.LightPunch;

        // 弱K判定
        if (_lightKickAction.action.IsPressed()) held |= InputButton.LightKick;
        if (_lightKickAction.action.WasPressedThisFrame()) pressed |= InputButton.LightKick;
        if (_lightKickAction.action.WasReleasedThisFrame()) released |= InputButton.LightKick;

        // 中P判定
        if (_mediumPunchAction.action.IsPressed()) held |= InputButton.MediumPunch;
        if (_mediumPunchAction.action.WasPressedThisFrame()) pressed |= InputButton.MediumPunch;
        if (_mediumPunchAction.action.WasReleasedThisFrame()) released |= InputButton.MediumPunch;

        // 中K判定
        if (_mediumKickAction.action.IsPressed()) held |= InputButton.MediumKick;
        if (_mediumKickAction.action.WasPressedThisFrame()) pressed = InputButton.MediumKick;
        if (_mediumKickAction.action.WasReleasedThisFrame()) released = InputButton.MediumKick;

        // 強P判定
        if (_heavyPunchAction.action.IsPressed()) held |= InputButton.HeavyPunch;
        if (_heavyPunchAction.action.WasPressedThisFrame()) pressed |= InputButton.HeavyPunch;
        if (_heavyPunchAction.action.WasReleasedThisFrame()) released |= InputButton.HeavyPunch;

        // 強K判定
        if (_heavyPunchAction.action.IsPressed()) held |= InputButton.HeavyKick;
        if (_heavyPunchAction.action.WasPressedThisFrame()) pressed |= InputButton.HeavyKick;
        if (_heavyPunchAction.action.WasReleasedThisFrame()) released |= InputButton.HeavyKick;

        // SP攻撃判定(specialAttack)
        if (_specialAttackAction.action.IsPressed()) held |= InputButton.Special;
        if(_specialAttackAction.action.WasPressedThisFrame()) pressed |= InputButton.Special;
        if(_specialAttackAction.action.WasReleasedThisFrame()) released |= InputButton.Special;

        // 入力フレーム（InputFrame）を作成
        InputFrame frame = new InputFrame(
            _currentFrame,
            rawInput,
            absDir,
            relDir,
            held,
            pressed,
            released
        );

        // バッファにフレームを追加
        _inputBuffer.Add(frame);

        _currentFrame++;

        // デバッグログを出力（絶対方向と相対方向、現在のキャラクターの向き）
        Debug.Log($"Frame: {frame.Frame} | Abs: {frame.AbsoluteDirection} | Rel: {frame.RelativeDirection} (Facing: {currentFacing})");
    }
}
