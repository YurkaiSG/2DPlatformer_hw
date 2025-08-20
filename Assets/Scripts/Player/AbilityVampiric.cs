using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Health))]
public class AbilityVampiric: MonoBehaviour
{
    [SerializeField] private float _duration = 6f;
    [SerializeField] private float _cooldown = 4f;
    [SerializeField] private float _interval = .5f;
    [SerializeField] private float _radius = 8f;
    [SerializeField] private float _damage = 4f;
    [SerializeField] private LayerMask _enemyLayerMask = 6;

    private InputReader _inputReader;
    private Health _health;
    private float _minBarValue = 0f;
    private bool _canActivate = true;
    private bool _isActive = false;
    private bool _isOnCooldown = false;

    public event Action<bool> Activated;
    public event Action<float> BarChanged;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _inputReader.AbilityActivated += Activate;
    }

    private void OnDisable()
    {
        _inputReader.AbilityActivated -= Activate;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    private void Activate()
    {
        if (_canActivate == false)
            return;

        _canActivate = false;
        StartCoroutine(ExecuteAbility());
    }

    private Collider2D FindClosestObject(Collider2D[] hits)
    {
        Collider2D closestObject = hits[0];
        float closestDistance = Vector3.Distance(transform.position, hits[0].transform.position);
        float tempDistance;

        for (int i = 0; i < hits.Length; i++)
        {
            tempDistance = Vector3.Distance(transform.position, hits[i].transform.position);

            if (closestDistance > tempDistance)
            {
                closestObject = hits[i];
                closestDistance = tempDistance;
            }
        }

        return closestObject;
    }

    private IEnumerator WaitCooldown()
    {
        Activated?.Invoke(false);
        WaitForSeconds waitInterval = new WaitForSeconds(_interval);
        float currentCooldown = 0;
        _isOnCooldown = true;

        while (_isOnCooldown && enabled)
        {
            currentCooldown += _interval;
            BarChanged?.Invoke(Mathf.InverseLerp(_minBarValue, _cooldown, currentCooldown));
            yield return waitInterval;

            if (currentCooldown >= _cooldown)
                _isOnCooldown = false;
        }
    }

    private IEnumerator ExecuteAbility()
    {
        _isActive = true;
        Activated?.Invoke(true);
        WaitForSeconds waitInterval = new WaitForSeconds(_interval);
        float currentDuration = _duration;

        while (_isActive && enabled)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _radius, _enemyLayerMask);

            if (hits.Length > 0)
            {
                Collider2D closestObject = FindClosestObject(hits);

                if (closestObject.TryGetComponent(out IDamageable target))
                {
                    target.TakeDamage(_damage);
                    _health.Heal(_damage);
                }
            }

            currentDuration -= _interval;
            BarChanged?.Invoke(Mathf.InverseLerp(_minBarValue, _duration, currentDuration));
            yield return waitInterval;

            if (currentDuration <= _minBarValue)
                _isActive = false;
        }

        yield return StartCoroutine(WaitCooldown());
        _canActivate = true;
    }
}
