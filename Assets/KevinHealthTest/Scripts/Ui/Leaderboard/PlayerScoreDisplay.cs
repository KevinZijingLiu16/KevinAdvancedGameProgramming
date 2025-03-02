using UnityEngine;
using TMPro;

public class PlayerScoreDisplay : MonoBehaviour
{
    private PlayerScorePanel scorePanel;
    private PlayerScoreCalculator scoreCalculator;

    private void Awake()
    {
        scorePanel = GetComponent<PlayerScorePanel>();
        scoreCalculator = GetComponent<PlayerScoreCalculator>();

        if (scoreCalculator != null)
        {
            scoreCalculator.OnTotalScoreUpdated += UpdateTotalScoreUI; 
        }
    }

    private void UpdateTotalScoreUI(int newTotal)
    {
        if (scorePanel == null || scorePanel.totalScore == null) return;

        scorePanel.totalScore.text = newTotal.ToString(); 
    }

    
}
