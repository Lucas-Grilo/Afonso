using System;
using UnityEngine;
public class Health : MonoBehaviour
{
    [SerializeField] private int lives;

    public event Action OnDead;
    public event Action OnHurt;

    public void TakeDamage()
    {
        lives--; HandleDamageTaken();
    }

    private void HandleDamageTaken()
    {
        if (lives <= 0)
        {
            OnDead?.Invoke();
        }
        else
        {
            OnHurt?.Invoke();
        }
    }
}
