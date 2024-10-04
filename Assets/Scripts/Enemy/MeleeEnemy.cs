using UnityEngine;
[RequireComponent(typeof(Animator))]
public class MeleeEnemy : BaseEnemy
{
    [SerializeField] private Transform detectPosition;
    [SerializeField] private Vector2 detectBoxSize;
    [SerializeField] private LayerMask playerLayer;

    protected override void Update()
    {
        print("is in sight? " + PlayerInSight());
    }
    private bool PlayerInSight()
    {
        Collider2D playerCollider = Physics2D.OverlapBox(detectPosition.position, detectBoxSize, 0f, playerLayer);
        return playerCollider != null;
    }

    private void OnDrawGizmos()
    {
        if (detectPosition == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectPosition.position, detectBoxSize);
    }
}