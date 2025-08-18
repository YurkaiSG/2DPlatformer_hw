using System;

public interface IPickable
{
    public event Action<PickableItem> PickedUp;
    public void PickUp();
}
