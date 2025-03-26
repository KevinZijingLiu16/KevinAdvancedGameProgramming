using UnityEngine;

public class RankUpdater : MonoBehaviour
{
    private RankManager rankManager;

    private void Start()
    {
        rankManager = GetComponent<RankManager>();
        InvokeRepeating(nameof(UpdateRankings), 1f, 1f); 
    }

    private void UpdateRankings()
    {
        if (rankManager != null)
        {
            rankManager.UpdateRankings();
        }
    }
}
