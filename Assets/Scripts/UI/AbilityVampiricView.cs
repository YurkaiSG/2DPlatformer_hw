using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AbilityVampiricView : MonoBehaviour
{
    [SerializeField] private AbilityVampiric _ability;
    private SpriteRenderer _sprite;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ChangeSpriteVisibility(false);
    }

    private void OnEnable()
    {
        _ability.Activated += ChangeSpriteVisibility;
    }

    private void OnDisable()
    {
        _ability.Activated -= ChangeSpriteVisibility;
    }

    private void ChangeSpriteVisibility(bool _abilityState)
    {
        if (_abilityState)
            _sprite.enabled = true;
        else
            _sprite.enabled = false;
    }
}
