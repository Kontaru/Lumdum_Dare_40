using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreenMessage : MonoBehaviour {

    public TextMeshProUGUI TMP_1;
    public TextMeshProUGUI TMP_2;
    public TextMeshProUGUI TMP_3;

    void Update()
    {

        TMP_1.text = string.Format("You collected " + GameManager.instance.player_Coins + " coins!");
        TMP_2.text = string.Format("Health multiplier: " + GameManager.instance.player_Health);
        TMP_3.text = string.Format("Final Score: " + GameManager.instance.player_Coins * GameManager.instance.player_Health * 50);

        TMP_1.color = new Color32(255, 216, 0, 255);
        TMP_2.color = new Color32(255, 216, 0, 255);
        TMP_3.color = new Color32(255, 216, 0, 255);
    }
}
