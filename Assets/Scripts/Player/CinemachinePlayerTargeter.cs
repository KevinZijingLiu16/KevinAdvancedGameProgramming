using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineTargetGroup))]
public class CinemachinePlayerTargeter : MonoBehaviour
{
    private CinemachineTargetGroup _targetGroup;

    [SerializeField] private Transform _localPlayer;

    [SerializeField] private float _playerRadius = 2;

    [SerializeField] private float _otherPlayersWeight = 0.33f;

    private void Start()
    {
        _targetGroup = GetComponent<CinemachineTargetGroup>();
    }

    private void FixedUpdate()
    {
        var allPlayers = GameManager.Instance.PlayerManager.Players;

        if (allPlayers.Length == _targetGroup.Targets.Count)
            return;

        var otherPlayers = allPlayers.Where(p => p.transform != _localPlayer).ToArray();

        _targetGroup.Targets.Clear();

        float weight = 1f / allPlayers.Length * _otherPlayersWeight;
        foreach (var player in otherPlayers)
        {
            _targetGroup.Targets.Add(new CinemachineTargetGroup.Target
            {
                Object = player.transform,
                Weight = weight,
                Radius = _playerRadius
            });
        }

        _targetGroup.Targets.Add(new CinemachineTargetGroup.Target
        {
            Object = _localPlayer,
            Weight = 1f,
            Radius = _playerRadius
        });
    }
}
