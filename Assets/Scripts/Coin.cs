using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour, IPickable
{
    [SerializeField] private int _coinsAmount = 1;

    public event Action<Coin> PickedUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
            PickUp(player);
    }

    public void PickUp(Player player)
    {
        player.PickUpCoin(_coinsAmount);
        PickedUp?.Invoke(this);
        gameObject.SetActive(false);
    }
}
