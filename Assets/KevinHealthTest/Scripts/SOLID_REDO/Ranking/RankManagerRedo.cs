using UnityEngine;
using System;

public class RankManagerRedo : MonoBehaviour
{
    private IScoreSource scoreSource;
    private IRankStrategy rankStrategy;

    public event Action<int[], int[]> OnRanksUpdated; 

    private void Awake()
    {
        scoreSource = GetComponent<IScoreSource>();
        rankStrategy = new DefaultRankStrategy();
    }

    public void UpdateRankings()
    {
        if (scoreSource == null || rankStrategy == null) return;

        int[] scores = scoreSource.GetScores();
        int[] ranks = rankStrategy.CalculateRanks(scores);

        OnRanksUpdated?.Invoke(scores, ranks); 
    }
}
