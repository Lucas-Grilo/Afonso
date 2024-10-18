using System;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce = 3;
    [SerializeField] private float crouchSpeed = 2;
    [SerializeField] private Vector2 crouchColliderSize = new Vector2(0.6f, 0.9f);
    [SerializeField] private Vector2 normalColliderSize = new Vector2(0.5f, 1);
    [SerializeField] private float slideSpeed = 8;  // Velocidade da escorregada
    [SerializeField] private float slideDuration = 0.5f;  // Duração da escorregada



    [Header("Propriedades de ataque")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private LayerMask attackLayer;

    private float moveDirection;
    private bool isSliding = false;  // Verifica se o personagem está escorregando
    private float slideTimer;  // Controla o tempo da escorregada
    private bool isCrouching = false;

    private Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;
    private Health health;
    private IsGroundedChecker isGroundedCheker;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        isGroundedCheker = GetComponent<IsGroundedChecker>();
        health = GetComponent<Health>();
        health.OnHurt += PlayHurtAudio;
        health.OnDead += HandlePlayerDeath;
    }

    private void Start()
    {
        GameManager.Instance.InputManager.OnJump += HandleJump;
        GameManager.Instance.InputManager.OnSlide += HandleSlide;  // Liga o evento de escorregar
        GameManager.Instance.InputManager.OnCrouch += HandleCrouch;

    }

    private void Update()
    {
        if (isSliding)
        {
            SlidePlayer();
        }
        else
        {
            MovePlayer();
        }

        FlipSpriteAccordingToMoveDirection();
    }

    private void MovePlayer()
    {
        moveDirection = GameManager.Instance.InputManager.Movement;

        if (isCrouching)
        {
            // Se agachado, limitar o movimento horizontal (ou permitir com velocidade reduzida)
            transform.Translate(moveDirection * Time.deltaTime * crouchSpeed, 0, 0);
        }
        else
        {
            // Movimentação normal
            transform.Translate(moveDirection * Time.deltaTime * moveSpeed, 0, 0);
        }
    }

    private void HandleCrouch(bool crouching)
    {
        isCrouching = crouching;

        if (crouching)
        {
            // Mudar o tamanho do colisor e reduzir a velocidade
            Debug.Log("Personagem agachou");
            boxCollider.size = crouchColliderSize;
            moveSpeed = crouchSpeed;
        }
        else
        {
            // Voltar ao estado normal
            Debug.Log("Personagem levantou");
            boxCollider.size = normalColliderSize;
            moveSpeed = 5;
        }
    }

    private void SlidePlayer()
    {
        slideTimer -= Time.deltaTime;
        if (slideTimer <= 0)
        {
            isSliding = false;
            return;
        }

        // Movimento da escorregada
        transform.Translate(moveDirection * Time.deltaTime * slideSpeed, 0, 0);
    }

    private void HandleSlide()
    {
        if (!isGroundedCheker.IsGrounded() || isSliding) return;

        // Inicia a escorregada
        isSliding = true;
        slideTimer = slideDuration;
        //GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerSlide); 
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
        if (!isGroundedCheker.IsGrounded() || isCrouching) return; // Não pular se estiver agachado
        rigidbody.velocity += Vector2.up * jumpForce;
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerJump);
    }

    private void PlayHurtAudio()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerHurt);
    }

    private void HandlePlayerDeath()
    {
        GetComponent<Collider2D>().enabled = false;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerDeath);
        GameManager.Instance.InputManager.DisablePlayerInput();
    }

    private void Attack()
    {
        Collider2D[] hittedEnemies =
            Physics2D.OverlapCircleAll(attackPosition.position, attackRange, attackLayer);

        //GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerAttack);

        foreach (Collider2D hittedEnemy in hittedEnemies)
        {
            if (hittedEnemy.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.TakeDamage();
            }
        }
    }

    private void PlayWalkSound()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerWalk);
    }

    public bool IsSliding()
    {
        return isSliding;  // Retorna o estado da escorregada
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}