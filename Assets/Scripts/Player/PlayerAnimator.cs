using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMovement))]
public class PlayerAnimator : MonoBehaviour
{
    public readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    public readonly int IsJumping = Animator.StringToHash(nameof(IsJumping));
    public readonly int Speed = Animator.StringToHash(nameof(Speed));

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_animator.GetBool(IsJumping) && _animator.GetBool(IsGrounded))
            _animator.SetBool(IsJumping, false);
    }

    public void SetIsGrounded(bool isGrounded)
    {
        _animator.SetBool(IsGrounded, isGrounded);
    }

    public void PlayMove(float direction)
    {
        _animator.SetFloat(Speed, Mathf.Abs(direction));
    }

    public void PlayJump()
    {
        _animator.SetBool(IsJumping, true);
    }
}
