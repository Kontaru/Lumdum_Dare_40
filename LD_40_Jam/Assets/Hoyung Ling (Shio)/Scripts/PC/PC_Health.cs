using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PC_Health : MonoBehaviour {

    public int loadindex;
    public int health;
    public static int max_health;

    // Use this for initialization
    void Start () {
        max_health = health;
    }

    void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            GameManager.instance.LoadScene(loadindex);
        }

        AudioManager.instance.Play("BossDamage");
    }

    void TakeHealth(int heal)
    {
        health += heal;
        AudioManager.instance.Play("Heal");
    }
}
