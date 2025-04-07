using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSceneManager : MonoBehaviour
{
   // [SerializeField] private PauseController pauseController;
    [SerializeField] private PlayerManager playerManager;
    private IPauseController pauseController;

    private void Awake()
    {
        pauseController = GetComponent<PauseController>();
    }
    public void OnClickRestart()
    {
        if (pauseController == null) return;

        if (pauseController.IsPaused)
        {
            pauseController.ResumeGame(); 
        }

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
        Debug.Log("Scene restarted.");
    }

    public void OnClickResetPosition()
    {
        if (playerManager != null)
        {
            pauseController.ResumeGame(); 
            playerManager.ResetPlayers();
            Debug.Log("Player position reset.");
        }
    }

    public void OnClickMainMenu()
    {
        
        // pauseController.ResumeGame();
        // SceneManager.LoadScene("MainMenu");
        Debug.Log("Main Menu clicked (functionality not yet implemented).");
    }
}
