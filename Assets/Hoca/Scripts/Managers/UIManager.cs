using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    
    [SerializeField] private TMP_Text roundText; 
    [SerializeField] private TMP_Text timerText; 
    [SerializeField] private GameObject gameOverPanel; 

    private void Start()
    {
        gameOverPanel.SetActive(false); 
    }

   
    public void UpdateRound(int round)
    {
        roundText.text = $"Round {round}";
    }

   
    public void UpdateTimer(float timeLeft)
    {
        timerText.text = $"{Mathf.Ceil(timeLeft)}s";
    }

   
    public void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
        timerText.text = "GAME OVER";
    }
}
