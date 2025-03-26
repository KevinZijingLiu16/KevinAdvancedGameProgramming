using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;            
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

public class PauseMenuManager : MonoBehaviour
{

    [SerializeField] private PlayerManager playerManager;
  
    [Header("UI References")]
    [SerializeField] private Canvas pauseMenuCanvas;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject pauseMenuPanel;


    [Header("Settings")]
    [SerializeField] private bool pauseByTimeScale = true;
   
    private bool isPaused;

   
    private List<PlayerInput> registeredPlayerInputs = new List<PlayerInput>();

    private void Awake()
    {
      
       
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.enabled = false;
        }
    }


    public void RegisterPlayerInput(PlayerInput input)
    {
        if (input == null) return;
        if (registeredPlayerInputs.Contains(input)) return;

        registeredPlayerInputs.Add(input);

        var pauseAction = input.actions["Pause"];
        pauseAction.performed += OnPausePerformed;

        var cancelAction = input.actions.FindAction("Cancel");
        if (cancelAction != null)
        {
            cancelAction.performed += OnCancelPerformed;
        }
    }


    // UnregisterPlayerInput if needed
    public void UnregisterPlayerInput(PlayerInput input)
    {
        if (input == null) return;
        if (!registeredPlayerInputs.Contains(input)) return;

        var pauseAction = input.actions["Pause"];
        pauseAction.performed -= OnPausePerformed;

        var cancelAction = input.actions.FindAction("Cancel");
        if (cancelAction != null)
        {
            cancelAction.performed -= OnCancelPerformed;
        }

        registeredPlayerInputs.Remove(input);
    }

    private void OnCancelPerformed(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        var input = context.control.device as InputDevice;

       
        foreach (var playerInput in registeredPlayerInputs)
        {
            if (playerInput.devices.Contains(input))
            {
                
                if (playerInput.currentActionMap.name != "UI") return;

                if (settingsPanel != null && settingsPanel.activeSelf)
                {
                    OnClickBackFromSettings();
                    Debug.Log("Cancel pressed, returning to Pause Menu");
                }
                if (pauseMenuPanel != null && pauseMenuPanel.activeSelf)
                {
                    OnClickResume();
                    Debug.Log("Cancel pressed, resuming game");
                }
            }
        }
    }


    

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
       
        if (!context.performed)
            return;

        TogglePause();
    }

   
    private void TogglePause()
    {
        if (!isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }


    private void PauseGame()
    {
        isPaused = true;
        if (pauseByTimeScale)
            Time.timeScale = 0f;

        pauseMenuCanvas.enabled = true;
        pauseMenuPanel.SetActive(true);
        settingsPanel.SetActive(false); 

        foreach (var playerInput in registeredPlayerInputs)
        {
            playerInput.SwitchCurrentActionMap("UI");
            Debug.Log("Input Action Map switched to UI");
        }
    }



    public void ResumeGame()
    {
        isPaused = false;
        if (pauseByTimeScale)
            Time.timeScale = 1f;

        pauseMenuCanvas.enabled = false;

       
        pauseMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);

        foreach (var playerInput in registeredPlayerInputs)
        {
            playerInput.SwitchCurrentActionMap("Player");
            Debug.Log("Input Action Map switched to Player");
        }
    }


    public void OnClickRestart()
    {
        
        if (playerManager != null)
        {
            if (!pauseMenuCanvas.enabled) return;
            ResumeGame();  // resume game before restarting just in case the time scale is 0 now
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Restart Function is called...");
        }
    }

    public void OnClickResetPosition()
    {
        if (playerManager != null)
        {
            ResumeGame();
            playerManager.ResetPlayers();
        }
    }


    public void OnClickMainMenu()
    {
        // ResumeGame();
       //SceneManager.LoadScene("MainMenu");

    }

   
    public void OnClickResume()
    {
        ResumeGame();
    }


    public void OnClickSettings()
    {
        pauseMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);

        GameObject firstSelected = settingsPanel.GetComponentInChildren<Selectable>().gameObject;
        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }
    

    public void OnClickBackFromSettings()
    {
        settingsPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);

        GameObject firstSelected = pauseMenuPanel.GetComponentInChildren<Selectable>().gameObject;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

}
