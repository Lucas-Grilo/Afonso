using UnityEngine;
[RequireComponent(typeof(Animator))]

public abstract class BaseEnemy : MonoBehaviour
{
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }
    protected abstract void Update(); 
}