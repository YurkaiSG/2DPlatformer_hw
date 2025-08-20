using UnityEngine;

public class HealthBar : Bar
{
    [SerializeField] protected Health Health;

    private void Start()
    {
        Init(Health.MinValue, Health.MaxValue, Health.CurrentValue);
    }

    private void OnEnable()
    {
        Health.Changed += ChangeView;
    }

    private void OnDisable()
    {
        Health.Changed -= ChangeView;
    }
}
