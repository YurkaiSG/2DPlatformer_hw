using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarView : MonoBehaviour
{
    [SerializeField] private Health _health;
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.minValue = _health.MinValue;
        _slider.maxValue = _health.MaxValue;
        ChangeView();
    }

    private void OnEnable()
    {
        _health.Changed += ChangeView;
    }

    private void OnDisable()
    {
        _health.Changed -= ChangeView;
    }

    private void ChangeView()
    {
        _slider.value = _health.CurrentValue;
    }
}
