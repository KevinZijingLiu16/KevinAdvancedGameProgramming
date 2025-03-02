using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class RankDisplay : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text[] totalScores = new TMP_Text[4]; 
    public TMP_Text[] rankTexts = new TMP_Text[4]; 

    private RankManager rankManager;

    private Color highestColor = Color.green;
    private Color lowestColor = Color.red;
    private Color defaultColor = Color.yellow;

    private void Awake()
    {
        rankManager = GetComponent<RankManager>();

        if (rankManager != null)
        {
            rankManager.OnRankUpdated += UpdateRankUI; //subscribe to the event
        }
    }

    private void Update()
    {
        for (int i = 0; i < totalScores.Length; i++)
        {
            int score = ParseScore(totalScores[i].text);//conver text to int
            rankManager.SetTotalScore(i, score); // send the score to the RankManager
        }
    }

    private void UpdateRankUI(Dictionary<int, int> rankMap, int highestScore, int lowestScore)
    {
        for (int i = 0; i < totalScores.Length; i++)
        {
            int score = rankManager.GetScore(i);
            int rank = rankMap.ContainsKey(score) ? rankMap[score] : 0;

            rankTexts[i].text = $"#{rank}";

            if (score == highestScore)
                rankTexts[i].color = highestColor;
            else if (score == lowestScore)
                rankTexts[i].color = lowestColor;
            else
                rankTexts[i].color = defaultColor;
        }
    }

    private int ParseScore(string scoreText)
    {
        return int.TryParse(scoreText, out int result) ? result : 0;
    }
}
