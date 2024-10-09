using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    private IsGroundChecker groundedChecker;
    private Health playerHealth;
   
    private void Awake()
    {
        animator = GetComponent<Animator> ();
        groundedChecker = GetComponent<IsGroundChecker>();
        playerHealth = GetComponent<Health>();

        playerHealth.OnHurt += PlayHurtAnim;

        GameManager.Instance.inputManager.OnAttack += PlayAttackAnim;

    }
    private void Update()
    {
        bool isWalking = GameManager.Instance.inputManager.Movement != 0;
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isJumping", !groundedChecker.IsGrounded());
    }
    private void PlayHurtAnim()
    {
        animator.SetTrigger("hurt");
    }
    private void PlayAttackAnim()
    {
        animator.SetTrigger("attack");
    }
}