using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AbilityVampiricBar : Bar
{
    [SerializeField] private AbilityVampiric _ability;

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        _ability.BarChanged += ChangeView;
    }

    private void OnDisable()
    {
        _ability.BarChanged -= ChangeView;
    }
}
