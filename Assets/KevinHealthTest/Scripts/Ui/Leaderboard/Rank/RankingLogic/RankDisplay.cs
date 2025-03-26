using UnityEngine;
using TMPro;
using System.Linq;

public class RankDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text[] rankTextsRedo;
    private RankManager rankManagerRedo;

    private Color highestColorR = Color.green;
    private Color lowestColorR = Color.red;
    private Color defaultColorR = Color.white;

    private void Awake()
    {
        rankManagerRedo = GetComponent<RankManager>();
        if (rankManagerRedo != null)
        {
            rankManagerRedo.OnRanksUpdated += UpdateRankUI; 
        }
    }

    private void UpdateRankUI(int[] scores, int[] ranks)
    {
        int highestScore = scores.Max();
        int lowestScore = scores.Min();

        for (int i = 0; i < ranks.Length; i++)
        {
            rankTextsRedo[i].text = $"#{ranks[i]}";
            rankTextsRedo[i].color = (scores[i] == highestScore) ? highestColorR :
                                 (scores[i] == lowestScore) ? lowestColorR :
                                 defaultColorR;
        }
    }
}
