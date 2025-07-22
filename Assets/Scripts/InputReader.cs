using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);

    public event Action<float> Moved;
    public event Action Jumped;

    private void Update()
    {
        Moved?.Invoke(Input.GetAxis(Horizontal));

        if (Input.GetButtonDown(Jump))
            Jumped?.Invoke();
    }
}
