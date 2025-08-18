using UnityEngine;

public abstract class HealthBar : MonoBehaviour
{
    [SerializeField] protected Health Health;

    private void OnEnable()
    {
        Health.Changed += ChangeView;
    }

    private void OnDisable()
    {
        Health.Changed -= ChangeView;
    }

    protected abstract void ChangeView();
}
