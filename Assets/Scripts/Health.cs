using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private float _maxValue = 100;

    public event Action Changed;
    public event Action Died;
    public float CurrentValue { get; private set; }
    public float MinValue { get; private set; }
    public float MaxValue => _maxValue;
    public bool IsAlive { get; private set; }

    private void Awake()
    {
        MinValue = 0;
        CurrentValue = _maxValue;
    }

    public void TakeDamage(float damage)
    {
        float minDamageValue = 0;
        damage = Mathf.Max(damage, minDamageValue);
        float finalHealth = CurrentValue - damage;
        CurrentValue = Mathf.Max(MinValue, finalHealth);
        Changed?.Invoke();

        if (CurrentValue == MinValue)
            Die();
    }

    public void Heal(float value)
    {
        float minHealValue = 0;
        value = Mathf.Max(value, minHealValue);
        float finalHealth = CurrentValue + value;
        CurrentValue = Mathf.Min(_maxValue, finalHealth);
        Changed?.Invoke();
    }

    private void Die()
    {
        IsAlive = false;
        Died?.Invoke();
        gameObject.SetActive(false);
    }
}
