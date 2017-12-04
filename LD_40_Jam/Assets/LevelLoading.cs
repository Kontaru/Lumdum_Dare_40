using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoading : MonoBehaviour {

    public void NextScene()
    {
        GameManager.instance.NextScene();
    }

    public void LoadScene(int index)
    {
        GameManager.instance.LoadScene(index);
    }

    public void QuitGame()
    {
        GameManager.instance.EndGame();
    }
}
