using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyView : MonoBehaviour
{
    public Action<Transform> FindedTarget;
    public Action LostTarget;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerAttack player))
            FindedTarget?.Invoke(player.transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerAttack player))
            LostTarget?.Invoke();
    }
}
