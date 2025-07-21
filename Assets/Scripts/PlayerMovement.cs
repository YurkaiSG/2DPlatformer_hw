using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);
    private readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    private readonly int IsJumping = Animator.StringToHash(nameof(IsJumping));
    private readonly int Speed = Animator.StringToHash(nameof(Speed));

    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _jumpForce = 10.0f;
    [Space(8)]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Vector2 _groundCheckBoxSize;
    [SerializeField] private float _groundCheckCastDistance;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private float _direction;
    private float _groundCheckAngle = 0;
    private bool _isGrounded = true;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _isGrounded = CheckGround();
        _animator.SetBool(IsGrounded, _isGrounded);
        Move();
        ExecuteJump();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * _groundCheckCastDistance, _groundCheckBoxSize);
    }

    private void Move()
    {
        _direction = Input.GetAxis(Horizontal);
        FlipDirection();
        _animator.SetFloat(Speed, Mathf.Abs(_direction));
        float _distance = _direction * _speed * Time.deltaTime;
        transform.Translate(_distance * Vector3.right);
    }

    private void ExecuteJump()
    {
        if (Input.GetButtonDown(Jump) && _isGrounded)
        {
            _isGrounded = false;
            _animator.SetBool(IsJumping, true);
            _rigidbody.AddForce(new Vector2(_rigidbody.velocity.x, _jumpForce), ForceMode2D.Impulse);
        }

        if (_animator.GetBool(IsJumping) && _isGrounded)
            _animator.SetBool(IsJumping, false);
    }

    private void FlipDirection()
    {
        if (_direction < 0)
            _spriteRenderer.flipX = true;
        else if (_direction > 0)
            _spriteRenderer.flipX = false;
    }

    private bool CheckGround()
    {
        if (Physics2D.BoxCast(transform.position, _groundCheckBoxSize, _groundCheckAngle, -transform.up, _groundCheckCastDistance, _groundLayer))
            return true;
        else
            return false;
    }
}
