using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public Player[] Players => _players.ToArray();
    
    public UnityEvent<int> OnNumPlayersChanged { get; } = new();

    [SerializeField] private PlayerInputManager _inputManager;

    [SerializeField] private PleaseJoinDisplay _pleaseJoinDisplay;

    [SerializeField] private PauseInputHandler _pauseInputHandler;

    private List<Player> _players = new();

    private void Start()
    {
        AllowSpawnPlayers();
    }

    public void AllowSpawnPlayers()
    {
        _inputManager.joinBehavior = PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed;
        _inputManager.EnableJoining();

        _pleaseJoinDisplay.gameObject.SetActive(true);
    }

    public void ResetPlayers()
    {
        foreach (var player in _players)
        {
            var spawnPoint = FindObjectsByType<PlayerStart>(FindObjectsSortMode.None)
                .First(start => start.TargetPlayerIndex == player.PlayerIndex);

            player.Movement.MoveTo(spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        Transform root = input.transform.root;
        var player = root.GetComponentInChildren<Player>();
        _players.Add(player);

        int index = input.playerIndex;
        player.Initialize(index);

        var spawnPoint = FindObjectsByType<PlayerStart>(FindObjectsSortMode.None)
            .First(start => start.TargetPlayerIndex == player.PlayerIndex);

        player.Movement.MoveTo(spawnPoint.transform.position, spawnPoint.transform.rotation);

        _pleaseJoinDisplay.gameObject.SetActive(false);

        OnNumPlayersChanged.Invoke(_players.Count);


        ConfigureAlwaysOnInputForPlayer(input);
    }

    //Enable AlwaysOn input map and register the player input with the pause menu manager
    private void ConfigureAlwaysOnInputForPlayer(PlayerInput input)
    {
        var globalMap = input.actions.FindActionMap("AlwaysOn", throwIfNotFound: false);
        if (globalMap != null)
        {
            globalMap.Enable();
        }

        if (_pauseInputHandler != null)
        {
            _pauseInputHandler.RegisterPlayerInput(input);
        }
    }
}
