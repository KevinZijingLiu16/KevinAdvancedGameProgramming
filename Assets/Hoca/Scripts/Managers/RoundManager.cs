using System;
using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public event Action<int> OnRoundStart;
    public event Action<int> OnRoundEnd;
    public event Action OnGameOver;
   
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private UIManager uiManager;
    
    [SerializeField] private float roundDuration = 30f;
    [SerializeField] private float timerStartCounting = 1f;
    [SerializeField] private int totalRounds = 3;

    private bool isGameOver = false;

    private bool _enoughPlayers;

    private int currentRound = 1;

    public void Initialize()
    {
        playerManager.OnNumPlayersChanged.AddListener(OnNumPlayersChanged);
        playerManager.AllowSpawnPlayers();
    }

    private void OnNumPlayersChanged(int numPlayers)
    {
        if (_enoughPlayers)
            return;

        if (numPlayers < 2)
            return;
        
        _enoughPlayers = true;

        ResetRound();
    } 

    public void ResetRound()
    {
        playerManager.ResetPlayers();

        StartRound();
    }  

    private void StartRound()
    {
        if (isGameOver) return;

        OnRoundStart?.Invoke(currentRound);

        uiManager.UpdateRound(currentRound);

        //playerManager.ResetPlayers();

        StartCoroutine(RoundTimer());
    }

    private IEnumerator RoundTimer()
    {
        float timer = roundDuration;
        while (timer > 0)
        {
            uiManager.UpdateTimer(timer);
            yield return new WaitForSeconds(timerStartCounting);
            timer--;
        }

        EndRound();
    }

    private void EndRound()
    {
        OnRoundEnd?.Invoke(currentRound);

        if (currentRound >= totalRounds)
        {
            isGameOver = true;
            uiManager.ShowGameOverScreen();
            OnGameOver?.Invoke();
            
        }
        else
        {
            currentRound++;
            ResetRound();
        }
    }
}
