using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce = 5;

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

    private void HandleJump()
    {
        if (isGroundChecker.IsGrounded() == false) return;
        rigidbody.velocity += Vector2.up * jumpForce;
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
}