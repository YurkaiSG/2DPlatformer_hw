using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonHealth : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _value = 0;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Change);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Change);
    }

    private void Change()
    {
        if (_value <= 0)
            _health.TakeDamage(Mathf.Abs(_value));
        else
            _health.Heal(_value);
    }
}
