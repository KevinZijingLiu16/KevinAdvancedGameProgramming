using UnityEngine;

public class RankUpdater : MonoBehaviour
{
    private RankManagerRedo rankManager;

    private void Start()
    {
        rankManager = GetComponent<RankManagerRedo>();
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
