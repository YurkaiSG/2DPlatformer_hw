using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(InputReader))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _minDamage = 10.0f;
    [SerializeField] private float _maxDamage = 25.0f;
    [SerializeField] private float _cooldown = 6.0f;
    [SerializeField] private Vector2 _attackBoxSize;
    [SerializeField] private Vector2 _attackBoxOffset;
    [SerializeField] private float _attackDistance = 4.0f;
    [SerializeField] private float _knockbackForce = 6.0f;

    private InputReader _inputReader;
    private float _attackAngle = 0;
    private bool _canAttack = true;

    public Action Attacked;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _inputReader.Attacked += Attack;
    }

    private void OnDisable()
    {
        _inputReader.Attacked -= Attack;
    }

    private void OnDrawGizmos()
    {
        Vector2 position = new Vector2(transform.position.x + (transform.right.x * _attackBoxOffset.x * _attackDistance), transform.position.y + _attackBoxOffset.y);
        Gizmos.DrawWireCube(position, _attackBoxSize);
    }

    private void Attack()
    {
        if (_canAttack == false)
            return;

        Attacked?.Invoke();
        Vector3 position = new Vector2(transform.position.x + (transform.right.x * _attackBoxOffset.x), transform.position.y + _attackBoxOffset.y);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(position, _attackBoxSize, _attackAngle, transform.right, _attackDistance);

        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent(out IDamageable target) && this.name != hit.collider.name)
            {
                float finalDamage = UnityEngine.Random.Range(_minDamage, _maxDamage);
                Debug.Log($"{this.name} наносит {finalDamage} урона {target}.");
                target.TakeDamage(finalDamage);

                if (hit.collider.TryGetComponent(out Rigidbody2D rigidbody))
                    rigidbody.AddForce(transform.right * _knockbackForce, ForceMode2D.Impulse);

                _canAttack = false;
                StartCoroutine(WaitCooldown());
            }
        }
    }

    private IEnumerator WaitCooldown()
    {
        WaitForSeconds wait = new WaitForSeconds(_cooldown);
        yield return wait;
        _canAttack = true;
    }
}
