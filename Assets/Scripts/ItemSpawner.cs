using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _timeToRespawn = 10;
    [SerializeField] private Transform[] _spawnPoints;
    private IPickable _item;
    private Vector3 nextSpawnPosition;

    private void Awake()
    {
        if (_spawnPoints.Length == 0)
        {
            Debug.LogWarning($"{this.name} don't have any spawnpoints assigned. Will use spawner position.");
            _spawnPoints = new Transform[] { transform };
        }

        SelectRandomSpawnPoint();
        GameObject spawnedObject = Instantiate(_prefab, nextSpawnPosition, Quaternion.identity);

        if (spawnedObject.TryGetComponent(out _item) == false)
        {
            Debug.LogError($"{this.name} is not a pickable object.");
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _item.PickedUp += Respawn;
    }

    private void OnDisable()
    {
        _item.PickedUp -= Respawn;
    }

    private void Respawn(GameObject item)
    {
        SelectRandomSpawnPoint();
        StartCoroutine(SpawnAfterTime(item));
    }

    private void SelectRandomSpawnPoint()
    {
        nextSpawnPosition = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
    }

    private IEnumerator SpawnAfterTime(GameObject item)
    {
        WaitForSeconds wait = new WaitForSeconds(_timeToRespawn);
        yield return wait;
        item.transform.position = nextSpawnPosition;
        item.SetActive(true);
    }
}
