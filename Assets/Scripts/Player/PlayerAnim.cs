using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    private IsGroundedChecker groundedChecker;
    private Health playerHealth;
    private PlayerBehaviour playerBehaviour;  // Referência para verificar o status da escorregada

    private void Awake()
    {
        animator = GetComponent<Animator>();
        groundedChecker = GetComponent<IsGroundedChecker>();
        playerHealth = GetComponent<Health>();
        playerBehaviour = GetComponent<PlayerBehaviour>();  // Obtenha a referência do PlayerBehaviour

        GameManager.Instance.InputManager.OnAttack += PlayAttackAnim;
        GameManager.Instance.InputManager.OnCrouch += HandleCrouchAnim;  // Liga o evento de agachamento
        playerHealth.OnHurt += PlayHurtAnim;
        playerHealth.OnDead += PlayDeadAnim;
    }

    private void Update()
    {
        bool isMoving = GameManager.Instance.InputManager.Movement != 0;
        bool isSliding = playerBehaviour.IsSliding();  // Verifica se o personagem está escorregando
        bool isJumping = !groundedChecker.IsGrounded();

        animator.SetBool("isWalking", isMoving && !isSliding);  // Não andar se estiver escorregando
        animator.SetBool("isWalking", isMoving && !animator.GetBool("isCrouching"));  // Não andar se estiver agachado
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isSliding", isSliding);  // Define o estado de escorregada
    }

    private void HandleCrouchAnim(bool isCrouching)
    {
        animator.SetBool("isCrouching", isCrouching);  // Atualiza o estado de agachamento
    }

    private void PlayAttackAnim()
    {
        animator.SetTrigger("attack");
    }

    private void PlayHurtAnim()
    {
        animator.SetTrigger("hurt");
    }

    private void PlayDeadAnim()
    {
        animator.SetTrigger("dead");
    }
}