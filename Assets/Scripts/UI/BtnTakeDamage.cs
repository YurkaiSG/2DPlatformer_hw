using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BtnTakeDamage : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField][Min(0f)] private float _value = 0;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(TakeDamage);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(TakeDamage);
    }

    private void TakeDamage()
    {
        _health.TakeDamage(_value);
    }
}
