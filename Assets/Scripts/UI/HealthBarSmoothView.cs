using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarSmoothView : HealthBar
{
    [SerializeField] private float _speed = 80f;
    private Slider _slider;
    private Coroutine _sliderChangeRoutine;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _slider.minValue = Health.MinValue;
        _slider.maxValue = Health.MaxValue;
        _slider.value = Health.CurrentValue;
    }

    protected override void ChangeView()
    {
        if (_sliderChangeRoutine != null)
            StopCoroutine(_sliderChangeRoutine);

        _sliderChangeRoutine = StartCoroutine(ChangeSliderSmoothly(Health.CurrentValue));
    }

    private IEnumerator ChangeSliderSmoothly(float _targetValue)
    {
        while (_slider.value != _targetValue && enabled)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _targetValue, _speed * Time.deltaTime);
            yield return null;
        }
    }
}
