using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private readonly int Speed = Animator.StringToHash(nameof(Speed));

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayMove(float direction)
    {
        _animator.SetFloat(Speed, direction);
    }
}
