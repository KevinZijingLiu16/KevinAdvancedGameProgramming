using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool CanPlayersMove { get; private set; }

    public static GameManager Instance { get; private set; }
    
    public PlayerManager PlayerManager => _playerManager;

    [SerializeField] private RoundManager _roundManager;
    [SerializeField] private PlayerManager _playerManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        _roundManager.Initialize();
    }
}
