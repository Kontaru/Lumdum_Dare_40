using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUpdater : MonoBehaviour {

    TextMeshProUGUI TMP;

    void Start()
    {
        TMP = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        TMP.text = string.Format("You made it this far, with " + GameManager.instance.player_Coins + " coins, but I'll be taking them all back");
    }
}
