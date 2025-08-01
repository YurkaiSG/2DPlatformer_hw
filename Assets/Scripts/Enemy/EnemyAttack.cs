using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _damage = 10.0f;
    [SerializeField] private float _cooldown = 1.5f;
    [SerializeField] private float _knockbackForce = 6.0f;
    private bool _canAttack = true;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_canAttack && other.collider.TryGetComponent(out PlayerAttack _))
        {
            if (other.collider.TryGetComponent(out IDamageable player))
            {
                Attack(player);

                if (other.collider.TryGetComponent(out Rigidbody2D rigidbody))
                    rigidbody.AddForce(transform.right * _knockbackForce, ForceMode2D.Impulse);
            }
        }
    }

    private void Attack(IDamageable target)
    {
        target.TakeDamage(_damage);
        _canAttack = false;
        StartCoroutine(WaitCooldown());
    }

    private IEnumerator WaitCooldown()
    {
        WaitForSeconds wait = new WaitForSeconds(_cooldown);
        yield return wait;
        _canAttack = true;
    }
}
