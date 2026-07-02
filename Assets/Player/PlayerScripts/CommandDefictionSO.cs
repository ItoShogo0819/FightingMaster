using UnityEngine;
using FightingGame.Inputs;

[CreateAssetMenu(fileName = "CommandDefinition", menuName = "FightingGame/CommandDefinition")]
public class CommandDefinitionSO : ScriptableObject
{
    [Header("コマンド設定")]
    [Tooltip("コマンド名(例：Hadouken)")]
    public string CommandName;

    [Tooltip("コマンドの方向キー入力順。最新入力を配列の最後とする")]
    public RelativeDirection[] inputSequence;

    [Tooltip("コマンド完成までの猶予フレーム(基本的に7～12f)")]
    public int inputWindow = 12;

    [Tooltip("必要とする攻撃ボタン")]
    public InputButton requiredButtons;

    [Tooltip("優先度(数値が大きいほど優先度が高い)")]
    public int priority;
}
