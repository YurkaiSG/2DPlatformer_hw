using UnityEngine;
using UnityEngine.UI;

public class BtnHeal : MonoBehaviour
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
        _button.onClick.AddListener(Heal);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Heal);
    }

    private void Heal()
    {
        _health.Heal(_value);
    }
}
