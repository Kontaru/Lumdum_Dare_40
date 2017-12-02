using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PC_Health : MonoBehaviour {

    public Image[] Health;
    Color alphaStart;
    Color alphaEnd;

    public int health;
    int max_health;

    // Use this for initialization
    void Start () {
        max_health = health;
        alphaStart = Health[0].color;
        alphaEnd = Health[0].color;
        alphaEnd.a = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if (health == 4)
        {
            Health[3].color = alphaStart;
            Health[2].color = alphaStart;
            Health[1].color = alphaStart;
            Health[0].color = alphaStart;
        }
        else if (health == 3)
        {
            Health[3].color = alphaEnd;
            Health[2].color = alphaStart;
            Health[1].color = alphaStart;
            Health[0].color = alphaStart;
        }
        else if (health == 2)
        {
            Health[3].color = alphaEnd;
            Health[2].color = alphaEnd;
            Health[1].color = alphaStart;
            Health[0].color = alphaStart;
        }
        else if (health == 1)
        {
            Health[3].color = alphaEnd;
            Health[2].color = alphaEnd;
            Health[1].color = alphaEnd;
            Health[0].color = alphaStart;
        }
        else if (health == 0)
        {
            Health[3].color = alphaEnd;
            Health[2].color = alphaEnd;
            Health[1].color = alphaEnd;
            Health[0].color = alphaEnd;
        }

    }

    void TakeDamage(int damage)
    {
        health -= damage;
    }
}
