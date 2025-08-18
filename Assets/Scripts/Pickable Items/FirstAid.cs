using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FirstAid : PickableItem
{
    [SerializeField] private float _healthValue = 45.0f;

    public float HealthValue => _healthValue;
}
