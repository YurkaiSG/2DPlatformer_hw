using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerCollector : MonoBehaviour
{
    private int _coinsAmount = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
            PickUpCoin(coin);
    }

    private void PickUpCoin(Coin coin)
    {
        _coinsAmount += coin.CoinAmount;
        Debug.Log(_coinsAmount);
        coin.PickUp();
    }
}
