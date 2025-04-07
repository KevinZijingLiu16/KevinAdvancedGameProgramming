using UnityEngine;
using TMPro;

public class PlayerScoreCalculator : MonoBehaviour
{
    private PlayerScorePanel scorePanel;

    private int score1;
    private int score2;
    private int score3;
    private int totalScore;

    public delegate void ScoreUpdatedHandler(int newTotal);
    public event ScoreUpdatedHandler OnTotalScoreUpdated;

    private void Awake()
    {
        scorePanel = GetComponent<PlayerScorePanel>();
    }

    private void Update()
    {
        if (scorePanel == null) return;

        
        score1 = ParseScore(scorePanel.roundText1.text);
        score2 = ParseScore(scorePanel.roundText2.text);
        score3 = ParseScore(scorePanel.roundText3.text);
        
       
        int newTotal = score1 + score2 + score3;

        
        if (newTotal != totalScore)
        {
            totalScore = newTotal;
            OnTotalScoreUpdated?.Invoke(totalScore); 
        }
    }

    private int ParseScore(string scoreText)
    {
        return int.TryParse(scoreText, out int result) ? result : 0;
        //use TryParse to convert text to int
    }

    public int GetTotalScore()
    {
        return totalScore;
    }
}
