using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour, IPickable
{
    [SerializeField] private int _coinsAmount = 1;

    public int CoinAmount => _coinsAmount;
    public event Action<Coin> PickedUp;

    public void PickUp()
    {
        PickedUp?.Invoke(this);
        gameObject.SetActive(false);
    }
}
