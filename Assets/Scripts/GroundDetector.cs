using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Vector2 _groundCheckBoxSize;
    [SerializeField] private float _groundCheckCastDistance;
    private float _groundCheckAngle = 0;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * _groundCheckCastDistance, _groundCheckBoxSize);
    }

    public bool CheckGround()
    {
        if (Physics2D.BoxCast(transform.position, _groundCheckBoxSize, _groundCheckAngle, -transform.up, _groundCheckCastDistance, _groundLayer))
            return true;
        else
            return false;
    }
}
