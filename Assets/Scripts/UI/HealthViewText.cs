using TMPro;
using UnityEngine;

public class HealthViewText : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private char _separator = '/';

    private void Start()
    {
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
        _text.text = $"{(int)_health.CurrentValue} {_separator} {(int)_health.MaxValue}";
    }
}
