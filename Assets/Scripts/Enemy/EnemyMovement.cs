using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ObjectFlipper), typeof(Animator), typeof(EnemyView))]
public class EnemyMovement : MonoBehaviour
{
    private readonly int Speed = Animator.StringToHash(nameof(Speed));

    [SerializeField] private float _speed = 8;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _chaiseTime = 2.0f;
    private ObjectFlipper _objectFlipper;
    private Animator _animator;
    private EnemyView _enemyView;
    private Transform _currentTarget;
    private int _currentWaypointIndex = 0;
    private bool _isChasing = false;
    private Coroutine _stopChaseRoutine;

    private void Awake()
    {
        _enemyView = GetComponent<EnemyView>();
        _objectFlipper = GetComponent<ObjectFlipper>();
        _animator = GetComponent<Animator>();
        _currentTarget = _waypoints[_currentWaypointIndex];
    }

    private void OnEnable()
    {
        _enemyView.FindedTarget += InitiateChase;
        _enemyView.LostTarget += StopChase;
    }

    private void OnDisable()
    {
        _enemyView.FindedTarget -= InitiateChase;
        _enemyView.LostTarget -= StopChase;
    }

    private void Update()
    {
        if (_isChasing == false)
            Patrol();

        Move();
    }

    private void Move()
    {
        Vector2 movementDirection = (_currentTarget.position - transform.position).normalized;
        _objectFlipper.FlipDirection(movementDirection.x);
        MoveToTarget(_currentTarget.position);
        _animator.SetFloat(Speed, Mathf.Abs(movementDirection.x));
    }

    private void InitiateChase(Transform target)
    {
        if (_stopChaseRoutine != null)
            StopCoroutine(_stopChaseRoutine);

        _isChasing = true;
        _currentTarget = target;
    }

    private void StopChase()
    {
        _stopChaseRoutine = StartCoroutine(StopChasingAfterTime());
    }

    private void Patrol()
    {
        if (transform.position == _currentTarget.position)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
            _currentTarget = _waypoints[_currentWaypointIndex];
        }
    }

    private void MoveToTarget(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
    }

    private IEnumerator StopChasingAfterTime()
    {
        WaitForSeconds wait = new WaitForSeconds(_chaiseTime);
        yield return wait;
        _isChasing = false;
        _currentTarget = _waypoints[_currentWaypointIndex];
    }
}
