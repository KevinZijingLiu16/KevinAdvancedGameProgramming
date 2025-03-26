using UnityEngine;

public struct ControlEvent
{
    public float horizontalSteer;
    public bool isAccelerating;
}

[RequireComponent(typeof(CharacterInput))]
public class CharacterControls : MonoBehaviour
{
    private CharacterInput _input;

    private void Start()
    {
        _input = GetComponent<CharacterInput>();
    }

    public ControlEvent PollControls()
    {
        if (_input == null)
            return default;

        return new ControlEvent()
        {
            horizontalSteer = _input.MoveInput.x,
            isAccelerating = _input.IsAccelerating
        };
    }
}
