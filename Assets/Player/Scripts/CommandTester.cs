using UnityEngine;
using FightingGame.Inputs;

/// <summary>
/// コマンド判定（波動拳など）をテストするためのデバッグ用コンポーネント。
/// 1つのコマンドSO（波動拳）を参照するだけで、押されたボタン（弱・中・強・EX）を自動判別してログ出力します。
/// </summary>
public class CommandTester : MonoBehaviour
{
    [Header("Command Asset (これ1つだけで弱中強すべて判定！)")]
    [SerializeField] private CommandDefinitionSO _hadoukenCommand;

    [Header("Input Reader")]
    [SerializeField] private FighterInputReader _inputReader;

    private CommandDetector _commandDetector;

    private void Awake()
    {
        _commandDetector = new CommandDetector();
    }

    private void LateUpdate()
    {
        if (_inputReader == null || _hadoukenCommand == null) return;

        // 1つのコマンド定義を渡すだけで、成立と同時に「押されたボタン（usedButton）」が手に入る！
        if (_commandDetector.CheckCommand(_inputReader.Buffer, _hadoukenCommand, out InputButton usedButton))
        {
            // 2ボタン同時押し（EX技）の判定
            if ((usedButton & (InputButton.LightPunch | InputButton.MediumPunch)) == (InputButton.LightPunch | InputButton.MediumPunch) ||
                (usedButton & (InputButton.MediumPunch | InputButton.HeavyPunch)) == (InputButton.MediumPunch | InputButton.HeavyPunch) ||
                (usedButton & (InputButton.LightPunch | InputButton.HeavyPunch)) == (InputButton.LightPunch | InputButton.HeavyPunch))
            {
                Debug.Log("⚡ 【EX波動拳】が成立しました！（同時押し）");
            }
            // 単発ボタンの判定
            else if ((usedButton & InputButton.HeavyPunch) != 0)
            {
                Debug.Log("【強波動拳】が成立しました！（強P）");
            }
            else if ((usedButton & InputButton.MediumPunch) != 0)
            {
                Debug.Log("【中波動拳】が成立しました！（中P）");
            }
            else if ((usedButton & InputButton.LightPunch) != 0)
            {
                Debug.Log("【弱波動拳】が成立しました！（弱P）");
            }
        }
    }
}