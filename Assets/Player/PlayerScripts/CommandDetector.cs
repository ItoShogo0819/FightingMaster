using UnityEngine;
using System.Collections.Generic;
using FightingGame.Inputs;

public class CommandDetector
{
    /// <summary>
    /// バッファから特定のコマンドが成立しているかを判定する
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    public bool CheckCommand(InputBuffer buffer, CommandDefinitionSO command)
    {
        // バッファが空の場合はコマンド成立しない(必要フレームが溜まっているのか)
        if (buffer.Count == 0) return false;

        //　最新フレームでボタンが「押された瞬間」かをチェック
        buffer.TryGetRecent(0,out InputFrame latestFrame);
        if ((latestFrame.PressedButtons & command.requiredButtons) == InputButton.None) return false;

        //　コマンド定義が空の場合は常に成立とする
        if (command.inputSequence == null || command.inputSequence.Length == 0) return true;

        //　逆順マッチング用のインデックス(コマンドの最後の入力から探索)
        int sequenceIndex = command.inputSequence.Length - 1;

        // 探索する最大フレーム(受付ウィンドウ)
        int maxSearchFrames = Mathf.Min(buffer.Count, command.inputWindow);

        // 前回の方向(変化検出用)
        RelativeDirection lastDir = RelativeDirection.Neutral;
        bool hasLastDir = false;

        // 最新フレーム(インデックス0)から過去へ遡るループ
        for(int i = 0; i < maxSearchFrames; i++)
        {
            if (!buffer.TryGetRecent(i, out InputFrame checkFrame)) break;

            RelativeDirection currentDir = checkFrame.RelativeDirection;

            //　初回ループ時は比較対象を最新に設定
            if (!hasLastDir)
            {
                lastDir = currentDir;
                hasLastDir = true;

                // 最新フレームの入力がコマンドの最後の入力と一致するかを確認
                if (currentDir == command.inputSequence[sequenceIndex])
                {
                    sequenceIndex--;
                    if (sequenceIndex < 0) return true;
                }
                continue;
            }

            // [変化検出]1フレーム先と入力が同じであるならスルー(変化した瞬間だけ見る)
            if (currentDir == lastDir) continue;

            // 入力変化時、それが今探しているキー入力と一致するかを確認
            if (currentDir == command.inputSequence[sequenceIndex])
            {
                sequenceIndex--;
                if (sequenceIndex < 0) return true;
            }
            // [ニュートラルリセット(オプショナル)]
            // コマンドに関係のない方向入力が挟まった場合コマンド入力失敗の処理を入れることも可能

            lastDir = currentDir;
        }

        return false;   // コマンドが成立していない場合はfalseを返す
    }
}
