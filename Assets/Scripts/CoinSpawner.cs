using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private int _timeToRespawn = 10;
    private Coin _spawnedItem;

    private void Awake()
    {
        _spawnedItem = Instantiate(_prefab, transform.position, Quaternion.identity);
    }

    private void OnEnable()
    {
        _spawnedItem.PickedUp += Respawn;
    }

    private void OnDisable()
    {
        _spawnedItem.PickedUp -= Respawn;
    }

    private void Respawn(Coin item)
    {
        StartCoroutine(SpawnAfterTime(item));
    }

    private IEnumerator SpawnAfterTime(Coin item)
    {
        WaitForSeconds wait = new WaitForSeconds(_timeToRespawn);
        yield return wait;
        item.gameObject.SetActive(true);
    }
}
