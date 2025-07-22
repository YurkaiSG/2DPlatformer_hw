using UnityEngine;

[RequireComponent(typeof(ObjectFlipper), typeof(Animator))]
public class EnemyMovement : MonoBehaviour
{
    private readonly int Speed = Animator.StringToHash(nameof(Speed));

    [SerializeField] private float _speed = 8;
    [SerializeField] private Transform[] _waypoints;
    private ObjectFlipper _objectFlipper;
    private Animator _animator;
    private int _currentWaypointIndex = 0;

    private void Start()
    {
        _objectFlipper = GetComponent<ObjectFlipper>();
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
        _objectFlipper.FlipDirection(movementDirection.x);
        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypointIndex].position, _speed * Time.deltaTime);
        _animator.SetFloat(Speed, Mathf.Abs(movementDirection.x));
    }
}
