using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    private PlayerAnimator _animator;
    private PlayerMovement _movement;
    private Jumper _jumper;

    private void Awake()
    {
        _animator = GetComponent<PlayerAnimator>();
        _movement = GetComponent<PlayerMovement>();
        _jumper = GetComponent<Jumper>();
    }

    private void OnEnable()
    {
        _movement.Moved += _animator.PlayMove;
        _jumper.Jumped += _animator.PlayJump;
    }

    private void OnDisable()
    {
        _movement.Moved -= _animator.PlayMove;
        _jumper.Jumped -= _animator.PlayJump;
    }

    private void Update()
    {
        _animator.SetIsGrounded(_jumper.IsGrounded);
    }
}
