using UnityEngine;

public class UITargetFollower : MonoBehaviour
{
    [SerializeField] Transform _target;

    private void LateUpdate()
    {
        transform.position = _target.position;
    }
}
