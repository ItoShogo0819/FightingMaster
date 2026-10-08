using System.Collections.Generic;
using UnityEngine;
using FightingGame.Inputs;

/// <summary>
/// 必殺技などのコマンド入力データ（正規シーケンス、簡易入力、受付猶予、ボタンなど）を定義する ScriptableObject。
/// </summary>
[CreateAssetMenu(fileName = "CommandDefinition", menuName = "FightingGame/CommandDefinition")]
public class CommandDefinitionSO : ScriptableObject
{
    [Header("コマンド設定")]
    [Tooltip("コマンド名（例：Hadouken / 波動拳）")]
    public string CommandName;

    [Header("方向シーケンス")]
    [Tooltip("正規のコマンド順序（例：[Down, DownForward, Forward] ＝ 236）")]
    public RelativeDirection[] inputSequence;

    [Tooltip("許容する簡易入力・省略入力のパターン（例：[Down, Forward] ＝ 26）")]
    public RelativeDirection[] allowedShortCuts;

    [Header("受付時間・ボタン")]
    [Tooltip("最初の方向入力から完了するまでの猶予フレーム数（デフォルトは15フレーム）")]
    public int inputWindow = 15;

    [Tooltip("コマンド成立に必要な攻撃ボタン（弱・中・強をまとめて指定可能）")]
    public InputButton requiredButtons;

    [Tooltip("技の発動に必要な最低同時押しボタン数（通常技は1、EX技は2）")]
    public int minPressedButtons = 1;

    public int priority;
}