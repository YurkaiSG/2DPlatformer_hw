using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMovement))]
public class PlayerAnimator : MonoBehaviour
{
    public readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    public readonly int IsJumping = Animator.StringToHash(nameof(IsJumping));
    public readonly int Speed = Animator.StringToHash(nameof(Speed));

    private Animator _animator;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        _playerMovement.Moved += AnimateMove;
        _playerMovement.Jumped += AnimateJump;
    }

    private void OnDisable()
    {
        _playerMovement.Moved -= AnimateMove;
        _playerMovement.Jumped -= AnimateJump;
    }

    private void Update()
    {
        if (_animator.GetBool(IsJumping) && _playerMovement.IsGrounded)
            _animator.SetBool(IsJumping, false);

        _animator.SetBool(IsGrounded, _playerMovement.IsGrounded);
    }

    private void AnimateMove(float direction)
    {
        _animator.SetFloat(Speed, Mathf.Abs(direction));
    }

    private void AnimateJump()
    {
        _animator.SetBool(IsJumping, true);
    }
}
