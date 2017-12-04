using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossHealthUpdater : MonoBehaviour {

    TextMeshProUGUI TMP;

    void Start()
    {
        TMP = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        TMP.text = string.Format("Health : " + Boss_Health.health);

        if (Boss_Health.health > 14)
            TMP.color = new Color32(146, 255, 0, 255);
        else if (Boss_Health.health > 10)
            TMP.color = new Color32(241, 255, 0, 255);
        else if (Boss_Health.health > 6)
            TMP.color = new Color32(255, 216, 0, 255);
        else if (Boss_Health.health > 3)
            TMP.color = new Color32(255, 195, 0, 255);
        else if (Boss_Health.health >= 0)
            TMP.color = new Color32(255, 0, 0, 255);
    }
}
