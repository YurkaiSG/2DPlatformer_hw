using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FirstAid : MonoBehaviour, IPickable
{
    [SerializeField] private float _healthValue = 45.0f;

    public float HealthValue => _healthValue;
    public event Action<FirstAid> PickedUp;

    public void PickUp()
    {
        PickedUp?.Invoke(this);
        gameObject.SetActive(false);
    }
}
