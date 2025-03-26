using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class DefaultRankStrategy : IRankStrategy
{
    public int[] CalculateRanks(int[] scores)
    {
        var sortedScores = scores.Distinct().OrderByDescending(s => s).ToList();
        Dictionary<int, int> scoreToRank = new();

        for (int i = 0; i < sortedScores.Count; i++)
        {
            scoreToRank[sortedScores[i]] = i + 1;
        }

        int[] ranks = new int[scores.Length];
        for (int i = 0; i < scores.Length; i++)
        {
            ranks[i] = scoreToRank[scores[i]];
        }

        return ranks;
    }
}
