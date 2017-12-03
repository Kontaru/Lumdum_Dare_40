using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PC_Health : MonoBehaviour {

    public int health;
    int max_health;

    // Use this for initialization
    void Start () {
        max_health = health;
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        AudioManager.instance.Play("Player Take Hit");
        AudioManager.instance.Play("Impact Noise 1");
    }
}
