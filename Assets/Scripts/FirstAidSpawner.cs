using System.Collections;
using UnityEngine;

public class FirstAidSpawner : MonoBehaviour
{
    [SerializeField] private FirstAid _prefab;
    [SerializeField] private int _timeToRespawn = 10;
    [SerializeField] private Transform[] _spawnPoints;
    private FirstAid _item;
    private Vector3 nextSpawnPosition;

    private void Awake()
    {
        if (_spawnPoints.Length == 0)
            _spawnPoints = new Transform[] { transform };

        SelectRandomSpawnPoint();
        _item = Instantiate(_prefab, nextSpawnPosition, Quaternion.identity);
    }

    private void OnEnable()
    {
        _item.PickedUp += Respawn;
    }

    private void OnDisable()
    {
        _item.PickedUp -= Respawn;
    }

    private void Respawn(FirstAid item)
    {
        SelectRandomSpawnPoint();
        StartCoroutine(SpawnAfterTime(item));
    }

    private void SelectRandomSpawnPoint()
    {
        nextSpawnPosition = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
    }

    private IEnumerator SpawnAfterTime(FirstAid item)
    {
        WaitForSeconds wait = new WaitForSeconds(_timeToRespawn);
        yield return wait;
        item.transform.position = nextSpawnPosition;
        item.gameObject.SetActive(true);
    }
}
