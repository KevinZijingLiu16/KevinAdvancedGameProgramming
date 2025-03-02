using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class RankManager : MonoBehaviour
{
    private int[] scores = new int[4];

    public delegate void RankUpdatedHandler(Dictionary<int, int> rankMap, int highestScore, int lowestScore);
    public event RankUpdatedHandler OnRankUpdated; //set the event

    public void SetTotalScore(int index, int score) //player index 0-3
    {
        if (index < 0 || index >= scores.Length) return;

        scores[index] = score;
        UpdateRankings();
    }

    private void UpdateRankings()
    {
        var sortedScores = scores.Distinct().OrderByDescending(s => s).ToList();//remove the repeated scores and sort them in descending order
        Dictionary<int, int> scoreToRank = new();

        for (int i = 0; i < sortedScores.Count; i++)
        {
            scoreToRank[sortedScores[i]] = i + 1;
        }

        int highestScore = sortedScores.First();
        int lowestScore = sortedScores.Last();

        OnRankUpdated?.Invoke(scoreToRank, highestScore, lowestScore);//invoke the event when the rankings are updated
    }

    public int GetScore(int index) //allow other scripts to get the score
    {
        return (index >= 0 && index < scores.Length) ? scores[index] : 0;
    }
}
