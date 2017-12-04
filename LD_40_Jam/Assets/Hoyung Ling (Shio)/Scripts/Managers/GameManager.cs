using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public int player_Coins;

    bool BL_Pause = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(transform.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        player_Coins = PC_Pouch.coins;
    }

    #region ~ Scene Related ~

    public void NextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex < 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        BL_Pause = !BL_Pause;

        if (BL_Pause == true)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    #endregion
}
