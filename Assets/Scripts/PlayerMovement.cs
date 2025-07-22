using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
[RequireComponent(typeof(ObjectFlipper), typeof(GroundDetector), typeof(InputReader))]
public class PlayerMovement : MonoBehaviour
{
    public readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    public readonly int IsJumping = Animator.StringToHash(nameof(IsJumping));
    public readonly int Speed = Animator.StringToHash(nameof(Speed));

    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _jumpForce = 10.0f;
    private GroundDetector _groundDetector;
    private ObjectFlipper _objectFlipper;
    private InputReader _inputReader;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private bool _isGrounded = true;

    private void Awake()
    {
        _groundDetector = GetComponent<GroundDetector>();
        _objectFlipper = GetComponent<ObjectFlipper>();
        _inputReader = GetComponent<InputReader>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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
        if (_animator.GetBool(IsJumping) && _isGrounded)
            _animator.SetBool(IsJumping, false);

        _isGrounded = _groundDetector.CheckGround();
        _animator.SetBool(IsGrounded, _isGrounded);
    }

    private void Move(float direction)
    {
        _objectFlipper.FlipDirection(direction);
        _animator.SetFloat(Speed, Mathf.Abs(direction));
        float _distance = direction * _speed * Time.deltaTime;
        transform.Translate(_distance * Vector3.right, Space.World);
    }

    private void ExecuteJump()
    {
        if (_isGrounded)
        {
            _isGrounded = false;
            _animator.SetBool(IsJumping, true);
            _rigidbody.AddForce(new Vector2(_rigidbody.velocity.x, _jumpForce), ForceMode2D.Impulse);
        }
    }
}
