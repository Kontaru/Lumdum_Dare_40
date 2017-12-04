using UnityEngine;
using System.Collections;

public class AC_Living : MonoBehaviour, IDamageable
{
    public float startingHealth;
    public float health;

    [Header("Sounds for Audiomanager")]
    public string death_sound;
    public string impact_sound;
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
        AudioManager.instance.Play(death_sound);
        AudioManager.instance.Play(impact_sound);

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