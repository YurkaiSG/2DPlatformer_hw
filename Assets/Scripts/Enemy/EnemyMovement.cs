using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ObjectFlipper))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 8;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _chaiseTime = 2.0f;
    [SerializeField] private EnemyView _enemyView;
    private ObjectFlipper _objectFlipper;
    private Transform _currentTarget;
    private int _currentWaypointIndex = 0;
    private bool _isChasing = false;
    private Coroutine _stopChaseRoutine;

    public Action<float> Moved;

    private void Awake()
    {
        _objectFlipper = GetComponent<ObjectFlipper>();
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
        Moved?.Invoke(Mathf.Abs(movementDirection.x));
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
        if (enabled)
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
