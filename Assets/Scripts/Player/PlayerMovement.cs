using System;
using UnityEngine;

[RequireComponent(typeof(ObjectFlipper), typeof(InputReader))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    
    private ObjectFlipper _objectFlipper;
    private InputReader _inputReader;

    public bool IsGrounded { get; private set; }
    public event Action<float> Moved;

    private void Awake()
    {
        _objectFlipper = GetComponent<ObjectFlipper>();
        _inputReader = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _inputReader.Moved += Move;
    }

    private void OnDisable()
    {
        _inputReader.Moved -= Move;
    }

    private void Move(float direction)
    {
        _objectFlipper.FlipDirection(direction);
        Moved?.Invoke(direction);
        float _distance = direction * _speed * Time.deltaTime;
        transform.Translate(_distance * Vector3.right, Space.World);
    }
}
