using FightingGame.Inputs;
using FightingGame.Character;
using UnityEngine;
using UnityEngine.InputSystem;

public class FighterInputReader : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveAction;

    [SerializeField] private InputActionReference _lightAttackAction;
    [SerializeField] private InputActionReference _mediumAttackAction;
    [SerializeField] private InputActionReference _heavyAttackAction;
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

        _lightAttackAction.action.Enable();
        _mediumAttackAction.action.Enable();
        _heavyAttackAction.action.Enable();
        _specialAttackAction.action.Enable();
    }

    private void OnDisable()
    {
        _moveAction.action.Disable();

        _lightAttackAction.action.Disable();
        _mediumAttackAction.action.Disable();
        _heavyAttackAction.action.Disable();
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

        // 弱攻撃判定(lightAttack)
        if (_lightAttackAction.action.IsPressed()) held |= InputButton.Light;
        if(_lightAttackAction.action.WasPressedThisFrame()) pressed |= InputButton.Light;
        if(_lightAttackAction.action.WasReleasedThisFrame()) released |= InputButton.Light;

        // 中攻撃判定(mediumAttack)
        if (_mediumAttackAction.action.IsPressed()) held |= InputButton.Medium;
        if (_mediumAttackAction.action.WasPressedThisFrame()) pressed |= InputButton.Medium;
        if(_mediumAttackAction.action.WasReleasedThisFrame()) released |= InputButton.Medium;

        // 強攻撃判定(heavyAttack)
        if (_heavyAttackAction.action.IsPressed()) held |= InputButton.Heavy;
        if (_heavyAttackAction.action.WasPressedThisFrame()) pressed |= InputButton.Heavy;
        if (_heavyAttackAction.action.WasReleasedThisFrame()) released |= InputButton.Heavy;

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
