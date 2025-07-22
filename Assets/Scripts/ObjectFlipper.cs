using UnityEngine;

public class ObjectFlipper : MonoBehaviour
{
    public void FlipDirection(float direction)
    {
        if (direction < 0)
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        else if (direction > 0)
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
    }
}
