using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private float _maxValue = 100;
    private float _minValue = 0;

    public float CurrentValue { get; private set; }
    public float MaxValue => _maxValue;

    private void Start()
    {
        CurrentValue = _maxValue;
    }

    public void TakeDamage(float damage)
    {
        float minDamageValue = 0;
        damage = Mathf.Max(damage, minDamageValue);
        float finalHealth = CurrentValue - damage;
        CurrentValue = Mathf.Max(_minValue, finalHealth);

        if (CurrentValue == _minValue)
            Die();
    }

    public void Heal(float value)
    {
        float minHealValue = 0;
        value = Mathf.Max(value, minHealValue);
        float finalHealth = CurrentValue + value;
        CurrentValue = Mathf.Min(_maxValue, finalHealth);
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
