using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;

public class PauseController : MonoBehaviour,IPauseController
{
    [SerializeField] private Canvas pauseMenuCanvas;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private bool pauseByTimeScale = true;

    private bool isPaused = false;
    private List<PlayerInput> registeredPlayerInputs = new();

    public event Action<bool> OnPauseStateChanged;    

    public void RegisterInput(PlayerInput input)
    {
        if (input != null && !registeredPlayerInputs.Contains(input))
        {
            registeredPlayerInputs.Add(input);
        }
    }
    public void UnregisterInput(PlayerInput input)
    {
        if (input != null && registeredPlayerInputs.Contains(input))
        {
            registeredPlayerInputs.Remove(input);
        }
    }
    public void TogglePause()
    {
        if (isPaused) ResumeGame();
        else PauseGame();
    }

    public void PauseGame()
    {
        isPaused = true;
        if (pauseByTimeScale)
            Time.timeScale = 0f;

        if (pauseMenuCanvas != null)
            pauseMenuCanvas.enabled = true;

        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(true);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        foreach (var input in registeredPlayerInputs)
        {
            input.SwitchCurrentActionMap("UI");
        }

        OnPauseStateChanged?.Invoke(true);// for future use
    }
    public void ResumeGame()
    {
        isPaused = false;
        if (pauseByTimeScale)
            Time.timeScale = 1f;

        if (pauseMenuCanvas != null)
            pauseMenuCanvas.enabled = false;

        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        foreach (var input in registeredPlayerInputs)
        {
            input.SwitchCurrentActionMap("Player");
        }

        OnPauseStateChanged?.Invoke(false); // for future use
    }
    public bool IsPaused => isPaused;
    
}
