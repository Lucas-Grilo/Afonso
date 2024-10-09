using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce = 5;

    [Header("Propriedades de ataque")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private LayerMask attackLayer;


    private Rigidbody2D rigidbody;
    private IsGroundChecker isGroundChecker;
    private float moveDirection;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        isGroundChecker = GetComponent<IsGroundChecker>();
    }
 
    void Start()
    {
        GameManager.Instance.inputManager.OnJump += HandleJump;
    }

    private void Update()
    {     
        MovePlayer();
        FlipSpriteAccordingToMoveDirection();
    }

    private void MovePlayer()
    {
        moveDirection = GameManager.Instance.inputManager.Movement;
        transform.Translate(moveDirection * Time.deltaTime * moveSpeed, 0, 0);
    }
    private void FlipSpriteAccordingToMoveDirection()
    {
        if (moveDirection < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDirection > 0)
        {
            transform.localScale = Vector3.one;
        }
    }


    private void HandleJump()
    {
        if (isGroundChecker.IsGrounded() == false) return;
        rigidbody.velocity += Vector2.up * jumpForce;
    }

    private void HandlePlayerDeath()
    {
        GetComponent<Collider2D>().enabled = false;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        //GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerDeath);
        //GameManager.Instance.InputManager.DisablePlayerInput();
    }

    private void Attack()
    {
        Collider2D[] hittedEnemies =
            Physics2D.OverlapCircleAll(attackPosition.position, attackRange, attackLayer);

        // GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerAttack);

        foreach (Collider2D hittedEnemy in hittedEnemies)
        {
            if (hittedEnemy.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.TakeDamage();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }


}