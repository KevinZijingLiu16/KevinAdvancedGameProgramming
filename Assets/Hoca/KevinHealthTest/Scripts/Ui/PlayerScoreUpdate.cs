using UnityEngine;
using TMPro;

public class PlayerScoreUpdate : MonoBehaviour
{
    //Only for prototype purpose, not for production.
    //Attach this script to the weapon, then compare the tag of the hit area to get the score.
    //Change the score by the current round by the GameRoundManager.
    [Header("Round Score Texts")]
    public TMP_Text roundText1;
    public TMP_Text roundText2;
    public TMP_Text roundText3;

    [Header("Game Manager Reference")]
    public GameRoundManager roundManager; 

    private bool canScore = true; 
    [SerializeField] private float resetTime = 1.5f; 

    private void OnTriggerEnter(Collider other)
    {
        if (!canScore) return; 

        
        if (roundManager == null) return;

       
        int currentRound = roundManager.CurrentRound;

        
        int scoreToAdd = GetScoreFromTag(other.tag);
        if (scoreToAdd == 0) return; 

        
        AddScoreToRound(currentRound, scoreToAdd, other.tag);

       
        canScore = false;
        Invoke(nameof(ResetScoreAbility), resetTime);
    }

   
    private int GetScoreFromTag(string tag)
    {
        return tag switch
        {
            "Head" => 20,
            "Chest" => 10,
            "Other" => 5,
            _ => 0
        };
    }

  
    private void AddScoreToRound(int round, int score, string tag)
    {
        switch (round)
        {
            case 1:
                roundText1.text = (ParseScore(roundText1.text) + score).ToString();
                break;
            case 2:
                roundText2.text = (ParseScore(roundText2.text) + score).ToString();
                break;
            case 3:
                roundText3.text = (ParseScore(roundText3.text) + score).ToString();
                break;
        }

        
        Debug.Log($"Player {gameObject.name} touch {tag}£¬get {score} ¡££¨current round: Round {round}£©");
    }

    
    private int ParseScore(string scoreText)
    {
        return int.TryParse(scoreText, out int result) ? result : 0;
    }

   
    private void ResetScoreAbility()
    {
        canScore = true;
    }
}
