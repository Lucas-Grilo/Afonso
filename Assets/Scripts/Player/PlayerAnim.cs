using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
   
    private void Awake()
    {
        animator = GetComponent<Animator> ();
    }
    private void Update()
    {
        bool isWalking = GameManager.Instance.inputManager.Movement != 0;
        animator.SetBool("isWalking", isWalking);
    }
}