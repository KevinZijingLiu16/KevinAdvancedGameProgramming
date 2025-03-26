using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    public Vector3 MoveInput { get; private set; }
    public bool IsAccelerating { get; private set; }

    public AudioSource moveSound;
    //public AudioSource moveVibration;

    public void OnMoveInput(InputAction.CallbackContext ctx)
    {
        var value = ctx.ReadValue<Vector2>();

        MoveInput = new(value.x, 0, value.y);
    }

    public void OnAccelerateInput(InputAction.CallbackContext ctx)
    {
        IsAccelerating = ctx.ReadValue<float>() > 0;

        if (IsAccelerating)
        {
            /*
             if (!moveSound.isPlaying && !moveVibration.isPlaying)
             {
                 moveSound.gamepadSpeakerOutputType = GamepadSpeakerOutputType.Speaker;
                 moveSound.PlayOnGamepad(0);

                 moveVibration.gamepadSpeakerOutputType = GamepadSpeakerOutputType.Vibration;
                 moveVibration.PlayOnGamepad(0);
             }
             */

            moveSound.Play();
        }
        else
        {
            moveSound.Stop();
            //moveVibration.Stop();
        }
    }
}
