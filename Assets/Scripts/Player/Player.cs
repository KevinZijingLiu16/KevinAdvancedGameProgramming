using UnityEngine;
using Unity.Cinemachine;

public class Player : MonoBehaviour
{
    public int PlayerIndex { get; private set; }

    public CharacterMovement Movement => _movement;

    [SerializeField] private GameObject _cinemachineRoot;
    [SerializeField] private CharacterMovement _movement;

    public void Initialize(int playerIndex)
    {
        PlayerIndex = playerIndex;

        var cinemachineCam = _cinemachineRoot.GetComponentInChildren<CinemachineCamera>();
        var cinemachineBrain = _cinemachineRoot.GetComponentInChildren<CinemachineBrain>();
        cinemachineCam.OutputChannel = (OutputChannels)(playerIndex + 1);
        cinemachineBrain.ChannelMask = (OutputChannels)(playerIndex + 1);

        _movement.Initialize();
    }
}
