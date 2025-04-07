using System;
using UnityEngine.InputSystem;

public interface IPauseController
{
    void PauseGame();
    void ResumeGame();
    void RegisterInput(PlayerInput input);
    void UnregisterInput(PlayerInput input);
    void TogglePause();

    bool IsPaused { get; }

    event Action<bool> OnPauseStateChanged;
}
