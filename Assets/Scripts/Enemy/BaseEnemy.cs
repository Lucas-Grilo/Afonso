using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public abstract class BaseEnemy : MonoBehaviour
{
    protected Animator animator;
    protected AudioSource audioSource;
    protected Health health;

    protected bool canAttack = true;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        health = GetComponent<Health>();

        health.OnHurt += PlayHurtAnim;
        health.OnDead += HandleDeath;
    }

    protected abstract void Update();

    private void PlayHurtAnim() => animator.SetTrigger("hurt");

    private void HandleDeath()
    {
        canAttack = false;
        //GetComponent<BoxCollider2D>().enabled = false;
        animator.SetTrigger("dead");
        StartCoroutine(DestroyEnemy(2));
    }

    private IEnumerator DestroyEnemy(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
