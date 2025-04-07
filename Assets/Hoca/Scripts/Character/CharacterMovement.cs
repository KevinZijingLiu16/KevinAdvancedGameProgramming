using UnityEngine;

[RequireComponent(typeof(CharacterControls))]
[RequireComponent(typeof(CharacterController))]
public class InventoryMoveTest : MonoBehaviour
{
    [SerializeField] private float _steerSpeed = 270;
    [SerializeField] private float _steerQuickness = 2;

    [SerializeField] private float _moveSpeed = 10;
    [SerializeField] private float _friction = 2;

    private CharacterController _controller;
    private CharacterControls _controls;

    private Vector3 _targetVelocity;
    private Vector3 _currentVelocity;

    private float _yVelocity;

    private float _targetSteer;
    private float _currentSteer;

    private bool _initialized;

    public void Initialize()
    {
        _controls = GetComponent<CharacterControls>();
        _controller = GetComponent<CharacterController>();

        _controller.enabled = true;

        _initialized = true;
    }

    public void MoveTo(Vector3 position)
    {
        _controller.enabled = false;
        transform.position = position;
        _controller.enabled = true;
    }

    public void MoveTo(Vector3 position, Quaternion rotation)
    {
        _controller.enabled = false;
        transform.SetPositionAndRotation(position, rotation);
        _controller.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_initialized)
            return;

        var controlEvent = _controls.PollControls();

        Vector3 direction = transform.forward;

        if (controlEvent.isAccelerating)
        {
            _targetVelocity = direction * _moveSpeed;
        }
        else
        {
            _targetVelocity = Vector3.zero;
        }

        _targetSteer = controlEvent.horizontalSteer;

        _currentSteer = Mathf.Lerp(_currentSteer, _targetSteer, _steerQuickness * Time.deltaTime);
        
        transform.Rotate(_currentSteer * _steerSpeed * Vector3.up * Time.deltaTime);

        _currentVelocity = Vector3.Lerp(_currentVelocity, _targetVelocity, _friction * Time.deltaTime);
        _currentVelocity.y = _yVelocity;

        if (!_controller.isGrounded)
        {
            _yVelocity += Physics.gravity.y * Time.deltaTime;
        }

        _controller.Move(_currentVelocity * Time.deltaTime);
    }
}
