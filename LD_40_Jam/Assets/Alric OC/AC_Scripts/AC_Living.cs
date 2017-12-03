using UnityEngine;
using System.Collections;

public class AC_Living : MonoBehaviour, IDamageable
{
    public float startingHealth;
    public float health;
    protected bool dead;

    public event System.Action OnDeath;

    protected virtual void Start()
    {
        health = startingHealth;
    }

    public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        TakeDamage(damage);
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        AudioManager.instance.Play("Enemy Take Hit");

        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    //[ContextMenu("Self Destruct")]
    protected void Die()
    {
        dead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        GameObject.Destroy(gameObject);
    }
}