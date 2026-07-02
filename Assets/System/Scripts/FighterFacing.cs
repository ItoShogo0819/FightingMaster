using UnityEngine;
using FightingGame.Character;

public class FighterFacing : MonoBehaviour
{
    public FacingDirection Current { get; private set; } = FacingDirection.Right;

    public void UpdateFacing(bool canTurn)
    {
        if (!canTurn) return;

        Current = opponent.position.x >= transform.position.x ? FacingDirection.Right : FacingDirection.Left;
    }

    [SerializeField] private Transform opponent;
}
