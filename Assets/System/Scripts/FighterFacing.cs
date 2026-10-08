using UnityEngine;
using FightingGame.Character;

/// <summary>
/// キャラクターの現在の向き（左右）を管理し、
/// 対戦相手との位置関係に基づいて動的に向きを更新するコンポーネント。
/// </summary>
public class FighterFacing : MonoBehaviour
{
    [SerializeField] private Transform opponent;

    /// <summary>
    /// キャラクターの現在の向き（右向き / 左向き）を取得します。
    /// </summary>
    public FacingDirection Current { get; private set; } = FacingDirection.Right;

    /// <summary>
    /// 対戦相手との位置関係を元に、キャラクターの向きを更新します。
    /// </summary>
    /// <param name="canTurn">振り向きが許可されている状態か（技の最中などは false を指定）</param>
    public void UpdateFacing(bool canTurn)
    {
        if (!canTurn || opponent == null) return;

        // 対戦相手が右側にいれば右向き、左側にいれば左向きにする
        Current = opponent.position.x >= transform.position.x ? FacingDirection.Right : FacingDirection.Left;
    }
}