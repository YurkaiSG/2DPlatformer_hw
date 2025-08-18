using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarView : HealthBar
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _slider.minValue = Health.MinValue;
        _slider.maxValue = Health.MaxValue;
        ChangeView();
    }

    protected override void ChangeView()
    {
        _slider.value = Health.CurrentValue;
    }
}
