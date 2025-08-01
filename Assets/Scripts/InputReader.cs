using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);
    private const string Fire1 = nameof(Fire1);

    public event Action<float> Moved;
    public event Action Jumped;
    public event Action Attacked;

    private void Update()
    {
        Moved?.Invoke(Input.GetAxis(Horizontal));

        if (Input.GetButtonDown(Jump))
            Jumped?.Invoke();

        if (Input.GetButtonDown(Fire1))
            Attacked?.Invoke();
    }
}
