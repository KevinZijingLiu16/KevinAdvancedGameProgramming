using UnityEngine;
using TMPro;

public class UIRankSource : MonoBehaviour, IScoreSource
{
    [SerializeField] private TMP_Text[] totalScores;

    public int[] GetScores()
    {
        int[] scores = new int[totalScores.Length];
        for (int i = 0; i < totalScores.Length; i++)
        {
            scores[i] = ParseScore(totalScores[i].text);
        }
        return scores;
    }

    private int ParseScore(string scoreText)
    {
        return int.TryParse(scoreText, out int result) ? result : 0;
    }
}
