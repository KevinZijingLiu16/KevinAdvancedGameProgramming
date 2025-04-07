using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;

public class PauseInputHandler : MonoBehaviour
{
    //[SerializeField] private PauseController pauseController;
    [SerializeField] private PauseUIManager pauseUIManager; 

    private List<PlayerInput> playerInputs = new();

    private IPauseController pauseController;

    private void Awake()
    {
        pauseController = GetComponent<PauseController>();
    }

    public void RegisterPlayerInput(PlayerInput input)
    {
        if (input == null || playerInputs.Contains(input)) return;

        playerInputs.Add(input);

        var pauseAction = input.actions["Pause"];
        pauseAction.performed += OnPausePerformed;

        var cancelAction = input.actions.FindAction("Cancel");
        if (cancelAction != null)
        {
            cancelAction.performed += OnCancelPerformed;
        }

        
        pauseController.RegisterInput(input);
    }

    public void UnregisterPlayerInput(PlayerInput input)
    {
        if (input == null || !playerInputs.Contains(input)) return;

        var pauseAction = input.actions["Pause"];
        pauseAction.performed -= OnPausePerformed;

        var cancelAction = input.actions.FindAction("Cancel");
        if (cancelAction != null)
        {
            cancelAction.performed -= OnCancelPerformed;
        }

        pauseController.UnregisterInput(input);
        playerInputs.Remove(input);
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        pauseController.TogglePause();
    }

    private void OnCancelPerformed(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        var inputDevice = context.control.device;

        foreach (var playerInput in playerInputs)
        {
            if (!playerInput.devices.Contains(inputDevice)) continue;
            if (playerInput.currentActionMap.name != "UI") return;

            if (pauseUIManager.IsSettingsOpen())
            {
                pauseUIManager.OnClickBackFromSettings();
                Debug.Log("Cancel pressed, returning to Pause Menu");
            }
            else
            {
                pauseController.ResumeGame();
                Debug.Log("Cancel pressed, resuming game");
            }
        }
    }
}
