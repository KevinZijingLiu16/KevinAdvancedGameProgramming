using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController controller;
    private InputReaderDemo inputReader;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputReader = GetComponent<InputReaderDemo>();
    }

    private void Update()
    {
        Vector2 moveInput = inputReader.MoveInput;
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y); // XZ ÷·“∆∂Ø

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
