using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Health))]
public class PlayerCollector : MonoBehaviour
{
    private Health _health;
    private int _coinsAmount = 0;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
            PickUpCoin(coin);
        else if (other.TryGetComponent(out FirstAid firstAid))
            if (_health.CurrentValue != _health.MaxValue)
                 PickUpFirstAidKit(firstAid);
    }

    private void PickUpCoin(Coin coin)
    {
        _coinsAmount += coin.CoinAmount;
        Debug.Log(_coinsAmount);
        coin.PickUp();
    }

    private void PickUpFirstAidKit(FirstAid firstAid)
    {
        _health.Heal(firstAid.HealthValue);
        firstAid.PickUp();
    }
}
