using System;
using UnityEngine;

public interface IPickable
{
    public event Action<GameObject> PickedUp;
    public void PickUp();
}
