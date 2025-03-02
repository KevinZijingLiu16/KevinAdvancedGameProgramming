using System.Collections;
using UnityEngine;
using TMPro;

public class GameRoundManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text roundText; 
    public TMP_Text timerText;
    [SerializeField] private PlayerScorePanel[] scorePanels; //get the panel reference from directly.

    [Header("Game Settings")]
    public float roundDuration = 30f; 
    private int currentRound = 1; 

    public int CurrentRound => currentRound; 

    private void Start()
    {
        UpdateRoundText();
        StartCoroutine(RoundTimer());
    }

    private IEnumerator RoundTimer()
    {
        while (currentRound <= 3)
        {
            float timeLeft = roundDuration;
            while (timeLeft > 0)
            {
                timerText.text = $"{Mathf.Ceil(timeLeft)}s"; 
                yield return new WaitForSeconds(1);
                timeLeft--;
            }

            currentRound++;
            UpdateRoundText();
        }

      
       // UpdateFinalTotalScores();

       
        timerText.text = "GAME OVER";
    }

    private void UpdateRoundText()
    {
        if (currentRound <= 3)
        {
            roundText.text = $"Round {currentRound}";
        }
    }

   
   /* private void UpdateFinalTotalScores()
    {
       
        foreach (var panel in scorePanels)
        {
            panel.ForceUpdateTotalScore();
        }
    }*/
}
