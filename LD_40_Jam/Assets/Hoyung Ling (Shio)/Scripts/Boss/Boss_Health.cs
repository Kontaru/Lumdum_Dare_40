using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Health : MonoBehaviour {

    public enum State
    {
        Phase1,
        Phase2,
        None
    }

    public State CurrentState;

    public int health;
    int max_health;

    // Use this for initialization
    void Start()
    {
        max_health = health;
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        if (health > 6 && health <= 16)
            CurrentState = State.Phase1;
        else if (health <= 6 && health > 0)
            CurrentState = State.Phase2;
        else if (health == 0)
            Destroy(gameObject);

        AudioManager.instance.Play("Player Take Hit");
        AudioManager.instance.Play("Impact Noise 1");
    }
}
