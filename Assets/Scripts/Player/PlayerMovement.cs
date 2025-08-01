using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ObjectFlipper), typeof(GroundDetector), typeof(InputReader))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _jumpForce = 10.0f;
    private GroundDetector _groundDetector;
    private ObjectFlipper _objectFlipper;
    private InputReader _inputReader;
    private Rigidbody2D _rigidbody;

    public bool IsGrounded { get; private set; }
    public event Action<float> Moved;
    public event Action Jumped;

    private void Awake()
    {
        _groundDetector = GetComponent<GroundDetector>();
        _objectFlipper = GetComponent<ObjectFlipper>();
        _inputReader = GetComponent<InputReader>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _inputReader.Moved += Move;
        _inputReader.Jumped += ExecuteJump;
    }

    private void OnDisable()
    {
        _inputReader.Moved -= Move;
        _inputReader.Jumped -= ExecuteJump;
    }

    private void Update()
    {
        IsGrounded = _groundDetector.IsGrounded();
    }

    private void Move(float direction)
    {
        _objectFlipper.FlipDirection(direction);
        Moved?.Invoke(direction);
        float _distance = direction * _speed * Time.deltaTime;
        transform.Translate(_distance * Vector3.right, Space.World);
    }

    private void ExecuteJump()
    {
        if (IsGrounded)
        {
            IsGrounded = false;
            Jumped?.Invoke();
            _rigidbody.AddForce(new Vector2(_rigidbody.velocity.x, _jumpForce), ForceMode2D.Impulse);
        }
    }
}
