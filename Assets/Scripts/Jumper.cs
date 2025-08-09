using System;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Rigidbody2D), typeof(GroundDetector))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 10.0f;
    private GroundDetector _groundDetector;
    private InputReader _inputReader;
    private Rigidbody2D _rigidbody;

    public bool IsGrounded { get; private set; }
    public event Action Jumped;

    private void Awake()
    {
        _groundDetector = GetComponent<GroundDetector>();
        _inputReader = GetComponent<InputReader>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        IsGrounded = _groundDetector.IsGrounded();
    }

    private void OnEnable()
    {
        _inputReader.Jumped += Jump;
    }

    private void OnDisable()
    {
        _inputReader.Jumped -= Jump;
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            IsGrounded = false;
            Jumped?.Invoke();
            _rigidbody.AddForce(new Vector2(_rigidbody.velocity.x, _jumpForce), ForceMode2D.Impulse);
        }
    }
}
