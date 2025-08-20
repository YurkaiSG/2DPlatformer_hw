using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform _UIBars;
    private PlayerAnimator _animator;
    private PlayerMovement _movement;
    private Jumper _jumper;
    private Health _health;

    private void Awake()
    {
        _animator = GetComponent<PlayerAnimator>();
        _movement = GetComponent<PlayerMovement>();
        _jumper = GetComponent<Jumper>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _movement.Moved += _animator.PlayMove;
        _jumper.Jumped += _animator.PlayJump;
        _health.Died += DisableUI;
    }

    private void OnDisable()
    {
        _movement.Moved -= _animator.PlayMove;
        _jumper.Jumped -= _animator.PlayJump;
        _health.Died += DisableUI;
    }

    private void Update()
    {
        _animator.SetIsGrounded(_jumper.IsGrounded);
    }

    private void LateUpdate()
    {
        _UIBars.position = transform.position;
    }

    private void DisableUI()
    {
        _UIBars.gameObject.SetActive(false);
    }
}
