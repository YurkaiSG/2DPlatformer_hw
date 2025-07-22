using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _cameraDistance = -20;

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (transform.position != _target.position)
        {
            float newPositionX = Mathf.Lerp(transform.position.x, _target.position.x, _speed * Time.deltaTime);
            float newPositionY = Mathf.Lerp(transform.position.y, _target.position.y, _speed * Time.deltaTime);
            transform.position = new Vector3(newPositionX, newPositionY, _cameraDistance);
        }
    }
}
