using UnityEngine;
using System.Collections.Generic;
using FightingGame.Inputs;

/// <summary>
/// 入力バッファ（InputBuffer）の履歴を走査し、
/// 特定のコマンド（正規コマンドおよび簡易入力）が成立しているかを判定するクラス。
/// MonoBehaviour を継承しない純粋な C# クラスとして実装されています。
/// </summary>
public class CommandDetector
{
    /// <summary>
    /// 指定された入力バッファから、コマンドが成立しているかを判定します。
    /// 成立した場合、実際に押された攻撃ボタンを out 引数で返します（弱・中・強の判別用）。
    /// </summary>
    /// <param name="buffer">入力履歴バッファ</param>
    /// <param name="command">判定対象のコマンド定義アセット</param>
    /// <param name="usedButton">成立時に押されていたボタン（弱P/中P/強P/EX等の判別用）</param>
    /// <returns>コマンドが成立した場合は true</returns>
    public bool CheckCommand(InputBuffer buffer, CommandDefinitionSO command, out InputButton usedButton)
    {
        usedButton = InputButton.None;
        if (buffer.Count == 0) return false;

        // 最新フレームで、コマンドが要求するいずれかのボタンが「新しく押された瞬間」かをチェック
        buffer.TryGetRecent(0, out InputFrame latestFrame);
        InputButton matchedButtons = latestFrame.PressedButtons & command.requiredButtons;
        if (matchedButtons == InputButton.None) return false;

        // まず正規コマンド（例：236）をチェック
        if (CheckSequence(buffer, command.inputSequence, command.inputWindow))
        {
            usedButton = matchedButtons;
            return true;
        }

        // 正規でダメなら、簡易入力（例：26 など）をチェック
        if (command.allowedShortCuts != null && command.allowedShortCuts.Length > 0)
        {
            if (CheckSequence(buffer, command.allowedShortCuts, command.inputWindow))
            {
                usedButton = matchedButtons;
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 過去互換性用のオーバーロード（ボタン詳細を受け取らない場合）
    /// </summary>
    public bool CheckCommand(InputBuffer buffer, CommandDefinitionSO command)
    {
        return CheckCommand(buffer, command, out _);
    }
    
    /// <summary>
    /// 複数コマンドの中から、成立かつ優先度の最も高いコマンドを検出
    /// </summary>
    public CommandDefinitionSO EvaluateBestCommand(InputBuffer buffer, IReadOnlyList<CommandDefinitionSO> commands,
        out InputButton usedButton)
    {
        CommandDefinitionSO bestCommand = null;
        int highestPriority = int.MinValue;
        usedButton = InputButton.None;

        foreach (var cmd in commands)
        {
            if(cmd == null) continue;
            
            if (CheckCommand(buffer, cmd, out InputButton btn))
            {
                if (cmd.priority > highestPriority)
                {
                    highestPriority = cmd.priority;
                    bestCommand = cmd;
                    usedButton = btn;
                }
            }
        }

        return bestCommand;
    }

    /// <summary>
    /// 指定された方向シーケンスがバッファ内で成立しているかを逆順走査する共通ヘルパー
    /// </summary>
    private bool CheckSequence(InputBuffer buffer, RelativeDirection[] sequence, int inputWindow)
    {
        if (sequence == null || sequence.Length == 0) return true;

        int sequenceIndex = sequence.Length - 1;
        int maxSearchFrames = Mathf.Min(buffer.Count, inputWindow);

        RelativeDirection lastDir = RelativeDirection.Neutral;
        bool hasLastDir = false;

        for (int i = 0; i < maxSearchFrames; i++)
        {
            if (!buffer.TryGetRecent(i, out InputFrame checkFrame)) break;

            RelativeDirection currentDir = checkFrame.RelativeDirection;

            if (!hasLastDir)
            {
                lastDir = currentDir;
                hasLastDir = true;

                if (currentDir == sequence[sequenceIndex])
                {
                    sequenceIndex--;
                    if (sequenceIndex < 0) return true;
                }
                continue;
            }

            // 変化検出（同じ方向が続いている間はスキップ）
            if (currentDir == lastDir) continue;

            if (currentDir == sequence[sequenceIndex])
            {
                sequenceIndex--;
                if (sequenceIndex < 0) return true;
            }

            lastDir = currentDir;
        }

        return false;
    }
}