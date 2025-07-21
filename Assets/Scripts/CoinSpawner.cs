using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private int _timeToRespawn = 10;
    private Coin _coin;

    private void Awake()
    {
        _coin = Instantiate(_coinPrefab, transform.position, Quaternion.identity);
    }

    private void OnEnable()
    {
        _coin.PickedUp += Respawn;
    }

    private void OnDisable()
    {
        _coin.PickedUp -= Respawn;
    }

    private void Respawn(Coin coin)
    {
        StartCoroutine(SpawnAfterTime(coin));
    }

    private IEnumerator SpawnAfterTime(Coin coin)
    {
        WaitForSeconds wait = new WaitForSeconds(_timeToRespawn);
        yield return wait;
        coin.gameObject.SetActive(true);
    }
}
