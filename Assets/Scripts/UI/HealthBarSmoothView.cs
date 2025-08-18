using UnityEngine;
using UnityEngine.UI;

public class HealthBarSmoothView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _speed = 80f;
    private Slider _slider;
    private float _targetValue;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.minValue = _health.MinValue;
        _slider.maxValue = _health.MaxValue;
        _slider.value = _health.CurrentValue;
        ChangeTargetValue();
    }

    private void OnEnable()
    {
        _health.Changed += ChangeTargetValue;
    }

    private void OnDisable()
    {
        _health.Changed -= ChangeTargetValue;
    }

    private void Update()
    {
        if (_slider.value != _targetValue)
            ChangeView();
    }

    private void ChangeView()
    {
        _slider.value = Mathf.MoveTowards(_slider.value, _targetValue, _speed * Time.deltaTime);
    }

    private void ChangeTargetValue()
    {
        _targetValue = _health.CurrentValue;
    }
}
