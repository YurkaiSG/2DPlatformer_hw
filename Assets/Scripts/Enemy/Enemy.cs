using UnityEngine;

[RequireComponent(typeof(EnemyAnimator), typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _healthBar;
    private EnemyAnimator _animator;
    private EnemyMovement _movement;

    private void Awake()
    {
        _animator = GetComponent<EnemyAnimator>();
        _movement = GetComponent<EnemyMovement>();
    }

    private void OnEnable()
    {
        _movement.Moved += _animator.PlayMove;
    }

    private void OnDisable()
    {
        _movement.Moved -= _animator.PlayMove;
    }

    private void LateUpdate()
    {
        _healthBar.position = transform.position;
    }
}
