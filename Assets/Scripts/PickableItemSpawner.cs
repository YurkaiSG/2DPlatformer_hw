using System.Collections;
using UnityEngine;

public class PickableItemSpawner : MonoBehaviour
{
    [SerializeField] private PickableItem _prefab;
    [SerializeField] private int _timeToRespawn = 10;
    [SerializeField] private Transform[] _spawnPoints;
    private PickableItem _spawnedItem;
    private Vector3 nextSpawnPosition;

    private void Awake()
    {
        if (_spawnPoints.Length == 0)
            _spawnPoints = new Transform[] { transform };

        SelectRandomSpawnPoint();
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

    private void Respawn(PickableItem item)
    {
        SelectRandomSpawnPoint();
        StartCoroutine(SpawnAfterTime(item));
    }

    private void SelectRandomSpawnPoint()
    {
        nextSpawnPosition = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
    }

    private IEnumerator SpawnAfterTime(PickableItem item)
    {
        WaitForSeconds wait = new WaitForSeconds(_timeToRespawn);
        yield return wait;
        item.transform.position = nextSpawnPosition;
        item.gameObject.SetActive(true);
    }
}
