using System;
using UnityEngine;

public abstract class PickableItem : MonoBehaviour, IPickable
{
    public event Action<PickableItem> PickedUp;

    public virtual void PickUp()
    {
        PickedUp?.Invoke(this);
        gameObject.SetActive(false);
    }
}
