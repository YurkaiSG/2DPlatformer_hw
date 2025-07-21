using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class EnemyMovement : MonoBehaviour
{
    private readonly int Speed = Animator.StringToHash(nameof(Speed));

    [SerializeField] private float _speed = 8;
    [SerializeField] private Transform[] _waypoints;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private int _currentWaypointIndex = 0;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position == _waypoints[_currentWaypointIndex].position)
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;

        Vector2 movementDirection = (_waypoints[_currentWaypointIndex].position - transform.position).normalized;
        Flip(movementDirection);
        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypointIndex].position, _speed * Time.deltaTime);
        _animator.SetFloat(Speed, Mathf.Abs(movementDirection.x));
    }

    private void Flip(Vector2 movementDirection)
    {
        if (movementDirection.x < 0)
            _spriteRenderer.flipX = true;
        else if (movementDirection.x > 0)
            _spriteRenderer.flipX = false;
    }
}
