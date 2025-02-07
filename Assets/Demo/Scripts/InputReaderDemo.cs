using UnityEngine;
using UnityEngine.InputSystem;

public class InputReaderDemo : MonoBehaviour
{
    private PlayerControl controls;
    public Vector2 MoveInput { get; private set; }

    private void Awake()
    {
        controls = new PlayerControl();
        controls.Enable();

        controls.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => MoveInput = Vector2.zero;
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();
}
