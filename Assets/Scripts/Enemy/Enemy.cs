using UnityEngine;

[RequireComponent(typeof(EnemyAnimator), typeof(EnemyMovement), typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _UIBars;
    private Health _health;
    private EnemyAnimator _animator;
    private EnemyMovement _movement;

    private void Awake()
    {
        _animator = GetComponent<EnemyAnimator>();
        _movement = GetComponent<EnemyMovement>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _movement.Moved += _animator.PlayMove;
        _health.Died += DisableUI;
    }

    private void OnDisable()
    {
        _movement.Moved -= _animator.PlayMove;
        _health.Died -= DisableUI;
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
