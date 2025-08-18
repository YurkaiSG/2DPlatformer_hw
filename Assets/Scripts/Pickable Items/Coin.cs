using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : PickableItem
{
    [SerializeField] private int _coinsAmount = 1;

    public int CoinAmount => _coinsAmount;
}
