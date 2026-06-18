using UnityEngine;
using FightingGame.Character;

public class FighterFacing : MonoBehaviour
{
    public FighterFaicing Current { get; private set; } = FighterFaicing.Right;

    public void UpdateFacing(bool canTurn)
    {
        if (!canTurn) return;

        Current = opponent.position.x >= transform.position.y ? FighterFaicing.Right : FighterFaicing.Left;
    }

    [SerializeField] private Transform opponent;
}
