using TMPro;
using UnityEngine;

public class HealthBarTextView : HealthBar
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private char _separator = '/';

    private void Start()
    {
        ChangeView();
    }

    protected override void ChangeView()
    {
        _text.text = $"{(int)Health.CurrentValue} {_separator} {(int)Health.MaxValue}";
    }
}
