using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseUIManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject pauseMenuPanel;
    //[SerializeField] private PauseController pauseController;

    private IPauseController pauseController;

    private void Awake()
    {
        pauseController = GetComponent<PauseController>();
    }

    public void OnClickResume()
    {
        pauseController?.ResumeGame();
    }

    public void OnClickSettings()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);

        SetFirstSelected(settingsPanel);
    }

    public void OnClickBackFromSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);

        SetFirstSelected(pauseMenuPanel);
    }

    public bool IsSettingsOpen()
    {
        return settingsPanel != null && settingsPanel.activeSelf;
    }

    private void SetFirstSelected(GameObject panel)
    {
        var firstSelectable = panel.GetComponentInChildren<Selectable>();
        if (firstSelectable != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstSelectable.gameObject);
        }
    }
}
