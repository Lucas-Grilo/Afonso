using UnityEngine;

public class IsGroundedChecker : MonoBehaviour
{
   [SerializeField] private Transform checkerPosition;
   [SerializeField] private Vector2 checkerSize;
   [SerializeField] private LayerMask groundLayer;

   public bool IsGrounded()
   {
        return Physics2D.OverlapBox(checkerPosition.position, checkerSize, 0f, groundLayer);
   }
   private void OnDrawGizmos()
   {
        Gizmos.DrawWireCube(checkerPosition.position, checkerSize);
   }
}