using FightingGame.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

public class FighterInputReader : MonoBehaviour
{
    [SerializeField]
    private InputActionReference _moveAction;

    private void OnEnable()
    {
        _moveAction.action.Enable();
    }

    private void OnDisable()
    {
        _moveAction.action.Disable();
    }

    private void Update()
    {
        Vector2 rawInput = _moveAction.action.ReadValue<Vector2>();

        AbsoluteDirection direction = DirectionConverter.ToAbsolute(rawInput);

        Debug.Log(direction);
    }
}
